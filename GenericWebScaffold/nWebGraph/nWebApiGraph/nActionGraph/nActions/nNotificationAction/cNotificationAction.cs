﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using Base.Core.nApplication;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction
{
    public class cNotificationAction : cBaseActionWithProps<cNotificationProps>
    {

        public cNotificationAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.Notification)
        {
        }
    }
}
