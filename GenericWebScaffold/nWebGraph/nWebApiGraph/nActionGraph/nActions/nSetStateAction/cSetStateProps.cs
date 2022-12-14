using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetStateAction
{
    public class cSetStateProps : cBaseProps
    {
        public virtual string ObjectTypeName { get; set; }
        public virtual string Name { get; set; }
        public virtual object Value { get; set; }
    }
}
