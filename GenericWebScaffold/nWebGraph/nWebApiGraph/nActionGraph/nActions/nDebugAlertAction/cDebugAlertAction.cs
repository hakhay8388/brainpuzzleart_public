﻿using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction
{
    public class cDebugAlertAction : cBaseActionWithProps<cDebugAlertProps>, IActionWithProps<cDebugAlertProps>
    {

        public cDebugAlertAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.DebugAlert)
        {
        }

		public void ErrorAction(Exception _Ex, IController _Controller, cDebugAlertProps _DebugAlertProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
		{
			App.Loggers.CoreLogger.LogError(_Ex);
			base.Action(_Controller, _DebugAlertProps.SerializeObject(), _SignalSessions, _InstantSend);
		}

		public void ErrorAction(IController _Controller, cDebugAlertProps _DebugAlertProps, List<cSession> _SignalSessions = null, bool _InstantSend = false)
		{
			base.Action(_Controller, _DebugAlertProps.SerializeObject(), _SignalSessions, _InstantSend);
		}
	}
}
