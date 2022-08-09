using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using Base.Core.nCore;
using Base.Core.nApplication;
using Core.GenericWebScaffold.Controllers;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph
{
    public abstract class cBaseCommand : cCoreObject
    {
        public CommandIDs CommandID;
        public cWebGraph WebGraph { get; set; }

        public cBaseCommand(cApp _App, cWebGraph _WebGraph, CommandIDs _CommandID)
            : base(_App)
        {
            CommandID = _CommandID;
            WebGraph = _WebGraph;
        }

        public abstract void Interpret(IController _Controller, JToken _JsonObject);
    }

}
