using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetVariableAction
{
    public class cSetVariableAction : cBaseActionWithProps<cVariableProps>
    {

        public cSetVariableAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.SetVariable)
        {
        }
    }
}
