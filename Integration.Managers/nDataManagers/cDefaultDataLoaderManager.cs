using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nAttributes;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;

namespace Integration.Managers.nDataManagers
{
    [Register(typeof(IDefaultDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager, IDefaultDataLoader
    {
        public cDefaultDataLoaderManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
       
            )

          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

        public void Load(IDataService _DataService)
        {
			Data.GenericWebScaffold.nDataService.nDataManagers.cDefaultDataLoaderManager __DefaultDataLoaderManager = App.Factories.ObjectFactory.ResolveInstance<Data.GenericWebScaffold.nDataService.nDataManagers.cDefaultDataLoaderManager>();
			__DefaultDataLoaderManager.Load(_DataService);
        }
    }
}

