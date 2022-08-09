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
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nLanguageListDataSources
{

    public class cLanguageListDataSource : cBaseListDataSourceForCustumQuery<cLanguageWordEntity, cDataSourceReadEmptyOptions>
    {

        cLanguageDataManager LanguageDataManager { get; set; }
        public cLanguageListDataSource(cApp _App, IDataServiceManager _DataServiceManager, cDataSourceManager _DataSourceManager,
            cDataSourceDataManager _DataSourceDataManager
            , cWebGraph _WebGraph
            , cLanguageDataManager _LanguageDataManager)
            : base(DataSourceIDs.LanguageList, _App, _WebGraph, _DataServiceManager, _DataSourceManager, _DataSourceDataManager)
        {
            LanguageDataManager = _LanguageDataManager;
        }


        public override cQuery<cLanguageWordEntity> ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData, cDataSourceReadEmptyOptions _Options)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();
                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {

                        cQuery<cLanguageWordEntity> __NotActivatedUserList = LanguageDataManager.GetLanguages(_ReceivedData.Search);
                        _ReceivedData.Search = "";
                        _ReceivedData.Filters.Clear();
                        return (cQuery<cLanguageWordEntity>)__DataService.Database.Query<cLanguageWordEntity>(__NotActivatedUserList);


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
        public override Dictionary<string, cBaseDataSourceObject> GetFieldTypes()
        {
            Dictionary<string, cBaseDataSourceObject> __Result = new Dictionary<string, cBaseDataSourceObject>();
            __Result.Add("ID", CreateDataSourceObject("ID", false, ColumnTypeIDs.Numeric, "ID", _OrderFromLeft: 0, _Visible: false));
            __Result.Add("LanguageName", CreateDataSourceObject("Name", false, ColumnTypeIDs.String, "LanguageName", _OrderFromLeft: 1, _TranslateValue: false));
            __Result.Add("CodeLanguage", CreateDataSourceObject("Code", false, ColumnTypeIDs.String, "CodeLanguage", _OrderFromLeft: 2, _TranslateValue: false));
            __Result.Add("LanguageWord", CreateDataSourceObject("Word", false, ColumnTypeIDs.String, "LanguageWord", _TranslateValue: false, _OrderFromLeft: 3));
            __Result.Add("CreateDate", CreateDataSourceObject("CreateDate", true, ColumnTypeIDs.Datetime, "CreateDate", _OrderFromLeft: 4, _TranslateValue: false));
            __Result.Add("ParamCount", CreateDataSourceObject("ParamCount", false, ColumnTypeIDs.Numeric, _OrderFromLeft: 0, _Visible: false, _Title: "ParamCountField"));
            return __Result;
        }
        //public override Action<dynamic> ToDynamicAction(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        //{

        //}


        public override cQuery<cLanguageWordEntity> SelectedColumns(cQuery<cLanguageWordEntity> _Query)
        {
            return _Query.SelectAll();
        }

        public override ESortDirectionTypes DefaultDirection()
        {
            return ESortDirectionTypes.Descending;
        }
        public override string DefaultDirectionColumn()
        {
            return "CreateDate";
        }
        public override List<int> GetPageSizes()
        {
            List<int> __PageSizes = new List<int>();
            __PageSizes.Add(5);
            __PageSizes.Add(10);
            __PageSizes.Add(20);
            __PageSizes.Add(1000);
            __PageSizes.Add(100000);
            return __PageSizes;
        }


    }
}
