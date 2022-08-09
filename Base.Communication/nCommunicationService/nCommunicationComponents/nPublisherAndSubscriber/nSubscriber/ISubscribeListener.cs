using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nPublisherAndSubscriber.nPublisher.nSubscriber
{
	public interface ISubscribeListener
	{
		void ReceivePublishedData(cSubscriberNode _SubscriberNode, string _Message);
	}
}
