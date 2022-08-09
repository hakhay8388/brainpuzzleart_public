using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Core.nApplication;
using Base.Core.nApplication.nBootstrapper;
using Base.Core.nApplication.nConfiguration;
using Base.Core.nApplication.nFactories;
using Base.Core.nApplication.nFactories.nFormFactory;
using Base.Core.nApplication.nFactories.nHookedObjectFactory;
using Base.Core.nAttributes;
using Base.Core.nHandlers;
using Base.Core.nApplication.nCoreLoggers;
//using Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;

namespace Base.Core.nCore
{
    public class cCoreService<TServiceContext> : cCoreObject where TServiceContext : cCoreServiceContext
    {
        public TServiceContext ServiceContext { get; set; }
        public cCoreService(TServiceContext _ServiceContext)
            :base(_ServiceContext.App)
        {
            ServiceContext = _ServiceContext;
        }

    }
}
