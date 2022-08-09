using Base.Core.nApplication.nConfiguration;
using Base.Core.nApplication.nCoreLoggers;
//using Base.Core.nApplication.nCoreLoggers.nMethodCallLogger;
using Base.Core.nApplication.nFactories.nFormFactory;
using Base.Core.nApplication.nFactories.nHookedObjectFactory;
using Base.Core.nApplication.nFactories.nObjectFactory;
using Base.Core.nAttributes;
using Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Core.nApplication.nFactories
{
    public class cFactories : cCoreObject
    {
        public cObjectFactory ObjectFactory { get; set; }
        public cFormFactory FormFactory { get; set; }
        public cHookedObjectFactory HookedObjectFactory { get; set; }
        public cFactories(cApp _App)
            :base(_App)
        {
            ObjectFactory = new cObjectFactory(_App);
            FormFactory = new cFormFactory(_App);
            HookedObjectFactory = new cHookedObjectFactory(_App);
        }

        public override void Init()
        {
            ObjectFactory.Init();
            ObjectFactory.RegisterInstance<cFactories>(this);
            FormFactory.Init();
            HookedObjectFactory.Init();            
        }
    }
}
