using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction
{
    public class cLogInOutProps : cBaseProps
    {
        public virtual bool LoginState { get; set; }
        public virtual string SessionID { get; set; }
        public virtual dynamic User { get; set; }
    }
}
