using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetClientLanguageAction
{
    public class cSetClientLanguageProps : cBaseProps
    {
        public virtual string LanguageCode { get; set; }
        public virtual JObject Language { get; set; }
    }
}
