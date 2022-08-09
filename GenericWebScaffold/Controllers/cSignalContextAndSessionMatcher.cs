using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.Controllers
{
    public class cSignalContextAndSessionMatcher
    {
        public int ThreadID { get; set; }
        public List<cSignalSessionMatcher> SignalSessions { get; set; }
        public cSignalContextAndSessionMatcher(int _ThreadID, List<cSignalSessionMatcher> _SignalSessions)
        {
            ThreadID = _ThreadID;
            SignalSessions = _SignalSessions;
        }
    }
}
