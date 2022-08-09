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
    public class cRelationalNumericFieldType<TEntity, TRelationalEntity> : cRelationalBaseFieldType<TEntity, TRelationalEntity>
        where TEntity : cBaseEntity
        where TRelationalEntity : cBaseEntity
    {
        public cRelationalNumericFieldType(cDataSourceFieldTypeProps<TEntity> _Props)
            :base(_Props)
        {
        }

        public override IBaseFilterForOperators<TEntity, TEntity> AddElasticSearch(IBaseFilterForOperands<TEntity, TEntity> _Where, string _Value)
        {
            int __Value = 0;

            if (int.TryParse(_Value, out __Value))
            {
                return _Where.Operand(Props.ColumnName_PropertyExpressions).Eq(__Value);
            }
            return null;
        }
    }
}
