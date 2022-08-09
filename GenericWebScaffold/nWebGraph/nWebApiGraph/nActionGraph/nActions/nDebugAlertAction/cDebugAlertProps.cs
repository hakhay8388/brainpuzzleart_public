using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction
{
    public class cDebugAlertProps : cBaseProps
    {
		public virtual string Header { get; set; }
		public virtual string Message { get; set; }
    }
}
