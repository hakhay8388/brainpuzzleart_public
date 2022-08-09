﻿using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public class cShowMessageAction : cBaseMessageAction
    {
        public cShowMessageAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.ShowMessage)
        {
        }

        public void Action<T>(IController _Controller, cMessageWithRequestObjectProps<T> _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            JObject __JsonObject = DesignAction<T>(_Controller, _MessageProps);
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }



        public override void Action(IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            JObject __JsonObject = DesignAction<object>(_Controller, _MessageProps);
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }

        public void ErrorAction(Exception _Ex, IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
			App.Loggers.CoreLogger.LogError(_Ex);
			_MessageProps.ColorType = EColorTypes.Error;
            _MessageProps.FirstButtonColorType = EColorTypes.Error;
            _MessageProps.CloseRequired = true;
            JObject __JsonObject = DesignAction<object>(_Controller, _MessageProps);
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }

		public void ErrorAction(IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
		{
			_MessageProps.ColorType = EColorTypes.Error;
			_MessageProps.FirstButtonColorType = EColorTypes.Error;
			_MessageProps.CloseRequired = true;
			JObject __JsonObject = DesignAction<object>(_Controller, _MessageProps);
			base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
		}

		public void WarningAction(IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            _MessageProps.ColorType = EColorTypes.Warning;
            _MessageProps.FirstButtonColorType = EColorTypes.Warning;
            _MessageProps.CloseRequired = true;
            JObject __JsonObject = DesignAction<object>(_Controller, _MessageProps);
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }

        public void SuccessAction(IController _Controller, cMessageProps _MessageProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            _MessageProps.ColorType = EColorTypes.Success;
            _MessageProps.FirstButtonColorType = EColorTypes.Success;
            _MessageProps.CloseRequired = true;
            JObject __JsonObject = DesignAction<object>(_Controller, _MessageProps);
            base.Action(_Controller, __JsonObject, _SignalSessions, _InstantSend);
        }
    }
}
