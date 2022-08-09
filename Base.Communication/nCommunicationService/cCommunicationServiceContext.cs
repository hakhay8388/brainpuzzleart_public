using Base.Communication.nConfiguration;
using Base.Core.nApplication;
using Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Communication.nCommunicationService
{
    public class cCommunicationServiceContext: cCoreServiceContext
    {
        public cCommunicationConfiguration Configuration { get { return App.Cfg<cCommunicationConfiguration>(); } }
        public cCommunicationServiceContext(cApp _App)
            :base(_App)
        {
        }
    }
}
