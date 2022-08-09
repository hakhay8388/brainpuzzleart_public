using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery.nGenerators;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nGroupBy.nHaving.nOperators
{
    public class cAvg<TOwnerEntity, TEntity, TAlias> : cQueryFilterOperand<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
        where TAlias : cBaseEntity
    {
        public cAvg(cHaving<TOwnerEntity, TEntity, TAlias> _Filter, Expression<Func<TEntity, object>> _PropertyExpression)
            : base(_Filter, _PropertyExpression)
        {
            FullName = _Filter.GroupBy.Query.DefaultAlias;
            FullName += "." + Filter.Query.Database.App.Handlers.LambdaHandler.GetParamPropName<TEntity>(_PropertyExpression);
            FullName = "AVG(" + FullName + ") ";
        }

        public cAvg(cHaving<TOwnerEntity, TEntity, TAlias> _Filter, string _Name)
           : base(_Filter, _Name)
        {
            FullName = _Filter.GroupBy.Query.DefaultAlias; 
            FullName += "." + _Name;
            FullName = "AVG(" + FullName + ") ";
        }

        public cAvg(cHaving<TOwnerEntity, TEntity, TAlias> _Filter)
            : base(_Filter)
        {
            Filter = _Filter;
            FullName = "";
        }
    }
}
