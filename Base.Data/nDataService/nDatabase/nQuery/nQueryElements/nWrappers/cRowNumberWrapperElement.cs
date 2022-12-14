using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery.nGroupBy;
using Base.Data.nDataService.nDatabase.nQuery.nJoins;
using Base.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber;
using Base.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nWrappers
{
    public class cRowNumberWrapperElement<TEntity> : cBaseQueryElement
        where TEntity : cBaseEntity
    {
        public cRowNumber<TEntity> RowNumber { get; set; }
        public cRowNumberWrapperElement(IQuery _Query, cRowNumber<TEntity> _RowNumber)
            : base(_Query)
        {
            RowNumber = _RowNumber;
        }

        public override string ToElementString(params object[] _Params)
        {
            return RowNumber.Wrap((cSql)_Params[0]);
        }
    }
}
