using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nApplication;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager
{
    [Register(typeof(IComponentLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cComponentManager : cCoreObject, IComponentLoader
    {
        public cDataSourceManager DataSourceManager { get; set; }
        public cComponentManager(cApp _App)
            : base(_App)
        {
        }

        public override void Init()
        {
             DataSourceManager = App.Factories.ObjectFactory.ResolveInstance<cDataSourceManager>();


            DataSourceManager.Init();
        }

        public void Load(IDataService _DataService)
        {
            _DataService.Perform(LoadDefaultPureData);
        }
        public void LoadDefaultPureData()
        {
            DataSourceManager.FirtsRequestInit(); 
        }
    }
}
