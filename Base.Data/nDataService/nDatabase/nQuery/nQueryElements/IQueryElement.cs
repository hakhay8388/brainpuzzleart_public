using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nQueryElements
{
    public interface IQueryElement
    {
        IBaseQuery Query { get; set; }
        string ToElementString(params object[] _Params);
    }
}
