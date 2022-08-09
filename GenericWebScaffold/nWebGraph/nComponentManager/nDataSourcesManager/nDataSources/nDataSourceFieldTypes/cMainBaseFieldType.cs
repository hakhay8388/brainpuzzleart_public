using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes
{
    public abstract class cMainBaseFieldType<TEntity> : cBaseDataSourceFieldType<TEntity, TEntity>
        where TEntity : cBaseEntity
    {
        protected Expression<Func<TEntity>> MainAlias { get; set; }
        public cMainBaseFieldType(cDataSourceFieldTypeProps<TEntity> _Props)
            :base(_Props)
        {
        }

        public override void AddColumn(cQuery<TEntity> _Query, Expression<Func<TEntity>> _Alias)
        {
            MainAlias = _Alias;
            _Query.SelectAliasColumn<TEntity>(_Alias, Props.ColumnName_PropertyExpressions, Props.ColumnAs);
        }

        public override void AddIDColumn(List<IAliasMatcher<TEntity>> _IDFieldList,  Expression<Func<TEntity>> _Alias)
        {
            MainAlias = _Alias;
            IDField = Props.OwnerDataSource.GetIDFieldMain(_IDFieldList, _Alias);
        }

        public override void AddJoin(cQuery<TEntity> _Query, Expression<Func<object>> _MainAlias, Expression<Func<TEntity>> _Alias)
        {
        }

        public override string GetFullName()
        {
            return GetColumnRoot() + "." + ColumnName;
        }

        public override void Update(long _ID, object _Value)
        {
            IDataService __DataService = Props.OwnerDataSource.DataServiceManager.GetDataService();
            TEntity __Entity = __DataService.Database.GetEntityByID<TEntity>(_ID);
            __Entity.GetType().GetProperty(ColumnName).GetSetMethod().Invoke(__Entity, new object[] { _Value });
            __Entity.Save();

        }
    }
}
