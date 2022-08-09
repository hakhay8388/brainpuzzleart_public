using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Base.Core.nCore;
using Base.Core.nApplication;
using Core.GenericWebScaffold.Controllers;
using System.Collections.Generic;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public abstract class cBaseActionWithProps<TActionProps> : 
        cBaseAction, 
        IActionWithProps<TActionProps>
         where TActionProps : IActionProps
    {
        public cBaseActionWithProps(cApp _App, cWebGraph _WebGraph, ActionIDs _ActionID)
            :base(_App, _WebGraph, _ActionID)
        {            
        }
        
        public virtual void Action(IController _Controller, TActionProps _ActionData, List<cSession> _SignalSessions = null, bool _InstantSend = false)
        {
            Action(_Controller, _ActionData.SerializeObject(), _SignalSessions, _InstantSend);
        }

		public virtual void ActionAll(IController _Controller, TActionProps _ActionData)
		{
			ActionAll(_Controller, _ActionData.SerializeObject());
		}

		public virtual void Action(cBaseListener _Listener, TActionProps _ActionData, List<cSession> _SignalSessions)
		{
			InstantAction(_Listener, _ActionData.SerializeObject(), _SignalSessions);
		}

	}
}
