using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction
{
    public class cHotSpotMessageAndRunCommandAction : cBaseActionWithProps<cHotSpotProps>, IActionWithProps<cHotSpotProps>
    {
        public cHotSpotMessageAndRunCommandAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.HotSpotMessageAndRunCommand)
        {
        }

        public override void Action(IController _Controller, cHotSpotProps _ActionData, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            JArray __Array = _Controller.ActionJson;
            _Controller.ActionJson = new JArray();

            JObject __JsonObject = _ActionData.SerializeObject();

            __JsonObject["RunAfterCommand"] = __Array;
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }

    }
}
