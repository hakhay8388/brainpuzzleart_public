using Base.Data.nDataFileEntity;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Base.FileData.nFileDataService;
using Core.BatchJobService.nBatchJobManager.nJobs.nTestJob;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.BatchJobService.nDefaultValueTypes;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using Ionic.Zip;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Core.BatchJobService.nBatchJobManager.nJobs.nMssqlBackupJob
{
    public class cMssqlBackupJob : cBaseJob<cMssqlBackupJobProps>
    {
        cMssqlBackupDataManager MssqlBackupDataManager;
        public cMssqlBackupJob(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IManagers _Managers, IMicroService _MicroService, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cMssqlBackupDataManager _MssqlBackupDataManager
            )
         : base(BatchJobIDs.MssqlBackupJob, _CoreServiceContext, _Managers, _MicroService, _DataServiceManager, _FileDataService, _BatchJobDataManager)
        {
            MssqlBackupDataManager = _MssqlBackupDataManager;
        }
        public object GetDirectoryCredentials(string _IncomingPath)
        {

            List<cSharedVolumeConfigEntity> __List = ((cFileDataService)FileDataService).GetAll<cSharedVolumeConfigEntity>();

            cSharedVolumeConfigEntity __SharedVolumeConfigEntity = null;

            __SharedVolumeConfigEntity = __List.Where(__Item => _IncomingPath.StartsWith(__Item.Path)).ToList().FirstOrDefault();


            return new
            {
                Path = __SharedVolumeConfigEntity != null ? __SharedVolumeConfigEntity.Path : "",
                Username = __SharedVolumeConfigEntity != null ? __SharedVolumeConfigEntity.UserName : "",
                Password = __SharedVolumeConfigEntity != null ? __SharedVolumeConfigEntity.Password : ""
            };

        }
        public override cBatchJobResult Run(cMssqlBackupJobProps _JobProps)
        {
            cBatchJobResult __BatchJobResult = new cBatchJobResult("");

            cGenericWebScaffoldDataService __GlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            string __BackupFolder = __GlobalParams.MssqlBackupPath;
            if (!__BackupFolder.EndsWith("\\"))
            {
                __BackupFolder = __BackupFolder + "\\";
            }
            dynamic __DirectoryCredential = GetDirectoryCredentials(__BackupFolder);



            try
            {
                if (__DirectoryCredential.Username == "")
                {
                    App.Handlers.FileHandler.MakeDirectory(__BackupFolder, true);
                }
                else
                {
                    App.Handlers.FileHandler.MakeDirectoryWithCredential(__BackupFolder, true, __DirectoryCredential.Path, __DirectoryCredential.Username, __DirectoryCredential.Password);
                }

            }
            catch (Exception _Ex)
            {
                App.Loggers.BatchJobLogger.LogError(_Ex);
                __BatchJobResult = new cBatchJobResult($"Mssql Yedek Klasörü Oluştururken Hata ile Karşılaşıldı. Path={__BackupFolder}");
                return __BatchJobResult;
            }
            //son full aldıktan sonra 30 gün geçtiyse
            cMssqlBackupEntity __FirstFullBackup = MssqlBackupDataManager.GetFirstFullBackup();
            EMssqlBackupType __BackupType = EMssqlBackupType.Other;

            dynamic __MssqlBackupTypeResult = MssqlCheckBackupType(__BackupFolder, __FirstFullBackup, __BackupType);


            MssqlTakeBackupAndZip(__MssqlBackupTypeResult.Sql, __MssqlBackupTypeResult.BackupType, __BatchJobResult);

            Managers.ConfigBackupManager.DoItBackup();

            return __BatchJobResult;
        }

        public object MssqlCheckBackupType(string _BackupFolder, cMssqlBackupEntity _FirstFullBackup, EMssqlBackupType _BackupType)
        {
            dynamic __DirectoryCredential = GetDirectoryCredentials(_BackupFolder);

            App.Utils.ImpersonatedUserUtils.ConnectPath(__DirectoryCredential.Path, __DirectoryCredential.Username, __DirectoryCredential.Password);
            string[] __OldFiles = Directory.GetFiles(_BackupFolder, "*.*");

            bool __HasFullBackupDirectory = Array.Exists(__OldFiles, __Item => __Item.Contains("_full_"));
            string __Sql = "";


            if (_FirstFullBackup == null || DateTime.Today.Day == 1)
            {
                if (_FirstFullBackup == null)
                {
                    __Sql = GetFullSql(_BackupFolder);
                    _BackupType = EMssqlBackupType.Full;
                }
                else
                {
                    List<cMssqlBackupEntity> __FullBackups = MssqlBackupDataManager.GetFullBackups();
                    bool __FullMonthBackup = false;
                    for (int i = 0; i < __FullBackups.Count; i++)
                    {
                        if (__FullBackups[i].CreateDate > DateTime.Today)
                        {
                            if (File.Exists(__FullBackups[i].FilePath) == true)
                            {
                                __FullMonthBackup = true;
                                break;
                            }


                        }
                    }
                    if (!__FullMonthBackup)
                    {
                        __Sql = GetFullSql(_BackupFolder);
                        _BackupType = EMssqlBackupType.Full;
                    }
                    else
                    {
                        __Sql = GetDiffSql(_BackupFolder);
                        _BackupType = EMssqlBackupType.Diff;
                    }
                }

            }
            else
            {
                if (__HasFullBackupDirectory == false)
                {
                    __Sql = GetFullSql(_BackupFolder);
                    _BackupType = EMssqlBackupType.Full;
                }
                else
                {
                    __Sql = GetDiffSql(_BackupFolder);
                    _BackupType = EMssqlBackupType.Diff;
                }
            }
            App.Utils.ImpersonatedUserUtils.DisConnectPath(__DirectoryCredential.Path);
            return new
            {
                Sql = __Sql,
                BackupType = _BackupType
            };

        }

        public void MssqlTakeBackupAndZip(string _Sql, EMssqlBackupType _BackupType, cBatchJobResult _BatchJobResult)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cSql __cSql = DataServiceManager.GetDataService().Database.DefaultConnection.CreateSql(_Sql);
            object __Result = DataServiceManager.GetDataService().Database.DefaultConnection.ExecuteScalar(__cSql);
            if (__Result != null && __Result.ToString() != "")
            {

                string __BackupPath = __Result.ToString();
                dynamic __DirectoryCredential = GetDirectoryCredentials(__BackupPath);
                if (__DirectoryCredential.Username != "")
                {
                    App.Utils.ImpersonatedUserUtils.ConnectPath(__DirectoryCredential.Path, __DirectoryCredential.Username, __DirectoryCredential.Password);
                }
                long __BackupFileSize = new FileInfo(__BackupPath).Length;
                string __ZipFilePath = "";
                
                __ZipFilePath = App.Handlers.FileHandler.File_Zip(__BackupPath, true);

                string __BackupLog = "";
                long __ZipFileSize = 0;
                __BackupLog += "Mssql Yedek Alındı." + System.Environment.NewLine;

                if (__ZipFilePath.IsNullOrEmpty())
                {
                    __BackupLog += "Dosya Ziplenemedi." + System.Environment.NewLine;
                }
                else
                {
                    __ZipFileSize = new FileInfo(__ZipFilePath).Length;
                }
                __DataService.Perform(() =>
                {
                    MssqlBackupDataManager.AddBackupPoint(_BackupType, __ZipFilePath != "" ? __ZipFilePath : __BackupPath, __BackupFileSize, __ZipFileSize);
                });
                List<cMssqlBackupEntity> __FullBackups = MssqlBackupDataManager.GetFullBackups();
                if (__FullBackups.Count > 2)
                {
                    cMssqlBackupEntity __OldBackupClearPoint = __FullBackups[1];
                    List<cMssqlBackupEntity> __OldBackups = MssqlBackupDataManager.GetBackupsByCreateDate(__OldBackupClearPoint.CreateDate);
                    for (int i = 0; i < __OldBackups.Count; i++)
                    {
                        if (File.Exists(__OldBackups[i].FilePath))
                        {
                            try
                            {
                                File.Delete(__OldBackups[i].FilePath);
                                __BackupLog += "Eski Yedek Dosyası Silindi." + __OldBackups[i].FilePath + System.Environment.NewLine;
                            }
                            catch (Exception _Ex)
                            {
                                App.Loggers.BatchJobLogger.LogError(_Ex);
                                __BackupLog += "Eski Yedek Dosyası Silinemedi." + __OldBackups[i].FilePath + System.Environment.NewLine;
                            }
                        }
                        else
                        {
                            __BackupLog += "Eski Yedek Dosyası Manuel Silinmiş." + __OldBackups[i].FilePath + System.Environment.NewLine;
                        }
                        __DataService.Perform(() =>
                        {
                            MssqlBackupDataManager.DeleteBackup(__OldBackups[i].ID);
                        });
                    }
                }
                App.Utils.ImpersonatedUserUtils.DisConnectPath(__DirectoryCredential.Path);



                _BatchJobResult = new cBatchJobResult(__BackupLog);
            }
            else
            {
                _BatchJobResult = new cBatchJobResult("Mssql Yedek Alınamadı.Result boş.");
            }
        }

        public string GetFullSql(string _BackupFolder)
        {

            string __Sql = @$"DECLARE @name VARCHAR(50) -- database name  
DECLARE @path VARCHAR(256) -- path for backup files  
DECLARE @fileName VARCHAR(256) -- filename for backup  
DECLARE @fileDate VARCHAR(20) -- used for file name 

SET @path = '{_BackupFolder}'  
 

SELECT @fileDate = CONVERT(VARCHAR(20),GETDATE(),112) + '' + REPLACE(CONVERT(VARCHAR(5),getdate(),108),':','') 

DECLARE db_cursor CURSOR FOR  
SELECT DB_NAME() as name 


OPEN db_cursor   
FETCH NEXT FROM db_cursor INTO @name   

WHILE @@FETCH_STATUS = 0   
BEGIN   
       SET @fileName = @path + @name + '_full_' +''+ @fileDate + '.BAK'  
       BACKUP DATABASE @name TO DISK = @fileName  

       FETCH NEXT FROM db_cursor INTO @name   
END   

CLOSE db_cursor   
DEALLOCATE db_cursor
select @fileName";
            return __Sql;
        }
        public string GetDiffSql(string _BackupFolder)
        {
            string __Sql = @$"DECLARE @name VARCHAR(50) -- database name  
                            DECLARE @path VARCHAR(256) -- path for backup files  
                            DECLARE @fileName VARCHAR(256) -- filename for backup  
                            DECLARE @fileDate VARCHAR(20) -- used for file name 

                            SET @path = '{_BackupFolder}'  

                            SELECT @fileDate = CONVERT(VARCHAR(20),GETDATE(),112)  + '' + REPLACE(CONVERT(VARCHAR(5),getdate(),108),':','') 

                            DECLARE db_cursor CURSOR FOR  
                            SELECT DB_NAME() as name 


                            OPEN db_cursor   
                            FETCH NEXT FROM db_cursor INTO @name   

                            WHILE @@FETCH_STATUS = 0   
                            BEGIN   
                                   SET @fileName = @path + @name + '_diff_' + @fileDate + '.BAK'  
                                   BACKUP DATABASE @name TO DISK = @fileName  WITH DIFFERENTIAL

                                   FETCH NEXT FROM db_cursor INTO @name   
                            END   

                            CLOSE db_cursor   
                            DEALLOCATE db_cursor
                            select @fileName";
            return __Sql;
        }

    }
}
