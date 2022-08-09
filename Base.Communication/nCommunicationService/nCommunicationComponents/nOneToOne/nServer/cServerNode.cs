using Base.Core.nApplication;
using Base.Core.nCore;
using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nOneToOne.nServer
{
	public class cServerNode : cCoreObject
	{
		IServerListener ServerListener { get; set; }
		Thread RecieverThread { get; set; }
		ResponseSocket m_ResponseSocket { get; set; }
		public cServerNode(cApp _App, IServerListener _ServerListener, string _Port)
			:base(_App)
		{
			CreateSocket(_ServerListener, "*", _Port);
		}

		public cServerNode(cApp _App, IServerListener _ServerListener, string _Ip, string _Port)
			: base(_App)
		{
			CreateSocket(_ServerListener, _Ip, _Port);
		}

		~cServerNode()
		{

			Stop();
			m_ResponseSocket.Dispose();

		}


		private void CreateSocket(IServerListener _ServerListener, string _Ip, string _Port)
		{
			ServerListener = _ServerListener;
			m_ResponseSocket = new ResponseSocket();
			m_ResponseSocket.Bind("tcp://" + _Ip  + ":" + _Port);
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
				string __Msg = m_ResponseSocket.ReceiveFrameString();
				try
				{
					__Msg = ServerListener.ReceiveServerData(this, __Msg);
				}
				catch (Exception _Ex)
				{
					App.Loggers.CoreLogger.LogError(_Ex); ;
				}				
				m_ResponseSocket.SendFrame(__Msg);
			}
		}
	}
}
