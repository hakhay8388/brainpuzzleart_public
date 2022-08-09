using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery.nGroupBy;
using Base.Data.nDataService.nDatabase.nQuery.nJoins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nOrderByQueryElement
{
    public class cOrderByQueryElement<TEntity> : cBaseQueryElement
        where TEntity : cBaseEntity
    {
       public cBaseQueryElement QueryElement { get; set; }
       public cOrderByQueryElement(IQuery _Query, cBaseQueryElement _BaseQueryElement)
            : base(_Query)
        {
            QueryElement = _BaseQueryElement;
        }

        public override string ToElementString(params object[] _Params)
        {
            return QueryElement.ToElementString();
        }
    }
}
