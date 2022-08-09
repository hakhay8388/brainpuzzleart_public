using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nQuery.nResult;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetSettingsCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager
{
    public abstract class cBaseListDataSource<TEntity> : cCoreObject, IDataSource
        where TEntity : cBaseEntity
    {
        //public List<IAliasMatcher<TEntity>> IDFieldList { get; set; }
        public List<IAliasMatcher<TEntity>> FieldList { get; set; }
        public DataSourceIDs DataSourceID { get; set; }
        public cDataSourceManager DataSourceManager { get; set; }
        public IDataServiceManager DataServiceManager { get; set; }

        public cDataSourceDataManager DataSourceDataManager { get; set; }

        protected TEntity m_MainAlias = null;

        public cWebGraph WebGraph { get; set; }
        public cBaseListDataSource(
            DataSourceIDs _DataSourceID
            , cApp _App, cWebGraph _WebGraph
            , IDataServiceManager _DataServiceManager
            , cDataSourceManager _DataSourceManager
            , cDataSourceDataManager _DataSourceDataManager
            )
            : base(_App)
        {
            DataSourceID = _DataSourceID;
            DataSourceManager = _DataSourceManager;
            DataServiceManager = _DataServiceManager;
            WebGraph = _WebGraph;
            DataSourceDataManager = _DataSourceDataManager;
            //IDFieldList = new List<IAliasMatcher<TEntity>>();
            FieldList = new List<IAliasMatcher<TEntity>>();
        }


        public IDataSourceFieldType<TEntity> GetIDFieldMain(List<IAliasMatcher<TEntity>> _IDFieldList, Expression<Func<TEntity>> _Alias)
        {
            foreach (var __Item in _IDFieldList)
            {
                if (__Item.IsAliasEqual<TEntity>())
                {
                    return __Item.DataSourceFieldType;
                }
            }

            cDataSourceFieldTypeProps<TEntity> __Props = new cDataSourceFieldTypeProps<TEntity>();
            __Props.OwnerDataSource = this;
            __Props.ColumnName_PropertyExpressions = __Item => __Item.ID;
            __Props.Editable = ColumnEditableIDs.Never;
            __Props.Visible = false;
            __Props.OrderFromLeft = 2;

            cBaseDataSourceFieldType<TEntity, TEntity> __Field = new cMainNumericFieldType<TEntity>(__Props);
            _IDFieldList.Add(new cAliasMatcher<TEntity, TEntity>(() => m_MainAlias.ID, _Alias, __Field));


            return __Field;
        }

        public IDataSourceFieldType<TEntity> GetMappedIDField<TMapEntity, TMapppedEntity>(List<IAliasMatcher<TEntity>> _IDFieldList, Expression<Func<TMapppedEntity>> _Alias)
            where TMapEntity : cBaseEntity
            where TMapppedEntity : cBaseEntity
        {
            foreach (var __Item in _IDFieldList)
            {
                if (__Item.IsAliasEqual<TMapppedEntity>())
                {
                    return __Item.DataSourceFieldType;
                }
            }

            cDataSourceFieldTypeProps<TEntity> __Props = new cDataSourceFieldTypeProps<TEntity>();
            __Props.OwnerDataSource = this;
            __Props.SetRelatedColumnName<TMapppedEntity>(__Item => __Item.ID);
            __Props.Editable = ColumnEditableIDs.Never;
            __Props.Visible = false;
            __Props.OrderFromLeft = 2;
            __Props.InnerJoin = false;
            __Props.ColumnAs = typeof(TMapppedEntity).Name + "_ID";


            cBaseDataSourceFieldType<TEntity, TMapppedEntity> __Field = new cMappedStringFieldType<TEntity, TMapEntity, TMapppedEntity>(__Props);
            _IDFieldList.Add(new cAliasMatcher<TEntity, TMapppedEntity>(() => m_MainAlias.ID, _Alias, __Field));

            return __Field;

        }

        public IDataSourceFieldType<TEntity> GetRelatedIDField<TRelatedEntity>(List<IAliasMatcher<TEntity>> _IDFieldList, Expression<Func<TRelatedEntity>> _Alias)
           where TRelatedEntity : cBaseEntity
        {
            foreach (var __Item in _IDFieldList)
            {
                if (__Item.IsAliasEqual<TRelatedEntity>())
                {
                    return __Item.DataSourceFieldType;
                }
            }

            cDataSourceFieldTypeProps<TEntity> __Props = new cDataSourceFieldTypeProps<TEntity>();
            __Props.OwnerDataSource = this;
            __Props.SetRelatedColumnName<TRelatedEntity>(__Item => __Item.ID);
            __Props.Editable = ColumnEditableIDs.Never;
            __Props.Visible = false;
            __Props.OrderFromLeft = 2;
            __Props.InnerJoin = false;
            __Props.ColumnAs = typeof(TRelatedEntity).Name + "_ID";


            cBaseDataSourceFieldType<TEntity, TRelatedEntity> __Field = new cRelationalNumericFieldType<TEntity, TRelatedEntity>(__Props);
            _IDFieldList.Add(new cAliasMatcher<TEntity, TRelatedEntity>(() => m_MainAlias.ID, _Alias, __Field));

            return __Field;

        }


        public virtual void SynchronizeColumnNames()
        {
            List<cDataSourceColumnEntity> __ColumnList = DataSourceDataManager.GetDataSourceColumns(DataSourceID);
            List<cDataSourceColumnEntity> __UnExistList = new List<cDataSourceColumnEntity>();

            foreach (var __Column in __ColumnList)
            {
                if (FieldList.Find(__Item => __Item.DataSourceFieldType.GetFullName() == __Column.ColumnName) == null)
                {
                    __UnExistList.Add(__Column);
                }
            }
            DataSourceDataManager.DeleteColumnFromDataSourceAndRoles(__UnExistList);

            foreach (var __Item in FieldList)
            {
                DataSourceDataManager.AddColumnToDataSource(DataSourceID, __Item.DataSourceFieldType.GetFullName());
            }
        }

        public List<IAliasMatcher<TEntity>> GetIDList(List<IAliasMatcher<TEntity>> _FieldList)
        {
            List<IAliasMatcher<TEntity>> __IDFieldList = new List<IAliasMatcher<TEntity>>();

            foreach (var __FieldItem in _FieldList)
            {
                if (__IDFieldList.Where(__Item => __FieldItem.DataSourceFieldType.IDField != null && __Item.DataSourceFieldType.GetFullName() == __FieldItem.DataSourceFieldType.IDField.GetFullName()).ToList().Count < 1)
                {
                    __FieldItem.AddID(__IDFieldList);
                }
            }

            return __IDFieldList;
        }

        public void TranslateObject(IController _Controller, List<dynamic> _ItemList, List<IAliasMatcher<TEntity>> _FieldList)
        {

            foreach (var __Item in _ItemList)
            {
                foreach (var __FieldItem in _FieldList)
                {
                    if (__FieldItem.DataSourceFieldType.Props.TranslateValue)
                    {
                        var __DictionaryItem = (IDictionary<string, object>)__Item;
                        object __Value = __DictionaryItem[__FieldItem.DataSourceFieldType.ColumnName];
                        __DictionaryItem[__FieldItem.DataSourceFieldType.ColumnName] = _Controller.GetWordValue(__Value.ToString());
                    }
                }
            }
        }
        public void TranslateObject(IController _Controller, List<dynamic> _ItemList, List<cBaseDataSourceObject> _FieldList)
        {

            foreach (var __Item in _ItemList)
            {
                foreach (var __FieldItem in _FieldList)
                {
                    if (__FieldItem.TranslateValue && __FieldItem.Type != ColumnTypeIDs.Numeric.Code)
                    {
                        var __DictionaryItem = (IDictionary<string, object>)__Item;
                        object __Value = __DictionaryItem[__FieldItem.FieldName];
                        __DictionaryItem[__FieldItem.FieldName] = _Controller.GetWordValue(__Value.ToString());
                    }
                }
            }
        }

        public virtual void AddHardCodedFilters(IController _Controller, cQuery<TEntity> _Query, IBaseFilterForOperands<TEntity, TEntity> _Where)
        {
        }

        public virtual void ReceiveDataSource_ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {

                //JArray __LanguageJObject = JArray.FromObject(_ReceivedData.Filters);
                //Console.WriteLine(__LanguageJObject.ToString());

                List<cDataSourcePermissionEntity> __Permissions = DataSourceDataManager.GetDataSourceInRoleByDataSourceID(_Controller.ClientSession.User.Actor.GetValue().Roles.ToList(), DataSourceID);
                if (__Permissions.Find(__Item => __Item.CanRead) != null)
                {
                    List<cRoleEntity> __List = _Controller.ClientSession.User.Actor.GetValue().Roles.ToList();
                    List<cDataSourceColumnEntity> __Colums = DataSourceDataManager.GetDataSourceColumnsByRoleAndDataSourceID(__List, DataSourceID);


                    ///////////////////// Kullanıcı kolonları /////////////////////////
                    List<IAliasMatcher<TEntity>> __FieldList = new List<IAliasMatcher<TEntity>>();
                    foreach (cDataSourceColumnEntity __ColumItem in __Colums)
                    {
                        IAliasMatcher<TEntity> __Item = FieldList.Find(__Item => __Item.DataSourceFieldType.GetFullName() == __ColumItem.ColumnName);
                        if (__Item != null)
                        {
                            __FieldList.Add(__Item);
                        }
                    }
                    ////////////////////////////////////////////////////////////////


                    /////////////////ID Fieldların eklenmesi //////////////////////////////////

                    List<IAliasMatcher<TEntity>> __IDFieldList = GetIDList(__FieldList);

                    foreach (var __Item in __IDFieldList)
                    {
                        __FieldList.Add(__Item);
                    }
                    ///////////////////////////////////////////////////////////////////////

                    ////////////////////// Seçilen kolonların eklenmesi
                    IDataService __DataService = DataServiceManager.GetDataService();

                    cQuery<TEntity> __Query = (cQuery<TEntity>)__DataService.Database.Query<TEntity>(() => m_MainAlias);
                    foreach (var __Item in __FieldList)
                    {
                        __Item.AddSelect(__Query);
                    }

                    ////////////////////////////////////////////////////////////////

                    ///////////////////// Aynı tobloda bulunanlar sadece birkere Joinlenecek şekilde joinler ekleniyor///////////
                    //////////////////////Fakat ilk bulunan kolonun Inner joinmi Left joinmi olacağı diğerleri içinde geçerli oluyor......////////

                    List<IAliasMatcher<TEntity>> __JoinFieldList = new List<IAliasMatcher<TEntity>>();

                    foreach (var __Item in __FieldList)
                    {
                        if (__Item.DataSourceFieldType.IDField != null)
                        {
                            string __Item1 = __Item.DataSourceFieldType.IDField.GetFullName();
                            if (__JoinFieldList.Where(__JoinItem => __JoinItem.DataSourceFieldType.IDField.GetFullName() == __Item.DataSourceFieldType.IDField.GetFullName()).ToList().Count < 1)
                            {
                                __JoinFieldList.Add(__Item);
                                __Item.AddJoin(__Query);
                            }
                        }
                    }

                    ////////////////////////////////////////////////////////////////////

                    //////////////////Elastik Searchlerin eklenmesi /////////////////////
                    ///
                    IBaseFilterForOperands<TEntity, TEntity> __Where = null;
                    IBaseFilterForOperators<TEntity, TEntity> __Operator = null;
                    if (!_ReceivedData.Search.IsNullOrEmpty())
                    {
                        __Where = __Query.Where();
                        __Where = __Where.PrOpen;

                        for (int i = 0; i < __FieldList.Count; i++)
                        {
                            if (__FieldList[i].DataSourceFieldType.Props.ElasticSearch)
                            {
                                if (__Operator != null)
                                {
                                    __Where = __Operator.Or;
                                }
                                IBaseFilterForOperators<TEntity, TEntity> __TempOperator = __FieldList[i].AddElasticSearch(__Where, _ReceivedData.Search);
                                if (__TempOperator != null)
                                {
                                    __Operator = __TempOperator;
                                }
                            }
                        }
                        __Where = __Where.PrClose;
                    }

                    if (__Where == null)
                    {
                        __Where = __Query.Where();
                        AddHardCodedFilters(_Controller, __Query, __Where);
                    }
                    else
                    {
                        if (__Operator != null)
                        {
                            __Where = __Operator.And;
                            AddHardCodedFilters(_Controller, __Query, __Where);
                        }
                        else
                        {
                            AddHardCodedFilters(_Controller, __Query, __Where);
                        }

                    }



                    //////////////////////////////////////////////////////////////////

                    //////////////////////////Sıralamada bakılacak kolon ////////////////////////////////
                    if (!_ReceivedData.OrderDirection.IsNullOrEmpty() && !_ReceivedData.OrderByField.IsNullOrEmpty())
                    {
                        if (_ReceivedData.OrderDirection == "asc")
                        {
                            __Query.OrderBy().Asc(_ReceivedData.OrderByField);
                        }
                        else
                        {
                            __Query.OrderBy().Desc(_ReceivedData.OrderByField);
                        }
                    }
                    ////////////////////////////////////////////////////////////////////////

                    ///////////////////////////////Sayfalamanın eklenmesi ///////////////

                    int __Start = (_ReceivedData.Page * _ReceivedData.PageSize);
                    int __End = (_ReceivedData.Page * _ReceivedData.PageSize) + _ReceivedData.PageSize;

                    __Query = __Query.Take(__Start, __End, "TotalRecordCount");

                    ///////////////////////////////////////////////////////////////////////

                    cSql __Sql = __Query.ToSql();
                    cResultList<List<dynamic>> __Detail = __Query.ToDynamicObjectListInResultList("TotalRecordCount");


                    TranslateObject(_Controller, __Detail.ResultList, __FieldList);

                    /*  __Query = (cQuery<TEntity>)__DataService.Database.Query<TEntity>(() => m_MainAlias);
                      __Query = __Query.SelectCount();


                      /////////////////////////////// Yukarıdaki sorgunun aynısı fdakat toplam sayıyı almak için çalışıyor /////////////////////////////////////////////
                      __JoinFieldList = new List<IAliasMatcher<TEntity>>();

                      foreach (var __Item in __FieldList)
                      {
                          if (__Item.DataSourceFieldType.IDField != null)
                          {
                              if (__JoinFieldList.Where(__JoinItem => __JoinItem.DataSourceFieldType.IDField.GetFullName() == __Item.DataSourceFieldType.IDField.GetFullName()).ToList().Count < 1)
                              {
                                  __JoinFieldList.Add(__Item);
                                  __Item.AddJoin(__Query);
                              }
                          }
                      }


                      __Where = __Query.Where();
                      __Operator = null;

                      if (!_ReceivedData.Search.IsNullOrEmpty())
                      {
                          __Where = __Where.PrOpen;
                          for (int i = 0; i < __FieldList.Count; i++)
                          {
                              if (__FieldList[i].DataSourceFieldType.Props.ElasticSearch)
                              {
                                  if (__Operator != null)
                                  {
                                      __Where = __Operator.Or;
                                  }
                                  IBaseFilterForOperators<TEntity, TEntity> __TempOperator = __FieldList[i].AddElasticSearch(__Where, _ReceivedData.Search);
                                  if (__TempOperator != null)
                                  {
                                      __Operator = __TempOperator;
                                  }
                              }
                          }
                          __Where = __Where.PrClose;
                      }

                      if (__Where == null)
                      {
                          __Where = __Query.Where();
                          AddHardCodedFilters(_Controller, __Query, __Where);
                      }
                      else
                      {
                          if (__Operator != null)
                          {
                              __Where = __Operator.And;
                              AddHardCodedFilters(_Controller, __Query, __Where);
                          }
                          else
                          {
                              AddHardCodedFilters(_Controller, __Query, __Where);
                          }

                      }

                      int __Count = __Query.ToCount();*/

                    ////////////////////////////////////////////////////////////////////////////////////////////

                    WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nWebApiGraph.nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = __Detail.ResultList, Total = __Detail.TotalRecordCount, Page = _ReceivedData.Page });
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

        public void ReceiveDataSource_GetSettingsData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetSettingsCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                List<cDataSourcePermissionEntity> __Permissions = DataSourceDataManager.GetDataSourceInRoleByDataSourceID(_Controller.ClientSession.User.Actor.GetValue().Roles.ToList(), DataSourceID);
                WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nWebApiGraph.nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = __Permissions, Page = 1, Total = __Permissions.Count });
            }
            else if (DataSourceID.IsPublic)
            {
                WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nWebApiGraph.nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = new List<dynamic>(), Page = 1, Total = 0 });
            }
            else
            {
                WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
            }
        }

        public abstract void ReceiveDataSource_CreateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_CreateCommandData _ReceivedData);

        public IAliasMatcher<TEntity> GetFielByColumnName(string _ColumnName)
        {
            return FieldList.Find(__Item => __Item.DataSourceFieldType.ColumnName == _ColumnName);
        }

        public virtual void ReceiveDataSource_UpdateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_UpdateCommandData _ReceivedData)
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
                                        IAliasMatcher<TEntity> __Field = GetFielByColumnName(__NewPair.Key);
                                        object __Value = __NewPair.Value.ToObject(__Field.DataSourceFieldType.ColumnType);
                                        long __ID = -1;
                                        string __Email = "";
                                        if (__Field.DataSourceFieldType.IDField != null)
                                        {
                                            foreach (var __NewPairIDField in __NewData)
                                            {
                                                if (__NewPairIDField.Key.ToString() == "Email")
                                                {
                                                    __Email = __NewPairIDField.Value.ToString();
                                                }
                                                if (__NewPairIDField.Key.ToString() == __Field.DataSourceFieldType.IDField.ColumnName)
                                                {
                                                    __ID = Convert.ToInt64(__NewPairIDField.Value.ToObject(__Field.DataSourceFieldType.IDField.ColumnType));
                                                    break;
                                                }
                                            }
                                        }
                                        if (__ID > 0)
                                        {
                                            __Field.DataSourceFieldType.Update(__ID, __Value);
                                            WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                                            cMessageProps __MessageProps = new cMessageProps();
                                            __MessageProps.Header = "Test";
                                            __MessageProps.Message = "Test";
                                            __MessageProps.CloseRequired = true;

                                            WebGraph.ActionGraph.ShowMessageAction.Action(_Controller, __MessageProps, WebGraph.SessionManager.GetSessionByEmail(__Email));

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

        public abstract void ReceiveDataSource_DeleteData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_DeleteCommandData _ReceivedData);

        public virtual void ReceiveDataSource_GetMetaDataData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaDataCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                List<cDataSourceColumnEntity> __Colums = DataSourceDataManager.GetDataSourceColumnsByRoleAndDataSourceID(_Controller.ClientSession.User.Actor.GetValue().Roles.ToList(), DataSourceID);

                List<IAliasMatcher<TEntity>> __FieldList = new List<IAliasMatcher<TEntity>>();
                foreach (cDataSourceColumnEntity __ColumItem in __Colums)
                {
                    IAliasMatcher<TEntity> __Item = FieldList.Find(__Item => __Item.DataSourceFieldType.GetFullName() == __ColumItem.ColumnName);
                    if (__Item != null)
                    {
                        __FieldList.Add(__Item);
                    }
                }


                List<IAliasMatcher<TEntity>> __IDFieldList = GetIDList(__FieldList);


                foreach (var __Item in __IDFieldList)
                {
                    __FieldList.Add(__Item);
                }

                __FieldList = __FieldList.OrderBy(__Item => __Item.DataSourceFieldType.Props.OrderFromLeft).ToList();

                List<object> __Result = new List<object>();

                __FieldList.ForEach((_Item) =>
                {
                    __Result.Add(_Item.DataSourceFieldType.ToMetaObject(_ListenerEvent, _Controller, _ReceivedData));
                });

                WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nWebApiGraph.nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = __Result, Page = 1, Total = __Result.Count });

                List<int> __PageSizes = GetPageSizes();
                WebGraph.ActionGraph.ResultItemAction.Action(_Controller, new cResultItemProps() { Item = new { PageSizes = __PageSizes } });
            }
            else
            {
                WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
            }
        }
        public List<int> GetPageSizes()
        {
            List<int> __PageSizes = new List<int>();
            __PageSizes.Add(5);
            __PageSizes.Add(10);
            __PageSizes.Add(20);
            return __PageSizes;
        }
        public int GetDefaultPageSize()
        {
            return 5;
        }
    }
}
