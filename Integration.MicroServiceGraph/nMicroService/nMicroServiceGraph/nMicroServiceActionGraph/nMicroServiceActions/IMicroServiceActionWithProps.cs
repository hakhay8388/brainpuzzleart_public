﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions
{
    public interface IMicroServiceActionWithProps<TActionProps> where TActionProps : IMicroServiceActionProps
    {
        //void BroadcastAction(cMicroServiceApi _MicroServiceApi, TActionProps _ActionProps);

		void BroadcastAction(TActionProps _ActionProps);
	}
}
