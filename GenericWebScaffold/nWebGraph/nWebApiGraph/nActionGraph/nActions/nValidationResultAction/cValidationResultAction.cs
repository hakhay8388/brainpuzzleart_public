using Newtonsoft.Json.Linq;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActionIDs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction
{
    public class cValidationResultAction : cBaseActionWithProps<cValidationResultProps>, IActionWithProps<cValidationResultProps>
    {

        public cValidationResultAction(cApp _App, cWebGraph _WebGraph)
           : base(_App, _WebGraph, ActionIDs.ValidationResult)
        {
        }
    }
}
