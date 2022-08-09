using Base.Core.nApplication;
using Base.Core.nCore;
using Data.GenericWebScaffold.nConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nGlobalDataServices
{
    public class cGlobalDataServiceContext: cCoreServiceContext
    {
        public cGenericWebScaffoldDataConfiguration Configuration { get { return App.Cfg<cGenericWebScaffoldDataConfiguration>(); } }
        public cGlobalDataServiceContext(cApp _App)
            :base(_App)
        {
        }
    }
}
