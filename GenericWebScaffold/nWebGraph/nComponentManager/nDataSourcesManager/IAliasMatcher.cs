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
    public interface IAliasMatcher<TEntity>
        where TEntity : cBaseEntity
    {
        IDataSourceFieldType<TEntity> DataSourceFieldType { get; }
        void AddSelect(cQuery<TEntity> _SelectionDemonstrator);

        void AddID(List<IAliasMatcher<TEntity>> _IDFieldList);

        void AddJoin(cQuery<TEntity> _Query);

        IBaseFilterForOperators<TEntity, TEntity> AddElasticSearch(IBaseFilterForOperands<TEntity, TEntity> _Where, string _Value);
        public bool IsAliasEqual<TControlAlias>() where TControlAlias : cBaseEntity;
    }
}
