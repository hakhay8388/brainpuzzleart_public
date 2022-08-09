

using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceNotificationAction;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceTestMessageAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph.nMicroServiceCommands.nMicroServiceTestMessageCommand
{
    public interface IMicroServiceTestMessageReceiver : IMicroServiceReceiver
    {
		void ReceiveMicroServiceTestMessageData(cMicroServiceListenerEvent _ListenerEvent, cMicroServiceApi _MicroServiceApi, cMicroServiceTestMessageProps _ReceivedData);
	}
}
