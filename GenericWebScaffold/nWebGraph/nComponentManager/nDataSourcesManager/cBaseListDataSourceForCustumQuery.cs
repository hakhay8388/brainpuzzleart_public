using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nQuery.nResult;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.
    nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nFilter;
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
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager
{
    public abstract class
        cBaseListDataSourceForCustumQuery<TEntity, TBaseDataSourceReadOptions> : cBaseListDataSource<TEntity>
        where TEntity : cBaseEntity
        where TBaseDataSourceReadOptions : cBaseDataSourceReadOptions
    {
        public cBaseListDataSourceForCustumQuery(
            DataSourceIDs _DataSourceID
            , cApp _App
            , cWebGraph _WebGraph
            , IDataServiceManager _DataServiceManager
            , cDataSourceManager _DataSourceManager
            , cDataSourceDataManager _DataSourceDataManager
        )
            : base(_DataSourceID, _App, _WebGraph, _DataServiceManager, _DataSourceManager, _DataSourceDataManager)
        {
        }

        public cBaseDataSourceObject CreateDataSourceObject(
            string _FieldName,
            bool _Calculated = false,
            ColumnTypeIDs _Type = null,
            string _Title = null,
            int _OrderFromLeft = 1,
            bool _Visible = true,
            bool _ElasticSearch = false,
            ColumnEditableIDs _Editable = null,
            bool _Removable = false,
            bool _Readonly = false,
            ILookupDataSource _LookUpDataSource = null,
            bool _TranslateValue = false,
            int _Width = 0
        )
        {
            cBaseDataSourceObject __Props = new cBaseDataSourceObject()
            {
                Calculated = _Calculated,
                Editable = (_Editable == null ? ColumnEditableIDs.Never.Code : _Editable.Code),
                LookUpDataSource = _LookUpDataSource,
                ElasticSearch = _ElasticSearch,
                Readonly = _Readonly,
                Removable = _Removable,
                TranslateValue = _TranslateValue,
                Visible = _Visible,
                Title = _Title,
                FieldName = _FieldName,
                Type = (_Type == null ? ColumnTypeIDs.String.Code : _Type.Code),
                OrderFromLeft = _OrderFromLeft,
                Width = _Width
            };
            return __Props;
        }

        public List<cBaseDataSourceObject> GetFieldList()
        {
            List<cBaseDataSourceObject> __Result = new List<cBaseDataSourceObject>();

            List<string> __Fields = GetFieldNames();

            Dictionary<string, cBaseDataSourceObject> __FieldTypes = GetFieldTypes();

            /* for (int i = 0; i < __Fields.Count; i++)
             {
                 if (__FieldTypes.ContainsKey(__Fields[i]))
                 {
                     __Result.Add(__FieldTypes[__Fields[i]]);
                 }
                 else
                 {
                     __Result.Add(CreateDataSourceObject(__Fields[i], false, ColumnTypeIDs.String, null,  i, true, false, ColumnEditableIDs.Never, false, true, null, false));
                 }
             }*/

            foreach (var __Item in __FieldTypes)
            {
                if (!__Result.Contains(__Item.Value))
                {
                    __Result.Add(__Item.Value);
                }
            }

            return __Result;
        }

        public override void SynchronizeColumnNames()
        {
            List<cDataSourceColumnEntity> __ColumnList = DataSourceDataManager.GetDataSourceColumns(DataSourceID);
            List<cDataSourceColumnEntity> __UnExistList = new List<cDataSourceColumnEntity>();

            List<cBaseDataSourceObject> __FieldNames = GetFieldList();

            foreach (var __Column in __ColumnList)
            {
                if (__FieldNames.Find(__Item =>
                    (DataSourceID.Code + "." + __Item.FieldName) == __Column.ColumnName && !__Item.Calculated) == null)
                {
                    __UnExistList.Add(__Column);
                }
            }

            DataSourceDataManager.DeleteColumnFromDataSourceAndRoles(__UnExistList);
            foreach (var __Item in __FieldNames)
            {
                if (!__Item.Calculated)
                {
                    DataSourceDataManager.AddColumnToDataSource(DataSourceID,
                        (DataSourceID.Code + "." + __Item.FieldName));
                }
            }
        }

        public List<string> GetFieldNames()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cQuery<TEntity> __TempQuery = (cQuery<TEntity>)__DataService.Database.Query<TEntity>();
            SelectedColumns(__TempQuery);

            List<string> __FieldList = __TempQuery.GetFieldNames();
            return __FieldList;
        }

        public List<cBaseDataSourceObject> GetFieldListForMyPermission(IController _Controller)
        {
            List<cBaseDataSourceObject> __FullFieldList = GetFieldList();
            if (DataSourceID.IsPublic)
            {
                __FullFieldList.ForEach(__Item =>
                {
                    if (__Item.LookUpDataSource != null &&
                        typeof(ILookupDataSource).IsAssignableFrom(__Item.LookUpDataSource.GetType()))
                    {
                        __Item.LookUpDataSource =
                            ((ILookupDataSource)__Item.LookUpDataSource).ToLookUpObject(null, _Controller, null);
                    }
                });

                __FullFieldList = __FullFieldList.OrderBy(_Item => _Item.OrderFromLeft).ToList();
                return __FullFieldList;
            }
            else
            {
                List<cDataSourceColumnEntity> __Colums = DataSourceDataManager.GetDataSourceColumnsByRoleAndDataSourceID(_Controller.ClientSession.User.Actor.GetValue().Roles.ToList(), DataSourceID);


                __FullFieldList.ForEach(__Item =>
                {
                    if (__Item.LookUpDataSource != null &&
                        typeof(ILookupDataSource).IsAssignableFrom(__Item.LookUpDataSource.GetType()))
                    {
                        __Item.LookUpDataSource =
                            ((ILookupDataSource)__Item.LookUpDataSource).ToLookUpObject(null, _Controller, null);
                    }
                });


                List<cBaseDataSourceObject> __Result = new List<cBaseDataSourceObject>();

                foreach (cDataSourceColumnEntity __ColumItem in __Colums)
                {
                    cBaseDataSourceObject __Item = __FullFieldList.Find(__Item =>
                        (DataSourceID.Code + "." + __Item.FieldName) == __ColumItem.ColumnName);
                    if (__Item != null)
                    {
                        __Result.Add(__Item);
                    }
                }

                __FullFieldList.ForEach(__Item =>
                {
                    if (__Item.Calculated)
                    {
                        __Result.Add(__Item);
                    }
                });
                __Result = __Result.OrderBy(_Item => _Item.OrderFromLeft).ToList();
                return __Result;
            }
        }


        public abstract Dictionary<string, cBaseDataSourceObject> GetFieldTypes();

        //public abstract cQuery<TEntity> SelectedColumns(cQuery<TEntity> _Query);

        public virtual cQuery<TEntity> SelectedColumns(cQuery<TEntity> _Query)
        {
            return _Query.SelectAll();
        }

        public abstract ESortDirectionTypes DefaultDirection();

        public abstract string DefaultDirectionColumn();

        public virtual List<int> GetPageSizes()
        {
            List<int> __PageSizes = new List<int>();
            __PageSizes.Add(5);
            __PageSizes.Add(10);
            __PageSizes.Add(20);
            return __PageSizes;
        }


        public abstract cQuery<TEntity> ReadData(cListenerEvent _ListenerEvent, IController _Controller,
            cDataSource_ReadCommandData _ReceivedData, TBaseDataSourceReadOptions _Options);

        public virtual Action<dynamic> ToDynamicAction(cListenerEvent _ListenerEvent, IController _Controller,
            cDataSource_ReadCommandData _ReceivedData)
        {
            return null;
        }

        public override void ReceiveDataSource_CreateData(cListenerEvent _ListenerEvent, IController _Controller,
            cDataSource_CreateCommandData _ReceivedData)
        {
            throw new NotImplementedException();
        }

        public override void ReceiveDataSource_DeleteData(cListenerEvent _ListenerEvent, IController _Controller,
            cDataSource_DeleteCommandData _ReceivedData)
        {
            throw new NotImplementedException();
        }

        public override void ReceiveDataSource_GetMetaDataData(cListenerEvent _ListenerEvent, IController _Controller,
            cDataSource_GetMetaDataCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined || DataSourceID.IsPublic)
            {
                List<cBaseDataSourceObject> __Result = GetFieldListForMyPermission(_Controller);

                __Result.ForEach(__Item =>
                {
                    /*
                    ----Visible False Column İsmini setlenecek
                    */
                    __Item.Title =
                        _Controller.GetWordValue((__Item.Title.IsNullOrEmpty() ? __Item.FieldName : __Item.Title));
                });
                __Result = __Result.OrderBy(__Item => __Item.OrderFromLeft).ToList();

                WebGraph.ActionGraph.ResultListAction.Action(_Controller,
                    new nWebApiGraph.nActionGraph.nActions.nResultListAction.cResultListProps()
                        { ResultList = __Result, Page = 1, Total = __Result.Count });

                List<int> __PageSizes = GetPageSizes();
                WebGraph.ActionGraph.ResultItemAction.Action(_Controller,
                    new cResultItemProps() { Item = new { PageSizes = __PageSizes } });
            }
            else
            {
                WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
            }
        }


        public override void ReceiveDataSource_ReadData(cListenerEvent _ListenerEvent, IController _Controller,
            cDataSource_ReadCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined || DataSourceID.IsPublic)
            {
                TBaseDataSourceReadOptions __Option = null;
                if (_ReceivedData.Options != null)
                {
                    __Option = ((JObject)_ReceivedData.Options).ToObject<TBaseDataSourceReadOptions>();
                }
                else
                {
                    __Option = (TBaseDataSourceReadOptions)typeof(TBaseDataSourceReadOptions).CreateInstance();
                }

                List<cDataSourcePermissionEntity> __Permissions = null;
                if (!DataSourceID.IsPublic)
                {
                    __Permissions = DataSourceDataManager.GetDataSourceInRoleByDataSourceID(_Controller.ClientSession.User.Actor.GetValue().Roles.ToList(), DataSourceID);
                }

                if (DataSourceID.IsPublic || __Permissions.Find(__Item => __Item.CanRead) != null)
                {
                    cQuery<TEntity> __Query = ReadData(_ListenerEvent, _Controller, _ReceivedData, __Option);
                    __Query = SelectedColumns(__Query);

                    IDataService __DataService = DataServiceManager.GetDataService();
                    TEntity __UniqueDataSourceAlias = null;

                    __Query = (cQuery<TEntity>)__DataService.Database.Query<TEntity>(() => __UniqueDataSourceAlias,
                        __Query);
                    __Query = __Query.SelectAll();


                    cBaseFilter<TEntity, TEntity> __Filter = null;

                    if (_ReceivedData.Filters.Count > 0)
                    {
                        if (__Query.Filters.Count > 0)
                        {
                            __Filter = __Query.Where()
                                .And.PrOpen;
                        }
                        else
                        {
                            __Filter = __Query.Where()
                                .PrOpen;
                        }


                        IBaseFilterForOperators<TEntity, TEntity> __Operand = null;

                        for (int i = 0; i < _ReceivedData.Filters.Count; i++)
                        {
                            cFilterItem __Item = _ReceivedData.Filters[i];
                            ColumnTypeIDs __ColumnTypeIDs =
                                ColumnTypeIDs.GetByCode(__Item.column.type, ColumnTypeIDs.String);
                            if (__ColumnTypeIDs.ID == ColumnTypeIDs.String.ID)
                            {
                                if (__Operand != null)
                                {
                                    __Operand = __Operand.And.Operand(__Item.column.field)
                                        .Like("%" + __Item.column.tableData.filterValue + "%");
                                }
                                else
                                {
                                    __Operand = __Filter.Operand(__Item.column.field)
                                        .Like("%" + __Item.column.tableData.filterValue + "%");
                                }
                            }
                            else if (__ColumnTypeIDs.ID == ColumnTypeIDs.Numeric.ID)
                            {
                                try
                                {
                                    int __Value = Convert.ToInt32(__Item.column.tableData.filterValue);
                                    if (__Operand != null)
                                    {
                                        __Operand = __Operand.And.Operand(__Item.column.field).Eq(__Value);
                                    }
                                    else
                                    {
                                        __Operand = __Filter.Operand(__Item.column.field).Eq(__Value);
                                    }
                                }
                                catch (Exception _Ex)
                                {
                                    App.Loggers.CoreLogger.LogError(_Ex);
                                }
                            }
                            else if (__ColumnTypeIDs.ID == ColumnTypeIDs.Boolean.ID)
                            {
                                try
                                {
                                    bool __Value = Convert.ToBoolean(__Item.column.tableData.filterValue);
                                    if (__Operand != null)
                                    {
                                        __Operand = __Operand.And.Operand(__Item.column.field).Eq(__Value);
                                    }
                                    else
                                    {
                                        __Operand = __Filter.Operand(__Item.column.field).Eq(__Value);
                                    }
                                }
                                catch (Exception _Ex)
                                {
                                    App.Loggers.CoreLogger.LogError(_Ex);
                                }
                            }
                            else if (__ColumnTypeIDs.ID == ColumnTypeIDs.Date.ID ||
                                     __ColumnTypeIDs.ID == ColumnTypeIDs.Datetime.ID ||
                                     __ColumnTypeIDs.ID == ColumnTypeIDs.Time.ID)
                            {
                                try
                                {
                                    DateTime __Value = Convert.ToDateTime(__Item.column.tableData.filterValue);
                                    if (__Operand != null)
                                    {
                                        __Operand = __Operand.And.Operand(__Item.column.field).Eq(__Value);
                                    }
                                    else
                                    {
                                        __Operand = __Filter.Operand(__Item.column.field).Eq(__Value);
                                    }
                                }
                                catch (Exception _Ex)
                                {
                                    App.Loggers.CoreLogger.LogError(_Ex);
                                }
                            }
                            else if (__ColumnTypeIDs.ID == ColumnTypeIDs.Currency.ID)
                            {
                                try
                                {
                                    decimal __Value = Convert.ToDecimal(__Item.column.tableData.filterValue);
                                    if (__Operand != null)
                                    {
                                        __Operand = __Operand.And.Operand(__Item.column.field).Eq(__Value);
                                    }
                                    else
                                    {
                                        __Operand = __Filter.Operand(__Item.column.field).Eq(__Value);
                                    }
                                }
                                catch (Exception _Ex)
                                {
                                    App.Loggers.CoreLogger.LogError(_Ex);
                                }
                            }
                        }

                        if (__Operand != null)
                        {
                            __Operand = __Operand.PrClose;
                        }
                        else
                        {
                            __Operand = __Filter.UnmanagedCondition("1=1");
                        }
                    }


                    List<cBaseDataSourceObject> __Field = GetFieldList();

                    if (!_ReceivedData.Search.IsNullOrEmpty())
                    {
                        if (__Query.Filters.Count > 0)
                        {
                            __Filter = __Query.Where()
                                .And.PrOpen;
                        }
                        else
                        {
                            __Filter = __Query.Where()
                                .PrOpen;
                        }

                        IBaseFilterForOperators<TEntity, TEntity> __Operand = null;

                        for (int i = 0; i < __Field.Count; i++)
                        {
                            cBaseDataSourceObject __Item = __Field[i];
                            if (!__Item.Calculated)
                            {
                                ColumnTypeIDs __Type = ColumnTypeIDs.GetByCode(__Item.Type, ColumnTypeIDs.String);

                                if (__Type.ID == ColumnTypeIDs.String.ID)
                                {
                                    if (__Operand != null)
                                    {
                                        __Operand = __Operand.Or.Operand(__Item.FieldName)
                                            .Like("%" + _ReceivedData.Search + "%");
                                    }
                                    else
                                    {
                                        __Operand = __Filter.Operand(__Item.FieldName)
                                            .Like("%" + _ReceivedData.Search + "%");
                                    }
                                }
                                else if (__Type.ID == ColumnTypeIDs.Numeric.ID)
                                {
                                    try
                                    {
                                        int __Value = Convert.ToInt32(_ReceivedData.Search);

                                        if (__Operand != null)
                                        {
                                            __Operand = __Operand.Or.Operand(__Item.FieldName).Eq(__Value);
                                        }
                                        else
                                        {
                                            __Operand = __Filter.Operand(__Item.FieldName).Eq(__Value);
                                        }
                                    }
                                    catch (Exception _Ex)
                                    {
                                        App.Loggers.CoreLogger.LogError(_Ex);
                                    }
                                }
                                else if (__Type.ID == ColumnTypeIDs.Boolean.ID)
                                {
                                    try
                                    {
                                        bool __Value = Convert.ToBoolean(_ReceivedData.Search);

                                        if (__Operand != null)
                                        {
                                            __Operand = __Operand.Or.Operand(__Item.FieldName).Eq(__Value);
                                        }
                                        else
                                        {
                                            __Operand = __Filter.Operand(__Item.FieldName).Eq(__Value);
                                        }
                                    }
                                    catch (Exception _Ex)
                                    {
                                        App.Loggers.CoreLogger.LogError(_Ex);
                                    }
                                }
                                else if (__Type.ID == ColumnTypeIDs.Date.ID || __Type.ID == ColumnTypeIDs.Datetime.ID ||
                                         __Type.ID == ColumnTypeIDs.Time.ID)
                                {
                                    try
                                    {
                                        if (DateTime.TryParse(_ReceivedData.Search, out DateTime __DateTime))
                                        {
                                            DateTime __Value = Convert.ToDateTime(_ReceivedData.Search);
                                            if (__Operand != null)
                                            {
                                                __Operand = __Operand.Or.Operand(__Item.FieldName).Eq(__Value);
                                            }
                                            else
                                            {
                                                __Operand = __Filter.Operand(__Item.FieldName).Eq(__Value);
                                            }
                                        }
                                      
                                    }
                                    catch (Exception _Ex)
                                    {
                                        App.Loggers.CoreLogger.LogError(_Ex);
                                    }
                                }
                                else if (__Type.ID == ColumnTypeIDs.Currency.ID)
                                {
                                    try
                                    {
                                        decimal __Value = Convert.ToDecimal(_ReceivedData.Search);
                                        if (__Operand != null)
                                        {
                                            __Operand = __Operand.Or.Operand(__Item.FieldName).Eq(__Value);
                                        }
                                        else
                                        {
                                            __Operand = __Filter.Operand(__Item.FieldName).Eq(__Value);
                                        }
                                    }
                                    catch (Exception _Ex)
                                    {
                                        App.Loggers.CoreLogger.LogError(_Ex);
                                    }
                                }
                            }
                        }

                        if (__Operand != null)
                        {
                            __Operand = __Operand.PrClose;
                        }
                        else
                        {
                            __Operand = __Filter.UnmanagedCondition("1=1");
                        }
                    }


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
                    else if (DefaultDirectionColumn() != "")
                    {
                        if (DefaultDirection().ID == -1 || DefaultDirection().ID == 0)
                        {
                            __Query.OrderBy().Asc(DefaultDirectionColumn());
                        }
                        else
                        {
                            __Query.OrderBy().Desc(DefaultDirectionColumn());
                        }
                    }

                    int __Start = (_ReceivedData.Page * _ReceivedData.PageSize) + 1;
                    int __End = (_ReceivedData.Page * _ReceivedData.PageSize) + _ReceivedData.PageSize;

                    __Query = __Query.Take(__Start, __End, "TotalRecordCount");


                    cSql __Sql = __Query.ToSql();
                    cResultList<List<dynamic>> __Detail = __Query.ToDynamicObjectListInResultList("TotalRecordCount",
                        ToDynamicAction(_ListenerEvent, _Controller, _ReceivedData));

                    List<cBaseDataSourceObject> __FieldList = GetFieldListForMyPermission(_Controller);

                    TranslateObject(_Controller, __Detail.ResultList, __FieldList);

                    WebGraph.ActionGraph.ResultListAction.Action(_Controller,
                        new nWebApiGraph.nActionGraph.nActions.nResultListAction.cResultListProps()
                        {
                            ResultList = __Detail.ResultList, Total = __Detail.TotalRecordCount,
                            Page = _ReceivedData.Page
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
}