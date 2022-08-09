using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCacheItAction
{
    public class cCacheItAction : cBaseActionWithProps<cCacheItProps>, IActionWithProps<cCacheItProps>
    {

        public cCacheItAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.CacheIt)
        {
        }
    }
}
