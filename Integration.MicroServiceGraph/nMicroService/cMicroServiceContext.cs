using Base.Communication.nConfiguration;
using Base.Core.nApplication;
using Base.Core.nCore;
using Integration.MicroServiceGraph.nConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService
{
    public class cMicroServiceContext: cCoreServiceContext
    {
        public cMicroServiceConfiguration Configuration { get { return App.Cfg<cMicroServiceConfiguration>(); } }
        public cMicroServiceContext(cApp _App)
            :base(_App)
        {
        }
    }
}
