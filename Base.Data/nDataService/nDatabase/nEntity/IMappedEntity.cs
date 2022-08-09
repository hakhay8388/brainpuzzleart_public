using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nEntity
{
    public interface IMappedEntity
    {
        IList ToIList();
        JArray ToJArray();
    }
}
