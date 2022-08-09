

using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceNotificationAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph.nMicroServiceCommands.nMicroServiceNotificationCommand
{
    public interface IMicroServiceNotificationReceiver : IMicroServiceReceiver
    {
		void ReceiveMicroServiceNotificationData(cMicroServiceListenerEvent _ListenerEvent, cMicroServiceApi _MicroServiceApi, cMicroServiceNotificationProps _ReceivedData);
	}
}
