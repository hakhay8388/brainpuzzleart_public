using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Base.Core.nApplication;
using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSuccessResultAction
{
    public class cSuccessResultAction : cBaseAction
    {

        public cSuccessResultAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.SuccessResult)
        {
        }

        public void Action(IController _Controller)
        {
            JObject __JsonObject = new JObject();
            Action(_Controller, __JsonObject, null, false);
        }
    }
}
