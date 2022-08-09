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
    public class cBatchJobExcutionListDataSources : cBaseListDataSourceForCustumQuery<cBatchJobExecutionEntity, cBatchJobExcutionListDataSourcesOptions>
    {

        cBatchJobExecutionDataManager BatchJobExecutionDataManager { get; set; }
        public cBatchJobExcutionListDataSources(
            cApp _App
            , cWebGraph _WebGraph
            , IDataServiceManager _DataServiceManager
            , cDataSourceManager _DataSourceManager
            , cDataSourceDataManager _DataSourceDataManager
            , cBatchJobExecutionDataManager _BatchJobExecutionDataManager
        )
            : base(DataSourceIDs.BatchJobExecutionList, _App, _WebGraph, _DataServiceManager, _DataSourceManager, _DataSourceDataManager)
        {
            BatchJobExecutionDataManager = _BatchJobExecutionDataManager;
        }

        public override Dictionary<string, cBaseDataSourceObject> GetFieldTypes()
        {
            Dictionary<string, cBaseDataSourceObject> __Result = new Dictionary<string, cBaseDataSourceObject>();
            //__Result.Add("Code", CreateDataSourceObject("Code", false, ColumnTypeIDs.String, "Code", _OrderFromLeft: 0));
            __Result.Add("ID", CreateDataSourceObject("ID", false, ColumnTypeIDs.Numeric, "ID", _OrderFromLeft: 0, _Visible: false));
            __Result.Add("ParameterObjects", CreateDataSourceObject("ParameterObjects", false, ColumnTypeIDs.String, "ParameterObjects", _OrderFromLeft: 1, _Visible: false));
            __Result.Add("State", CreateDataSourceObject("State", false, ColumnTypeIDs.Numeric, "State", _OrderFromLeft: 3, _Visible: false));
            __Result.Add("StateText", CreateDataSourceObject("StateText", true, ColumnTypeIDs.String, "StateText", _OrderFromLeft: 3));
            __Result.Add("Exception", CreateDataSourceObject("Exception", false, ColumnTypeIDs.String, "Exception", _OrderFromLeft: 4));
            __Result.Add("Result", CreateDataSourceObject("Result", false, ColumnTypeIDs.String, "Result", _OrderFromLeft: 5));
            __Result.Add("ElapsedTimeMilisecond", CreateDataSourceObject("ElapsedTimeMilisecond", false, ColumnTypeIDs.Numeric, "ElapsedTimeMilisecond", _OrderFromLeft: 6));
            __Result.Add("CurrentTryCount", CreateDataSourceObject("CurrentTryCount", false, ColumnTypeIDs.Numeric, "CurrentTryCount", _OrderFromLeft: 7));
            __Result.Add("ExecutionTime", CreateDataSourceObject("ExecutionTime", false, ColumnTypeIDs.String, "ExecutionTime", _OrderFromLeft: 8));


            return __Result;
        }

        public override cQuery<cBatchJobExecutionEntity> ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData, cBatchJobExcutionListDataSourcesOptions _Options)
        {

            IDataService __DataService = DataServiceManager.GetDataService();

            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();
                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {
                        IQuery __SubQuery = __DataService.Database.Query<cBatchJobExecutionEntity>()
                            .SelectAll()
                            .Where()
                            .Operand<cBatchJobEntity>().Eq(_Options.BatchJobID)
                            .ToQuery();

                        return (cQuery<cBatchJobExecutionEntity>)__DataService.Database.Query<cBatchJobExecutionEntity>(__SubQuery);
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
                __Item.StateText = _Controller.GetWordValue(EBatchJobExecutionState.GetByID(__Item.State, null).Name);
                __Item.ExecutionTime = ((DateTime)__Item.ExecutionTime).ToString("dd.MM.yyyy HH:mm");
                __Item.ParameterObjects = ((string)__Item.ParameterObjects).Left(((string)__Item.ParameterObjects).Length - 1).Right(((string)__Item.ParameterObjects).Length - 2);
                //__Item.ReservationDate = ((DateTime)__Item.ReservationDate).ToString("dd.MM.yyyy HH:mm");
                //__Item.BookedDate = ((DateTime)__Item.BookedDate).ToString("dd.MM.yyyy HH:mm");

            });
        }

        public override cQuery<cBatchJobExecutionEntity> SelectedColumns(cQuery<cBatchJobExecutionEntity> _Query)
        {
            return _Query.SelectAll();
        }

        public override ESortDirectionTypes DefaultDirection()
        {
            return ESortDirectionTypes.Descending;
        }

        public override string DefaultDirectionColumn()
        {
            return "ExecutionTime";
        }
    }
}
