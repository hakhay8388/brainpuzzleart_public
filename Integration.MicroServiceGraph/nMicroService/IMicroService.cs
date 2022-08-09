using Base.Communication.nCommunicationService.nCommunicationComponents.nPublisherAndSubscriber.nPublisher.nSubscriber;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceListenerGraph;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.MicroServiceGraph.nMicroService
{
	public interface IMicroService
	{
		cMicroServiceActionGraph MicroServiceActionGraph { get; set; }
		cMicroServiceCommandGraph MicroServiceCommandGraph { get; set; }
		cMicroServiceListenerGraph MicroServiceListenerGraph { get; set; }
		void ReceivePublishedData(cSubscriberNode _SubscriberNode, string _Message);
		void BroadcastMessage(cMicroServiceApi _MicroServiceApi);

		JArray ActionMessage(string _Ip, string _Port, cMicroServiceApi _MicroServiceApi);
	}
}
