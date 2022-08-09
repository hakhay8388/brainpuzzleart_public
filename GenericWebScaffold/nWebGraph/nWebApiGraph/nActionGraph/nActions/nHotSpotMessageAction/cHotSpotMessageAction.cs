using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction
{
    public class cHotSpotMessageAction : cBaseActionWithProps<cHotSpotProps>, IActionWithProps<cHotSpotProps>
    {

        public cHotSpotMessageAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.HotSpotMessage)
        {
        }

      
    }
}
