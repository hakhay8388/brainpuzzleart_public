using Base.Core.nApplication;
using Base.Core.nCore;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nPushAndPull.nServer
{
	public class cPullNode : cCoreObject
	{
		IPullListener ServerListener { get; set; }
		Thread RecieverThread { get; set; }
		PullSocket m_PullSocket { get; set; }
		public cPullNode(cApp _App, IPullListener _ServerListener, string _Port)
			:base(_App)
		{
			CreateSocket(_ServerListener, "*", _Port);
		}

		public cPullNode(cApp _App, IPullListener _ServerListener, string _Ip, string _Port)
			: base(_App)
		{
			CreateSocket(_ServerListener, _Ip, _Port);
		}

		~cPullNode()
		{
			Stop();
			m_PullSocket.Dispose();
		}


		private void CreateSocket(IPullListener _ServerListener, string _Ip, string _Port)
		{
			ServerListener = _ServerListener;
			m_PullSocket = new PullSocket();
			m_PullSocket.Bind("tcp://" + _Ip  + ":" + _Port);
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
				string __Msg = m_PullSocket.ReceiveFrameString();
				try
				{
					__Msg = ServerListener.ReceivePullData(this, __Msg);
				}
				catch (Exception _Ex)
				{
					App.Loggers.CoreLogger.LogError(_Ex); ;
				}				
				m_PullSocket.SendFrame(__Msg);
			}
		}
	}
}
