using Base.Core.nApplication;
using Base.Core.nCore;
using Integration.MicroServiceGraph.nMicroService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceListenerGraph
{
	public class cBaseMicroServiceListener : cCoreObject, IMicroServiceListener
	{
		public IMicroService MicroService { get; set; }
		public cBaseMicroServiceListener(cApp _App, IMicroService _MicroService)
			: base(_App)
		{
			MicroService = _MicroService;
			MicroService.MicroServiceListenerGraph.ListenerList.Add(this);
		}

		public override void Init()
		{
			MicroService.MicroServiceCommandGraph.ConnectToCommands(this);
		}
	}
}
