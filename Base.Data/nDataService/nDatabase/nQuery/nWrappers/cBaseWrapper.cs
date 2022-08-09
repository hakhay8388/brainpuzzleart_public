using Base.Data.nDataService.nDatabase.nCatalog.nRowOperationCatalog;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery.nGenerators;
using Base.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving;
using Base.Data.nDataService.nDatabase.nQuery.nJoins.nFilter;
using Base.Data.nDataService.nDatabase.nQuery.nOrderBy.nAsc;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nGroupByQueryElement;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nWrappers;
using Base.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nWrappers
{
    public abstract class cBaseWrapper<TEntity> where TEntity : cBaseEntity
    {
        public cQuery<TEntity> Query { get; set; }
        public cBaseWrapper(cQuery<TEntity> _Query)
        {
            Query = _Query;
        }

        public abstract string Wrap(cSql _Sql);
    }
}
