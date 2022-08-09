using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public interface IActionWithProps<TActionProps> where TActionProps : IActionProps
    {
        void Action(IController _Controller, TActionProps _ActionProps, List<cSession> _SignalSessions = null, bool _InstantSend = false);
    }
}
