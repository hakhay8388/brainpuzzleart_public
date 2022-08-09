using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager
{
    public class cAliasMatcher<TEntity, TAlias> : IAliasMatcher<TEntity>
        where TAlias : cBaseEntity
        where TEntity : cBaseEntity
    {
        cBaseDataSourceFieldType<TEntity, TAlias> DataSourceField { get; set; }

        public Expression<Func<object>> MainAlias { get; set; }
        public Expression<Func<TAlias>> Alias { get; set; }

        public IDataSourceFieldType<TEntity> DataSourceFieldType => DataSourceField;

        public cAliasMatcher(Expression<Func<object>> _MainAlias,  Expression<Func<TAlias>> _Alias,  cBaseDataSourceFieldType<TEntity, TAlias> _DataSourceField)
        {
            MainAlias = _MainAlias;
            Alias = _Alias;
            DataSourceField = _DataSourceField;
        }

        public bool IsAliasEqual<TControlAlias>()
            where TControlAlias : cBaseEntity
        {
            return (typeof(TAlias) == typeof(TControlAlias));            
        }

        public void AddSelect(cQuery<TEntity> _Query)
        {
            DataSourceField.AddColumn(_Query, Alias);
        }

        public void AddJoin(cQuery<TEntity> _Query)
        {
            DataSourceField.AddJoin(_Query, MainAlias, Alias);
        }
        public IBaseFilterForOperators<TEntity, TEntity> AddElasticSearch(IBaseFilterForOperands<TEntity, TEntity> _Where, string _Value)
        {
            return DataSourceField.AddElasticSearch(_Where, _Value);
        }

        public void AddID(List<IAliasMatcher<TEntity>> _IDFieldList)
        {
            DataSourceField.AddIDColumn(_IDFieldList, Alias);
        }
    }
}
