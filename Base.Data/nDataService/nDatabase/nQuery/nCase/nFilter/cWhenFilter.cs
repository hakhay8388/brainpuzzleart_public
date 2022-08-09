using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Base.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nCase.nFilter
{
    public class cWhenFilter<TEntity> : cBaseFilter<TEntity, TEntity>, IBaseFilterForOperands<TEntity, TEntity>
        where TEntity : cBaseEntity
    {
        cWhen<TEntity> OwnerQuery { get; set; }
        public cWhenFilter(cWhen<TEntity> _Query)
            : base(_Query)
        {
            OwnerQuery = _Query;
        }

        public override cWhen<TEntity> Then()
        {
            return OwnerQuery;
        }
    }
}
