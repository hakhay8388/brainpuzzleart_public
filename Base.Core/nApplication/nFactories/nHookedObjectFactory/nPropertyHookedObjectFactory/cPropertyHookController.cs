using Base.Core.nApplication;
using Base.Core.nApplication.nCoreLoggers;
//using Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Core.nApplication.nFactories.nHookedObjectFactory.nPropertyHookedObjectFactory
{
    public abstract class cPropertyHookController : cCoreObject
    {
        public cPropertyHookController()
            : base(null)
        {
        }
        public abstract void HookedFuction(object _Instance, string _PropertyName, object _PropertyInner);
    }
}
