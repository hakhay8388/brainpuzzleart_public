using Base.Core.nApplication;
using Base.Core.nCore;
using NetMQ.Sockets;
using NetMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nPublisherAndSubscriber.nPublisher
{
	public class cPublisherNode : cCoreObject
	{
		PublisherSocket m_PublisherSocket { get; set; }
		public cPublisherNode(cApp _App, string _Port)
			: base(_App)
		{
			CreateSocket("*", _Port);
		}

		public cPublisherNode(cApp _App, string _Ip, string _Port)
			: base(_App)
		{
			CreateSocket(_Ip, _Port);
		}

		~cPublisherNode()
		{
			m_PublisherSocket.Dispose();
		}


		private void CreateSocket(string _Ip, string _Port)
		{
			m_PublisherSocket = new PublisherSocket();
			m_PublisherSocket.Bind("tcp://" + _Ip + ":" + _Port);
		}

		public void PublishMessage(string _Channel, string _Message)
		{
			m_PublisherSocket
					.SendMoreFrame(_Channel)
					.SendFrame(_Message);
		}

	}
}
