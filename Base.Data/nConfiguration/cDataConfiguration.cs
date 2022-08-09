using Base.Boundary.nCore.nBootType;
using Base.Boundary.nData;
using Base.Core.nApplication;
using Base.Data.nDataFileEntity;
using Base.Data.nDataService.nDatabase.nSql;
using Base.FileData.nConfiguration;
using Base.FileData.nFileDataService;
using System.Collections.Generic;
using System.IO;

namespace Base.Data.nConfiguration
{
    public class cDataConfiguration : cFileDataConfiguration
    {
        protected cFileDataService FileDataService { get; set; }

        public string DBUserName { get; set; }
        public string DBPassword { get; set; }
        public string DBServer { get; set; }
        public int MaxConnectCount { get; set; }
        public EDBVendor DBVendor { get; set; }
        public string GlobalDBName { get; set; }

        public cVersionEntity VersionEntity { get; set; }
        public cSharedVolumeConfigEntity SharedVolumeConfigEntity { get; set; }

        public bool SimulateDBSynchronize { get; set; }

        public string TargetHostName { get; set; }
        public string ProxyedSiteUrl { get; set; }

        public bool LoadDefaultDataOnStart { get; set; }
        public bool LoadBatchJobOnStart { get; set; }

        public bool LoadGlobalParamsOnStart { get; set; }

        public List<cSql> ExecutedSqlList { get; set; }


        public cDataConfiguration(EBootType _BootType)
            :base(_BootType)
        {
            LoadDefaultDataOnStart = true;
            LoadGlobalParamsOnStart = true;
            LoadBatchJobOnStart = true;
        }

        public void WriteExecutedSqlListToFile()
        {
            string __LogPath = Path.Combine(GeneralLogPath, "SqlExcutionLog.log");

            App.Handlers.FileHandler.AppendString("", __LogPath);
            for (int i = 0; i < ExecutedSqlList.Count; i++)
            {
                App.Handlers.FileHandler.AppendString("\n", __LogPath);
                App.Handlers.FileHandler.AppendString(ExecutedSqlList[i].FullSQLString, __LogPath);
                if (ExecutedSqlList[i].Parameters.Count > 0)
                {
                    App.Handlers.FileHandler.AppendString("\n", __LogPath);
                    App.Handlers.FileHandler.AppendString("//////////// PARAMETERS /////////// ", __LogPath);
                    App.Handlers.FileHandler.AppendString("\n", __LogPath);
                    foreach (var __Item in ExecutedSqlList[i].Parameters)
                    {
                        App.Handlers.FileHandler.AppendString("\t" + __Item.Key + "\t:\t" + __Item.Value, __LogPath);
                        App.Handlers.FileHandler.AppendString("\n", __LogPath);
                    }
                    App.Handlers.FileHandler.AppendString("/////////////////////////////////// ", __LogPath);
                    App.Handlers.FileHandler.AppendString("\n", __LogPath);
                }

            }

            App.Handlers.ProcessHandler.OpenModalProcess("notepad.exe", __LogPath);
        }

        public override void Init()
        {
            base.Init();
            ExecutedSqlList = new List<cSql>();
            FileDataService = App.Factories.ObjectFactory.ResolveInstance<cFileDataService>();
            LoadVersion();            
        }

        private void LoadVersion()
        {
            VersionEntity = FileDataService.FindByID<cVersionEntity>(1);
        }
    }
}
