using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nEnumDataSources;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetSettingsCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nConfigBackupsDataSources
{

    public class cConfigBackupsDataSources : cBaseListDataSourceForCustumQuery<cConfigBackupEntity, cDataSourceReadEmptyOptions>
    {

        cConfigBackupDataManager ConfigBackupDataManager { get; set; }
        public cConfigBackupsDataSources(cApp _App, IDataServiceManager _DataServiceManager, cDataSourceManager _DataSourceManager,
            cDataSourceDataManager _DataSourceDataManager
            , cWebGraph _WebGraph
            , cConfigBackupDataManager _ConfigBackupDataManager)
            : base(DataSourceIDs.ConfigBackups, _App, _WebGraph, _DataServiceManager, _DataSourceManager, _DataSourceDataManager)
        {
            ConfigBackupDataManager = _ConfigBackupDataManager;
        }

        public override Dictionary<string, cBaseDataSourceObject> GetFieldTypes()
        {
            Dictionary<string, cBaseDataSourceObject> __Result = new Dictionary<string, cBaseDataSourceObject>();
            __Result.Add("ID", CreateDataSourceObject("ID", false, ColumnTypeIDs.Numeric, "ID", _OrderFromLeft: 0, _Visible: false));
            __Result.Add("ConfigBackupFile", CreateDataSourceObject("ConfigBackupFile", false, ColumnTypeIDs.String, "ConfigBackupFile", _OrderFromLeft: 1, _TranslateValue: false));
            __Result.Add("CreateDate", CreateDataSourceObject("CreateDate", true, ColumnTypeIDs.Datetime, "CreateDate", _OrderFromLeft: 4, _TranslateValue: false));
            __Result.Add("FilePath", CreateDataSourceObject("FilePath", true, ColumnTypeIDs.String, "FilePath", _OrderFromLeft: 10, _Visible: false));
            __Result.Add("SiteUrl", CreateDataSourceObject("SiteUrl", true, ColumnTypeIDs.String, "SiteUrl", _OrderFromLeft: 10, _Visible: false));
            return __Result;
        }
        public override Action<dynamic> ToDynamicAction(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            return new Action<dynamic>(__Item =>
            {
                string __FileName = __Item.ConfigBackupFile;
                __Item.SiteUrl = __FileName.Split("_")[1];
                __Item.CreateDate = __FileName.Left(8).Right(2) + "." + __FileName.Left(6).Right(2) + "." + __FileName.Left(4) + " " + __FileName.Left(10).Right(2) + ":" + __FileName.Left(12).Right(2);
                cGenericWebScaffoldDataService __GlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
               
                __Item.FilePath = "api/GlobalParamFileDownload/Download/" + DefaultGlobalParamsIDs.ConfigBackupPath.Code + "/" + __Item.ConfigBackupFile;
            });
        }


        public override cQuery<cConfigBackupEntity> SelectedColumns(cQuery<cConfigBackupEntity> _Query)
        {
            return _Query.SelectAll();
        }

        public override ESortDirectionTypes DefaultDirection()
        {
            return ESortDirectionTypes.Ascending;
        }
        public override string DefaultDirectionColumn()
        {
            return "ID";
        }

        public override void ReceiveDataSource_ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            base.ReceiveDataSource_ReadData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public override cQuery<cConfigBackupEntity> ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData, cDataSourceReadEmptyOptions _Options)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();
                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {

                        cGenericWebScaffoldDataService __GlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
                        App.Handlers.FileHandler.MakeDirectory(__GlobalParams.ConfigBackupPath, true);
                        string[] __Files = Directory.GetFiles(__GlobalParams.ConfigBackupPath);
                        __Files = __Files.OrderByDescending(__Item => __Item.ToString()).ToArray();
                        if (__Files.Length > 0)
                        {
                            cQuery<cConfigBackupEntity> __RequestResponseLogs = ConfigBackupDataManager.GetListFromFile(__Files);
                            _ReceivedData.Search = "";
                            _ReceivedData.Filters.Clear();
                            return (cQuery<cConfigBackupEntity>)__DataService.Database.Query<cConfigBackupEntity>(__RequestResponseLogs);
                        }
                        else
                        {

                            _ReceivedData.Search = "";
                            _ReceivedData.Filters.Clear();
                            return (cQuery<cConfigBackupEntity>)__DataService.Database.Query<cConfigBackupEntity>().Top(0);
                        }



                    }
                    else
                    {
                        WebGraph.ActionGraph.NoPermissionAction.Action(_Controller);
                    }
                }
                catch (Exception _Ex)
                {
					WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
				}
            }
            else
            {
                WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
            }
            return null;
        }
    }
}
