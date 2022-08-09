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
    public class cMappedStringFieldType<TEntity, TMapEntity, TMappedEntity> : cMappedBaseFieldType<TEntity, TMapEntity, TMappedEntity>
        where TEntity : cBaseEntity
        where TMapEntity : cBaseEntity
        where TMappedEntity : cBaseEntity
    {

        public cMappedStringFieldType(cDataSourceFieldTypeProps<TEntity> _Props)
            : base(_Props)
        {            
        }

        public override IBaseFilterForOperators<TEntity, TEntity> AddElasticSearch(IBaseFilterForOperands<TEntity, TEntity> _Where, string _Value)
        {
            return _Where.Operand(MappedAlias, RelatedColumn).Like("%" + _Value + "%");
        }
    }
}
