using Base.Core.nApplication;
using Base.Core.nCore;
using NetMQ.Sockets;
using NetMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nPushAndPull.nClient
{
	public class cPushNode : cCoreObject
	{
		PushSocket m_PushSocket { get; set; }
		public cPushNode(cApp _App, string _Ip, string _Port)
			: base(_App)
		{
			CreateSocket(_Ip, _Port);
		}

		~cPushNode()
		{
			m_PushSocket.Dispose();
		}

		private void CreateSocket( string _Ip, string _Port)
		{
			m_PushSocket = new PushSocket();
			m_PushSocket.Connect("tcp://" + _Ip + ":" + _Port); 
		}


		public void SendMessage(string _Message)
		{
			m_PushSocket.SendFrame(_Message);
		}
	}
}
