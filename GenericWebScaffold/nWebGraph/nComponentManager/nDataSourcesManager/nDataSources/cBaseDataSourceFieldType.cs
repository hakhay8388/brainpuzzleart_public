using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources
{
    public abstract class cBaseDataSourceFieldType<TEntity, TAlias> : IDataSourceFieldType<TEntity>
        where TEntity : cBaseEntity
        where TAlias : cBaseEntity        
    {
        public cDataSourceFieldTypeProps<TEntity> Props { get; set; }
        public string ColumnName { get; protected set; }

        public Type ColumnType { get; protected set; }
        public IDataSourceFieldType<TEntity> IDField { get; protected set; }

        public cBaseDataSourceFieldType(cDataSourceFieldTypeProps<TEntity> _Props)
        {
            Props = _Props;
            if (_Props.ColumnName_PropertyExpressions != null)
            {
                ColumnName = _Props.OwnerDataSource.App.Handlers.LambdaHandler.GetParamPropName(_Props.ColumnName_PropertyExpressions);
                ColumnType = typeof(TEntity).GetProperty(ColumnName).PropertyType;
            }
            else if (_Props.RelatedColumnName_PropertyExpressions != null)
            {
                ColumnName = _Props.OwnerDataSource.App.Handlers.LambdaHandler.GetParamPropName((Expression<Func<TAlias, object>>)_Props.RelatedColumnName_PropertyExpressions);
                ColumnType = typeof(TAlias).GetProperty(ColumnName).PropertyType;
            }
        }

        public string GetColumnRoot()
        {
            return typeof(TAlias).Name;
        }

        public virtual object ToMetaObject(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaDataCommandData _ReceivedData)
        {
            Type __Type = typeof(TAlias);
            PropertyInfo __PropertyInfo = __Type.GetProperty(ColumnName);
            cMetaObject<TEntity> __MetaObject = new cMetaObject<TEntity>(_Controller, (Props.ColumnAs.IsNullOrEmpty() ? ColumnName : Props.ColumnAs), __PropertyInfo.PropertyType.Name, Props);
            __MetaObject.LoadLookUp(_ListenerEvent, _Controller, _ReceivedData, Props);
            return __MetaObject;
        }

        public abstract IBaseFilterForOperators<TEntity, TEntity> AddElasticSearch(IBaseFilterForOperands<TEntity, TEntity> _Where, string _Value);

        public abstract string GetFullName();

        public abstract void AddColumn(cQuery<TEntity> _Query, Expression<Func<TAlias>> _Alias);

        public abstract void AddIDColumn(List<IAliasMatcher<TEntity>> _IDFieldList, Expression<Func<TAlias>> _Alias);

        public abstract void AddJoin(cQuery<TEntity> _Query, Expression<Func<object>> _MainAlias, Expression<Func<TAlias>> _Alias);
        public abstract void Update(long _ID, object _Value);
    }
}
