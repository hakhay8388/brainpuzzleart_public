using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAndRunAction
{
    public class cShowMessageAndRunCommandAction : cBaseMessageAction
    {
        public cShowMessageAndRunCommandAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.ShowMessageAndRunCommand)
        {
        }

        public override void Action(IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            JArray __Array = _Controller.ActionJson;
            _Controller.ActionJson = new JArray();
            JObject __JsonObject = DesignAction(_Controller, _MessageProps);
            __JsonObject["RunAfterCommand"] = __Array;
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }


        public void ErrorAction(IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            _MessageProps.ColorType = EColorTypes.Error;
            _MessageProps.FirstButtonColorType = EColorTypes.Error;

            JArray __Array = _Controller.ActionJson;
            _Controller.ActionJson = new JArray();
            JObject __JsonObject = DesignAction(_Controller, _MessageProps);
            __JsonObject["RunAfterCommand"] = __Array;
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }

        public void WarningAction(IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            _MessageProps.ColorType = EColorTypes.Warning;
            _MessageProps.FirstButtonColorType = EColorTypes.Warning;

            JArray __Array = _Controller.ActionJson;
            _Controller.ActionJson = new JArray();
            JObject __JsonObject = DesignAction(_Controller, _MessageProps);
            __JsonObject["RunAfterCommand"] = __Array;
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);

        }

        public void SuccessAction(IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            _MessageProps.ColorType = EColorTypes.Success;
            _MessageProps.FirstButtonColorType = EColorTypes.Success;
            
            JArray __Array = _Controller.ActionJson;
            _Controller.ActionJson = new JArray();
            JObject __JsonObject = DesignAction(_Controller, _MessageProps);
            __JsonObject["RunAfterCommand"] = __Array;
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);

        }
    }
}
