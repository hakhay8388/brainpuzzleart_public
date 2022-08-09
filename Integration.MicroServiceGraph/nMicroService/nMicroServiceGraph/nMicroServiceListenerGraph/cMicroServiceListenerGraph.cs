using Base.Core.nApplication;
using Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceListenerGraph
{
    public class cMicroServiceListenerGraph : cCoreObject
	{
        public List<cBaseMicroServiceListener> ListenerList = null;

        public cMicroServiceListenerGraph(cApp _App)
            : base(_App)
        {
            ListenerList = new List<cBaseMicroServiceListener>();
        }

		public override void Init()
		{
		}
	}
}
