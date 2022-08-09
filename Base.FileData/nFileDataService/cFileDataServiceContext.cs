using Base.Core.nApplication;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.FileData.nConfiguration;


namespace Base.FileData.nFileDataService
{
    public class cFileDataServiceContext : cCoreServiceContext
    {
        public cFileDataConfiguration Configuration { get { return App.Cfg< cFileDataConfiguration>(); } }

        public cFileDataServiceContext(cApp _App)
            :base(_App)
        {
        }
    }
}
