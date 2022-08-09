using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction
{
    public class cValidationResultProps : cBaseProps
    {
        public virtual List<cValidationItem> ValidationItems { get; set; }

		public cValidationResultProps()
		{
			ValidationItems = new List<cValidationItem>();
		}
	}
}
