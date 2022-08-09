using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions
{
    public interface IAction
    {
        ActionIDs ActionID { get; set; }
        cWebGraph WebGraph { get; set; }
    }
}
