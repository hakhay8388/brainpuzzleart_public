using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Integration.Managers.nManagers.nConfigBackupManager;
using Data.Boundary.nData;
using Data.Boundary.nNotificationProps;
using Integration.Managers.nManagers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.GenericWebScaffold.nDataService.nDataManagers;

namespace Integration.Managers.nManagers.nWebApiGraph.nConfigBackupManager
{
    public abstract class cBaseConfigBackupStep : cCoreObject
    {
        protected IDataServiceManager DataServiceManager { get; set; }
        public EConfigBackupStepIDs ConfigBackupStepID { get; set; }

        public cConfigBackupManager ConfigBackupManager { get; set; }

        public cPageDataManager PageDataManager { get; set; }
        public cParamsDataManager ParamsDataManager { get; set; }
        public List<string> Paths { get; set; }


        public cBaseConfigBackupStep(EConfigBackupStepIDs _ConfigBackupStepID, cApp _App,  IDataServiceManager _DataServiceManager
            , cPageDataManager _PageDataManager
            , cParamsDataManager _ParamsDataManager
            )
            : base(_App)
        {
            ConfigBackupStepID = _ConfigBackupStepID;
            DataServiceManager = _DataServiceManager;
            PageDataManager = _PageDataManager;
            ParamsDataManager = _ParamsDataManager;
        }

        public abstract void DoItBackup(List<string> _ConfigBackupFileList,JObject _Params);

        public abstract void DoItReload(JObject _Params,string _StepContent);

        public virtual bool Save(List<string> _ConfigBackupFileList, object __DataValues)
        {
            string __ConfigBackupPath = "";
            try
            {
                __ConfigBackupPath = ConfigBackupManager.GetConfigParamPath();
            }
            catch (Exception _Ex)
            {
                App.Loggers.CoreLogger.LogError(_Ex);
                return false;
            }
            string __Path = __ConfigBackupPath + "\\" + this.ConfigBackupStepID.Code + ".json";
            string __Values = JsonConvert.SerializeObject(__DataValues);
            _ConfigBackupFileList.Add(__Path);
            return App.Handlers.FileHandler.WriteString(__Values, __Path);
        }


        public override void Init()
        {
        }
    }
}
