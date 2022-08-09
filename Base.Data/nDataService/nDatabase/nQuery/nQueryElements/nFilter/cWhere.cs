using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter
{
    public class cWhere<TEntity> : cBaseFilter<TEntity, TEntity>, IWhere, IBaseFilterForOperands<TEntity, TEntity> where TEntity : cBaseEntity
    {
        public cWhere(IQuery _Query)
            : base(_Query)
        {
        }       
    }
}
