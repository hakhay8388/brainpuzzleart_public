using Base.Core.nApplication;
using Base.Core.nCore;
using NetMQ.Sockets;
using NetMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Communication.nCommunicationService.nCommunicationComponents.nOneToOne.nClient
{
	public class cClientNode : cCoreObject
	{
		public string Ip { get; set; }
		public string Port { get; set; }
		RequestSocket m_RequestSocket { get; set; }
		public cClientNode(cApp _App, string _Ip, string _Port)
			: base(_App)
		{
			Ip = _Ip;
			Port = _Port;
			CreateSocket(_Ip, _Port);
		}

		~cClientNode()
		{
			m_RequestSocket.Dispose();
		}

		private void CreateSocket( string _Ip, string _Port)
		{
			m_RequestSocket = new RequestSocket();
			m_RequestSocket.Connect("tcp://" + _Ip + ":" + _Port); 
		}


		public string SendMessage(string _Message)
		{
			m_RequestSocket.SendFrame(_Message);
			return m_RequestSocket.ReceiveFrameString();
		}
	}
}
