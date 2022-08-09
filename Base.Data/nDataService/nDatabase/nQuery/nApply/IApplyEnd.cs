using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nApply
{
    public interface IApplyEnd<TEntity>
        where TEntity : cBaseEntity
    {
        cQuery<TEntity> EndApply();
    }
}
