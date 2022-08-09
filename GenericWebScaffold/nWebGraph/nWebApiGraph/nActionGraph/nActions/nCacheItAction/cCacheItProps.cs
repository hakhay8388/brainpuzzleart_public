using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCacheItAction
{
    public class cCacheItProps : cBaseProps
    {
        public virtual JArray CacheActionList { get; set; }
    }
}
