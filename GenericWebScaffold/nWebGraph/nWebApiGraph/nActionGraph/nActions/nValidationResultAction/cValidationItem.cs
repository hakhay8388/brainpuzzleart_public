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
    public class cValidationItem
    {
        public virtual string FieldName { get; set; }
        public virtual bool Success { get; set; }
		public virtual string Message { get; set; }
	}
}
