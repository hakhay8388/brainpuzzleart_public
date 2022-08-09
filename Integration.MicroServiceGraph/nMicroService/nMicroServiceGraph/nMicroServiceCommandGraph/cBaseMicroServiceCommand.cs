using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Base.Core.nCore;
using Base.Core.nApplication;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceIDs;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions;
using Integration.MicroServiceGraph.nMicroService;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph
{
    public abstract class cBaseMicroServiceCommand<TActionProps> : cCoreObject, IMicroServiceCommand
		where TActionProps : cMicroServiceBaseProps
	{
		public MicroServiceIDs MicroServiceID { get; set; }
		public IMicroService MicroService { get; set; }

		public cBaseMicroServiceCommand(cApp _App, IMicroService _MicroService, MicroServiceIDs _MicroServiceID)
			: base(_App)
		{
			MicroServiceID = _MicroServiceID;
			MicroService = _MicroService;
		}

		public abstract void Interpret(cMicroServiceApi _MicroServiceApi, JToken _JsonObject);
	}

}
