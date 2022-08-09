using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nAttributes;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction
{
    public class cMessageWithRequestObjectProps<T> : cBaseMessageProps<T>
    {
        public cMessageWithRequestObjectProps()
            :base()
        {
        }
    }
}
