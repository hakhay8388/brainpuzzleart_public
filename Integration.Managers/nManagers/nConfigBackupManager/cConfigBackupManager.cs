using System;
using System.Collections.Generic;
using System.Linq;
using Base.Core.nCore;
using Base.Core.nApplication;
using Base.Data.nDataServiceManager;
using Integration.Managers.nManagers.nWebApiGraph.nConfigBackupManager;
using Data.GenericWebScaffold.nDataService;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Integration.Managers.nManagers;

namespace Integration.Managers.nManagers.nConfigBackupManager
{
    public class cConfigBackupManager : cBaseManager
    {
        IDataServiceManager DataServiceManager { get; set; }


        public List<cBaseConfigBackupStep> ConfigBackupSteps { get; set; }

        public cConfigBackupManager(cApp _App, IDataServiceManager _DataServiceManager)
            : base(_App)
        {
            ConfigBackupSteps = new List<cBaseConfigBackupStep>();
            DataServiceManager = _DataServiceManager;

        }

        public cBaseConfigBackupStep GetStepByWorkCode(string _WorkCode)
        {
            return ConfigBackupSteps.Where(__Item => __Item.ConfigBackupStepID.Code == _WorkCode).FirstOrDefault();
        }
        public string GetConfigParamPath(bool _DirectoryCreate = true)
        {
            cGenericWebScaffoldDataService __GlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();


            string __ConfigBackupPath = __GlobalParams.ConfigBackupPath;

            if (!__ConfigBackupPath.EndsWith("\\"))
            {
                __ConfigBackupPath = __ConfigBackupPath + "\\";
            }
            __ConfigBackupPath = __ConfigBackupPath + "\\" + App.Handlers.StringHandler.HunDate();
            if (_DirectoryCreate)
            {
                if (Directory.Exists(__ConfigBackupPath) == false)
                {
                    Directory.CreateDirectory(__ConfigBackupPath);
                }
            }

            return __ConfigBackupPath;
        }

        public bool Save(List<string> __ConfigBackupFileList, object _DataValues)
        {
            string __ConfigBackupPath = "";
            try
            {
                __ConfigBackupPath = GetConfigParamPath();
            }
            catch (Exception _Ex)
            {
                App.Loggers.CoreLogger.LogError(_Ex);
                return false;
            }
            string __Path = __ConfigBackupPath + "\\BackupConfig.json";
            string __Values = JsonConvert.SerializeObject(_DataValues);
            __ConfigBackupFileList.Add(__Path);
            return App.Handlers.FileHandler.WriteString(__Values, __Path);
        }

        public void DoItBackup()
        {
            List<string> __ConfigBackupFileList = new List<string>();

            JObject __Object = new JObject();
            JArray __Operations = new JArray();

            __Object.Add("Backups", __Operations);
            foreach (var __Item in ConfigBackupSteps)
            {
                JObject __ParamObject = new JObject();
                __Operations.Add(__ParamObject);
                __ParamObject.Add("Name", __Item.ConfigBackupStepID.Code);

                __Item.DoItBackup(__ConfigBackupFileList, __ParamObject);
            }
            Save(__ConfigBackupFileList, __Object);
            string __Host = DataServiceManager.GetDataHost();
            App.Handlers.FileHandler.ZipFiles(GetConfigParamPath(false) + "_" + __Host, __ConfigBackupFileList, true);
            cGenericWebScaffoldDataService __GlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            App.Handlers.FileHandler.EmptyDirectoryClear(__GlobalParams.ConfigBackupPath);
        }

        public void ReloadBackup(string _Path, List<string> _ConfigBackupStep)
        {
            List<string> ConfigBackupFileList = new List<string>();

            cGenericWebScaffoldDataService __GlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            string __ZipFilePath = __GlobalParams.ConfigBackupPath + "\\" + _Path;
            string __ExtractedPath = App.Handlers.FileHandler.ZipFilesExtract(__ZipFilePath);
            string __BackupConfigFileRead = __ExtractedPath + "\\BackupConfig.json";
            string __BackupConfigFileReadContent = App.Handlers.FileHandler.ReadString(__BackupConfigFileRead);
            JObject __Params = JObject.Parse(__BackupConfigFileReadContent);
            JArray __Backups = (JArray)__Params.GetValue("Backups");


            foreach (JObject __JsonItem in __Backups)
            {
                string __StepName = __JsonItem.GetValue("Name").ToString();
                if (_ConfigBackupStep.IndexOf(__StepName) > -1)
                {
                    cBaseConfigBackupStep __BackupStepItem = ConfigBackupSteps.Where(__Item => __Item.ConfigBackupStepID.Code == __StepName).FirstOrDefault();
                    if (__BackupStepItem != null)
                    {
                        string __StepFile = __ExtractedPath + "\\" + __StepName + ".json";
                        string __StepContent = App.Handlers.FileHandler.ReadString(__StepFile);
                        __BackupStepItem.DoItReload(__JsonItem, __StepContent);
                    }

                }
            }
            Directory.Delete(__ExtractedPath, true);
        }

        public void Init()
        {
            List<Type> __ConfigBackupSteps = App.Handlers.AssemblyHandler.GetTypesFromBaseType<cBaseConfigBackupStep>();
            __ConfigBackupSteps.ForEach(__Type =>
            {
                cBaseConfigBackupStep __Step = (cBaseConfigBackupStep)App.Factories.ObjectFactory.ResolveInstance(__Type);
                __Step.ConfigBackupManager = this;
                __Step.Init();
                ConfigBackupSteps.Add(__Step);
            });


        }
        public TConfigBackupStep GetListenerByType<TConfigBackupStep>()
            where TConfigBackupStep : cBaseConfigBackupStep
        {
            TConfigBackupStep __Listener = (TConfigBackupStep)ConfigBackupSteps.Where(__Item => typeof(TConfigBackupStep).IsAssignableFrom(__Item.GetType())).FirstOrDefault();
            return __Listener;
        }

    }
}
