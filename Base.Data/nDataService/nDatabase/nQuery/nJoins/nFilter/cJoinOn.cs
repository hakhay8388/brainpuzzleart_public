using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nJoins.nFilter
{
    public class cJoinOn<TOwnerEntity, TEntity> : cBaseFilter<TOwnerEntity, TEntity>, IBaseFilterForOperands<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public cJoinOn(IJoin _Query)
            : base(_Query)
        {
        }

        public override cQuery<TOwnerEntity> ToQuery()
        {
            IQuery __Result = (((IJoin)Query).OwnerQuery);
            return ((cQuery<TOwnerEntity>)__Result);
        }
    }
}
