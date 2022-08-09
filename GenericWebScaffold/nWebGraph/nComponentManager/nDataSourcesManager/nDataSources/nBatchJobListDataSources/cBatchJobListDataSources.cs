using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataServiceManager;
using Core.BatchJobService.nDataService.nDataManagers;
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
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nBatchJobListDataSources
{
    public class cBatchJobListDataSources : cBaseListDataSourceForCustumQuery<cBatchJobEntity, cDataSourceReadEmptyOptions>
    {

        cBatchJobDataManager BatchJobDataManager { get; set; }
        public cBatchJobListDataSources(
            cApp _App
            , cWebGraph _WebGraph
            , IDataServiceManager _DataServiceManager
            , cDataSourceManager _DataSourceManager
            , cDataSourceDataManager _DataSourceDataManager
            , cBatchJobDataManager _BatchJobDataManager
        )
            : base(DataSourceIDs.BatchJobList, _App, _WebGraph, _DataServiceManager, _DataSourceManager, _DataSourceDataManager)
        {
            BatchJobDataManager = _BatchJobDataManager;
        }

        public override Dictionary<string, cBaseDataSourceObject> GetFieldTypes()
        {
            Dictionary<string, cBaseDataSourceObject> __Result = new Dictionary<string, cBaseDataSourceObject>();
            //__Result.Add("Code", CreateDataSourceObject("Code", false, ColumnTypeIDs.String, "Code", _OrderFromLeft: 0));
            __Result.Add("ID", CreateDataSourceObject("ID", false, ColumnTypeIDs.Numeric, "ID", _OrderFromLeft: 0, _Visible: false));
            __Result.Add("Name", CreateDataSourceObject("Name", false, ColumnTypeIDs.String, "Name", _OrderFromLeft: 1, _TranslateValue: true));
            __Result.Add("TimePeriodMilisecond", CreateDataSourceObject("TimePeriodMilisecond", false, ColumnTypeIDs.Numeric, "TimePeriodMilisecond", _OrderFromLeft: 2));
            __Result.Add("State", CreateDataSourceObject("State", false, ColumnTypeIDs.Numeric, "State", _OrderFromLeft: 3, _Visible: false));
            __Result.Add("StateText", CreateDataSourceObject("StateText", true, ColumnTypeIDs.String, "StateText", _OrderFromLeft: 3));
            __Result.Add("ExecuteFirstWithoutWait", CreateDataSourceObject("ExecuteFirstWithoutWait", false, ColumnTypeIDs.String, "ExecuteFirstWithoutWait", _OrderFromLeft: 4));
            __Result.Add("AutoAddExecution", CreateDataSourceObject("AutoAddExecution", false, ColumnTypeIDs.String, "AutoAddExecution", _OrderFromLeft: 5));
            __Result.Add("StopAfterFirstExecution", CreateDataSourceObject("StopAfterFirstExecution", false, ColumnTypeIDs.String, "StopAfterFirstExecution", _OrderFromLeft: 6));
            __Result.Add("MaxRetryCount", CreateDataSourceObject("MaxRetryCount", false, ColumnTypeIDs.Numeric, "MaxRetryCount", _OrderFromLeft: 7));

            return __Result;
        }

        public override cQuery<cBatchJobEntity> ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData, cDataSourceReadEmptyOptions _Options)
        {

            IDataService __DataService = DataServiceManager.GetDataService();

            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();
                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {
                        return (cQuery<cBatchJobEntity>)__DataService.Database.Query<cBatchJobEntity>();
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

        public override Action<dynamic> ToDynamicAction(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            return new Action<dynamic>(__Item =>
            {
                __Item.StateText = __Item.State== EBatchJobState.Started.ID ? _Controller.GetWordValue(EBatchJobState.Started.Name) : _Controller.GetWordValue(EBatchJobState.Stopped.Name);
                __Item.ExecuteFirstWithoutWait = __Item.ExecuteFirstWithoutWait ? _Controller.GetWordValue("Yes") : _Controller.GetWordValue("No");
                __Item.AutoAddExecution = __Item.AutoAddExecution ? _Controller.GetWordValue("Yes") : _Controller.GetWordValue("No");
                __Item.StopAfterFirstExecution = __Item.StopAfterFirstExecution ? _Controller.GetWordValue("Yes") : _Controller.GetWordValue("No");


                //__Item.ReservationDate = ((DateTime)__Item.ReservationDate).ToString("dd.MM.yyyy HH:mm");
                //__Item.BookedDate = ((DateTime)__Item.BookedDate).ToString("dd.MM.yyyy HH:mm");

            });
        }

        public override cQuery<cBatchJobEntity> SelectedColumns(cQuery<cBatchJobEntity> _Query)
        {
            return _Query.SelectAll();
        }

        public override ESortDirectionTypes DefaultDirection()
        {
            return ESortDirectionTypes.Ascending;
        }

        public override string DefaultDirectionColumn()
        {
            return "Name";
        }
    }
}
