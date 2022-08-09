using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nGoPageAction
{
    public class cGoPageProps : cBaseProps
    {
        public virtual object Page { get; set; }
        public virtual string Params { get; set; }
    }
}
