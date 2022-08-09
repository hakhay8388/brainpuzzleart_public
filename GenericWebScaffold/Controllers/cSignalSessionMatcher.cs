using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.Controllers
{
    public class cSignalSessionMatcher
    {
        public cSession Session { get; set; }
        public JArray ActionJson { get; set; }
        public cSignalSessionMatcher(cSession _Session)
        {
            Session = _Session;
            ActionJson = new JArray();
        }
    }
}
