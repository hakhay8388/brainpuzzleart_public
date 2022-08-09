using Base.Core.nApplication;
using Base.Core.nApplication.nBootstrapper;
using Base.Core.nApplication.nConfiguration;
using Base.Core.nApplication.nCoreLoggers;
using Base.Core.nApplication.nFactories;
using Base.Core.nHandlers;
using Base.Core.nUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Core.nCore
{
    public abstract class cCoreObject 
    {
        public cApp App { get; set; }

        public cCoreObject(cApp _App)
        {
            App = _App;
        }

        public virtual void Init()
        {
        }

        public new Type GetType()
        {
            Type __Type = base.GetType();
            if (__Type.FullName.Contains("__Proxy__"))
            {
                __Type = __Type.BaseType;
            }
            return __Type;
        }
    }
}
