using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction
{
    public class cDoCheckLoginRequestProps : cBaseProps
    {
		public virtual bool IsLogined { get; set; }
	}
}
