using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.Boundary.nData;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cMssqlBackupDataManager : cBaseDataManager
    {
        public cMssqlBackupDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }
        public cMssqlBackupEntity GetFirstFullBackup()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cMssqlBackupEntity __MssqlBackupEntity = null;

            cQuery<cMssqlBackupEntity> __Query = __DataService.Database.Query<cMssqlBackupEntity>(() => __MssqlBackupEntity)
                .SelectAllColumns();



            __Query.Where()
                 .Operand<cMssqlBackupEntity>(() => __MssqlBackupEntity, __Item => __Item.BackupType).Eq(EMssqlBackupTypeEnums.Full);
            __Query.OrderBy().Asc(Item => Item.ID).ToQuery();
            cMssqlBackupEntity __QueryResult = __Query.ToList().FirstOrDefault();

            return __QueryResult;
        }


        public List<cMssqlBackupEntity> GetFullBackups()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cMssqlBackupEntity __MssqlBackupEntity = null;

            cQuery<cMssqlBackupEntity> __Query = __DataService.Database.Query<cMssqlBackupEntity>(() => __MssqlBackupEntity)
                .SelectAllColumns();



            __Query.Where()
                 .Operand<cMssqlBackupEntity>(() => __MssqlBackupEntity, __Item => __Item.BackupType).Eq(EMssqlBackupTypeEnums.Full);
            __Query.OrderBy().Asc(Item => Item.ID).ToQuery();
            List<cMssqlBackupEntity> __QueryResult = __Query.ToList();

            return __QueryResult;
        }
        public List<cMssqlBackupEntity> GetBackupsByCreateDate(DateTime _CreateDate)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cMssqlBackupEntity __MssqlBackupEntity = null;

            cQuery<cMssqlBackupEntity> __Query = __DataService.Database.Query<cMssqlBackupEntity>(() => __MssqlBackupEntity)
                .SelectAllColumns();



            __Query.Where()
                 .Operand<cMssqlBackupEntity>(() => __MssqlBackupEntity, __Item => __Item.CreateDate).Lt(_CreateDate);
            __Query.OrderBy().Asc(Item => Item.ID).ToQuery();
            List<cMssqlBackupEntity> __QueryResult = __Query.ToList();

            return __QueryResult;
        }
        public int DeleteBackup(long _ID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            return __DataService.Database.Delete<cMssqlBackupEntity>()
      .Operand(__Item => __Item.ID).Eq(_ID)
      .ToQuery()
      .ExecuteForDeleteAndUpdate();
        }
        public cMssqlBackupEntity AddBackupPoint(EMssqlBackupType _BackupType, string _FilePath, long _FileSize, long _ZipFileSize)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cMssqlBackupEntity __MenuEntity = __DataService.Database.CreateNew<cMssqlBackupEntity>();
            __MenuEntity.BackupType = _BackupType.ID;
            __MenuEntity.FilePath = _FilePath;
            __MenuEntity.FileSize = _FileSize;
            __MenuEntity.ZipFileSize = _ZipFileSize;
            __MenuEntity.Save();
            return __MenuEntity;
        }


    }
}
