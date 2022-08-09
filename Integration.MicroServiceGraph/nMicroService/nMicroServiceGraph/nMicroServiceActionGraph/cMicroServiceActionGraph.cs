using Base.Core.nApplication;
using Base.Core.nCore;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceNotificationAction;
using Integration.MicroServiceGraph.nMicroService;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceTestMessageAction;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph
{
    public class cMicroServiceActionGraph : cCoreObject
	{
		public IMicroService MicroService { get; set; }


		public List<IMicroServiceAction> MicroServiceActionList;

        public cMicroServiceNotificationAction NotificationAction { get; set; }

		public cMicroServiceTestMessageAction TestMessageAction { get; set; }

		public cMicroServiceActionGraph(cApp _App, IMicroService _MicroService)
            : base(_App)
        {
			MicroService = _MicroService;
			MicroServiceActionList = new List<IMicroServiceAction>();
        }

        public override void Init()
        {
			NotificationAction = App.Factories.ObjectFactory.ResolveInstance<cMicroServiceNotificationAction>();
			NotificationAction.MicroServiceActionGraph = this;

			TestMessageAction = App.Factories.ObjectFactory.ResolveInstance<cMicroServiceTestMessageAction>();
			TestMessageAction.MicroServiceActionGraph = this;
		}

    }
}