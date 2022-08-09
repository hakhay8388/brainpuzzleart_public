using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nUserListDataSources
{
    public class cUserList_CustomQueryDataSource : cBaseListDataSourceForCustumQuery<cUserEntity, cDataSourceReadEmptyOptions>
    {
        cUserEntity m_UserAlias = null;
        public cUserDataManager UserDataManager { get; set; }
        public cRoleDataManager RoleDataManager { get; set; }

        public cUserList_CustomQueryDataSource(cApp _App, IDataServiceManager _DataServiceManager, cDataSourceManager _DataSourceManager,
            cDataSourceDataManager _DataSourceDataManager,
            cRoleDataManager _RoleDataManager,
            cUserDataManager _UserDataManager,

            cWebGraph _WebGraph)
            : base(DataSourceIDs.UserList_CustomQuery, _App, _WebGraph, _DataServiceManager, _DataSourceManager, _DataSourceDataManager)
        {
            UserDataManager = _UserDataManager;
            RoleDataManager = _RoleDataManager;

        }

        public override Dictionary<string, cBaseDataSourceObject> GetFieldTypes()
        {
            Dictionary<string, cBaseDataSourceObject> __Result = new Dictionary<string, cBaseDataSourceObject>();

            //__Result.Add("TestSelect", CreateDataSourceObject("TestSelect", false, ColumnTypeIDs.String));

            __Result.Add("ID", CreateDataSourceObject("ID", false, ColumnTypeIDs.Numeric, _Visible: false));
            __Result.Add("Name", CreateDataSourceObject("Name", false, ColumnTypeIDs.String));
            __Result.Add("Surname", CreateDataSourceObject("RealSurname", false, ColumnTypeIDs.String));
            __Result.Add("Email", CreateDataSourceObject("Email", false, ColumnTypeIDs.String));
            __Result.Add("Language", CreateDataSourceObject("Language", false, ColumnTypeIDs.String));
            __Result.Add("CreateDate", CreateDataSourceObject("CreateDate", false, ColumnTypeIDs.Datetime));
            __Result.Add("UpdateDate", CreateDataSourceObject("UpdateDate", false, ColumnTypeIDs.Datetime));
            __Result.Add("State", CreateDataSourceObject("State", false, ColumnTypeIDs.Numeric, "StateText", _OrderFromLeft: 7, _Visible: true, _Editable: ColumnEditableIDs.Always, _LookUpDataSource: new cUserStateLookUpDataSource()));
            __Result.Add("RoleCode", CreateDataSourceObject("RoleID", false, ColumnTypeIDs.Numeric, "RoleCode", _OrderFromLeft: 8, _Visible: true));

            return __Result;

        }


        public override void ReceiveDataSource_UpdateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_UpdateCommandData _ReceivedData)
        {
            if (_ReceivedData.Item != null)
            {
                if (_Controller.ClientSession.IsLogined)
                {
                    List<cDataSourcePermissionEntity> __Permissions = DataSourceDataManager.GetDataSourceInRoleByDataSourceID(_Controller.ClientSession.User.Actor.GetValue().Roles.ToList(), DataSourceID);
                    if (__Permissions.Find(__Item => __Item.CanUpdate) != null)
                    {
                        IDataService __DataService = DataServiceManager.GetDataService();
                        DataServiceManager.GetDataService().Perform(() =>
                        {

                            JObject __NewData = _ReceivedData.Item["NewData"].Value<JObject>();
                            JObject __OldData = _ReceivedData.Item["OldData"].Value<JObject>();

                            foreach (var __NewPair in __NewData)
                            {
                                foreach (var __OldPair in __OldData)
                                {
                                    if (__NewPair.Key == __OldPair.Key && __NewPair.Value.ToString() != __OldPair.Value.ToString())
                                    {
                                        if (__NewPair.Key == "State")
                                        {
                                            long __UserID = __NewData["ID"].ToObject<long>();
                                            cUserEntity __UserEntity = __DataService.Database.GetEntityByID<cUserEntity>(__UserID);
                                            __UserEntity.State = __NewPair.Value.ToObject<int>();
                                            __UserEntity.Save();

                                            WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                                        }
                                    }
                                }
                            }
                        });
                    }
                    else
                    {
                        WebGraph.ActionGraph.NoPermissionAction.Action(_Controller);
                    }
                }
                else
                {
                    WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
                }
            }
        }

        public override cQuery<cUserEntity> ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData, cDataSourceReadEmptyOptions _Options)
        {

            IDataService __DataService = DataServiceManager.GetDataService();

               return (cQuery<cUserEntity>)__DataService.Database.Query<cUserEntity>(() => m_UserAlias, UserDataManager.GetAllUserListQuery());
        }

        public override Action<dynamic> ToDynamicAction(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            return new Action<dynamic>(__Item =>
            {
                __Item.CreateDate = ((DateTime)__Item.CreateDate).ToString("dd.MM.yyyy HH:mm");
                __Item.UpdateDate = ((DateTime)__Item.UpdateDate).ToString("dd.MM.yyyy HH:mm");
                RoleIDs __RoleId = RoleIDs.GetByID((int)__Item.RoleID, null);
                __Item.RoleID = App.Handlers.LanguageHandler.GetWordValue(_Controller.ClientSession.Language, (__RoleId.Name));


            });
        }

        public override cQuery<cUserEntity> SelectedColumns(cQuery<cUserEntity> _Query)
        {
            return _Query.SelectAll();
            /// Burası örnek olsun diye yapıldı gerksiz bir kod fakat örnek olması bakımından önemli
            /*return _Query
				.SelectAliasAllColumns<cUserEntity>(() => m_UserAlias)
				.Case("TestSelect").When().Operand(__Item => __Item.ID).Le(5).Then().SelectValue("Küçük Eşit 5")
				.Else().SelectValue("Büyük 5")
				.ToQuery();*/
        }

        public override ESortDirectionTypes DefaultDirection()
        {
            return ESortDirectionTypes.Descending;
        }

        public override string DefaultDirectionColumn()
        {
            return "UpdateDate";
        }
    }
}
