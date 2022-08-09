using Base.Communication.nConfiguration;
using Base.Core.nApplication;
using Base.Core.nCore;
using Integration.Managers.nConfiguration;
using Integration.MicroServiceGraph.nConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Managers.nManagers
{
    public class cManagersContext: cCoreServiceContext
    {
        public cManagersConfiguration Configuration { get { return App.Cfg<cManagersConfiguration>(); } }
        public cManagersContext(cApp _App)
            :base(_App)
        {
        }
    }
}
