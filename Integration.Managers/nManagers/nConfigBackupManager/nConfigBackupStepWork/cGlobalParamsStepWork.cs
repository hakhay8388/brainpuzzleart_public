using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Integration.Managers.nManagers.nWebApiGraph.nConfigBackupManager;
using Data.Boundary.nData;
using Integration.Managers.nConfiguration;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Integration.Managers.nManagers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Integration.Managers.nManagers.nConfigBackupManager.nConfigBackupStepWork
{
    public class cGlobalParamsStepWork : cBaseConfigBackupStep
    {

        public cGlobalParamsStepWork(cApp _App,  IDataServiceManager _DataServiceManager, cPageDataManager _PageDataManager, cParamsDataManager _ParamsDataManager)
            : base(EConfigBackupStepIDs.GlobalParams, _App, _DataServiceManager, _PageDataManager, _ParamsDataManager)
        {
        }

        public override void DoItBackup(List<string> _ConfigBackupFileList, JObject _Params)
        {
            _Params.Add("Version", App.Cfg<cManagersConfiguration>().VersionEntity.DBVersion);
            List<dynamic> __GlobalParamList = ParamsDataManager.GetAllParamsBackup();

            this.Save(_ConfigBackupFileList,__GlobalParamList);

        }
        public override void DoItReload(JObject _Params, string _StepContent)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            string __Version = _Params.GetValue("Version").ToString();
            dynamic __GlobalParamList = JsonConvert.DeserializeObject(_StepContent);

            __DataService.Perform(() =>
            {
                for (int i = 0; i < __GlobalParamList.Count; i++)
                {
                    string __Code = __GlobalParamList[i].Code;
                    string __Value = __GlobalParamList[i].Value;
                    cGlobalParamEntity __GlobalParamEntity = ParamsDataManager.GetParamByCode(__Code);
                    if (__GlobalParamEntity != null)
                    {
                        ParamsDataManager.UpdateGlobalParam(__GlobalParamEntity.ID, __Value);
                    }

                }
            });
            __DataService.LoadGlobalParams();
        }

    }
}
