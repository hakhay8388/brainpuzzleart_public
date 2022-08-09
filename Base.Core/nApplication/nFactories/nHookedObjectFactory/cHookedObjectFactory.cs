using Base.Core.nApplication.nCoreLoggers;
//using Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Base.Core.nApplication.nFactories.nHookedObjectFactory.nPropertyHookedObjectFactory;
using Base.Core.nAttributes;
using Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Core.nApplication.nFactories.nHookedObjectFactory
{
    public class cHookedObjectFactory : cCoreObject
    {
        public cPropertyHookedObjectFactory PropertyHookedObjectFactory { get; set; }
        public cHookedObjectFactory(cApp _App)
            :base(_App)
        {
            PropertyHookedObjectFactory = new cPropertyHookedObjectFactory(_App);
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cHookedObjectFactory>(this);
        }
    }
}
