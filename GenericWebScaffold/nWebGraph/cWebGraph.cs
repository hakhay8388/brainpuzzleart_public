using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nApplication;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.nWebGraph.nComponentManager;
using Core.GenericWebScaffold.nWebGraph.nErrorMessageManager;
using Core.GenericWebScaffold.nWebGraph.nNotificationManager;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nValidationGraph;

namespace Core.GenericWebScaffold.nWebGraph
{
    [Register(typeof(cWebGraph), false, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
    public class cWebGraph : cCoreObject
    {
        public cSessionManagerServices SessionManagerServices { get; set; }
        public cSessionManager SessionManager 
        { 
            get
            {
                return SessionManagerServices.GetCurrentSessionManager();
            }
        }
        public cActionGraph ActionGraph { get; set; }
        public cCommandGraph CommandGraph { get; set; }
        public cListenerGraph ListenerGraph { get; set; }
        public cValidationGraph ValidationGraph { get; set; }
        public cComponentManager ComponentManager { get; set; }
        public cNotificationManager NotificationManager { get; set; }

		public cErrorMessageManager ErrorMessageManager { get; set; }


		public cWebGraph(cApp _App)
            :base(_App)
        {
        }

        public override void Init()
        {
            SessionManagerServices = App.Factories.ObjectFactory.ResolveInstance<cSessionManagerServices>();
            ActionGraph = App.Factories.ObjectFactory.ResolveInstance<cActionGraph>();
            CommandGraph = App.Factories.ObjectFactory.ResolveInstance<cCommandGraph>();
            ListenerGraph = App.Factories.ObjectFactory.ResolveInstance<cListenerGraph>();
            ValidationGraph = App.Factories.ObjectFactory.ResolveInstance<cValidationGraph>();
            ComponentManager = (cComponentManager)App.Factories.ObjectFactory.ResolveInstance<IComponentLoader>();
            NotificationManager = App.Factories.ObjectFactory.ResolveInstance<cNotificationManager>();
			ErrorMessageManager = App.Factories.ObjectFactory.ResolveInstance<cErrorMessageManager>();


			SessionManagerServices.Init();
            ActionGraph.Init();
            CommandGraph.Init();
            ListenerGraph.Init();
            ValidationGraph.Init();
            ComponentManager.Init();
            NotificationManager.Init();
		}
    }
}
