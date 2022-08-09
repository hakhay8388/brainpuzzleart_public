using Base.Core.nApplication;
using Base.Core.nCore;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nPublisherAndSubscriber.nPublisher.nSubscriber
{
	public class cSubscriberNode : cCoreObject
	{
		string Channel { get; set; }
		ISubscribeListener SubscribeListener { get; set; }
		Thread RecieverThread { get; set; }
		SubscriberSocket m_SubscriberSocket { get; set; }
		public cSubscriberNode(cApp _App, ISubscribeListener _SubscribeListener, string _Port, string _Channel)
			:base(_App)
		{
			CreateSocket(_SubscribeListener, "*", _Port, _Channel);
		}

		public cSubscriberNode(cApp _App, ISubscribeListener _SubscribeListener, string _Ip, string _Port, string _Channel)
			: base(_App)
		{
			CreateSocket(_SubscribeListener, _Ip, _Port, _Channel);
		}

		~cSubscriberNode()
		{
			Stop();
			m_SubscriberSocket.Dispose();
		}


		private void CreateSocket(ISubscribeListener _ServerListener, string _Ip, string _Port, string _Channel)
		{
			SubscribeListener = _ServerListener;
			m_SubscriberSocket = new SubscriberSocket();
			m_SubscriberSocket.Connect("tcp://" + _Ip  + ":" + _Port);
			m_SubscriberSocket.Subscribe(_Channel);
			Channel = _Channel;
		}

		public void Start()
		{
			RecieverThread = new Thread(new ThreadStart(this.RecieverThreadFunction));
			RecieverThread.Name = "Server";
			RecieverThread.Start();
		}

		public void Stop()
		{
			if (RecieverThread != null)
			{
				try
				{
					RecieverThread.Abort();
				}
				catch(Exception _Ex)
				{
					App.Loggers.CoreLogger.LogError(_Ex);
				}
			}
		}

		void RecieverThreadFunction()
		{
			while(true)
			{
				string __Channel = m_SubscriberSocket.ReceiveFrameString();
				string __Message = m_SubscriberSocket.ReceiveFrameString();

				try
				{
					SubscribeListener.ReceivePublishedData(this, __Message);
				}
				catch (Exception _Ex)
				{
					App.Loggers.CoreLogger.LogError(_Ex);
				}				
			}
		}
	}
}
