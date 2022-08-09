using Base.Communication.nCommunicationService.nCommunicationComponents.nOneToOne.nClient;
using Base.Core.nApplication;
using Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceWhichServersAmIAClientOfPoolManager
{
	public class cMicroServiceWhichServersAmIAClientOfPoolManager : cCoreObject
	{
		public List<cClientNode> Clients { get; set; }
		public cMicroServiceWhichServersAmIAClientOfPoolManager(cApp _App)
			: base(_App)
		{
			Clients = new List<cClientNode>();
		}

		public cClientNode GetClientNode(string _Ip, string _Port)
		{
			lock(Clients)
			{
				cClientNode __ClientNode = Clients.Where(__Item => __Item.Ip == _Ip && __Item.Port == _Port).FirstOrDefault();
				if (__ClientNode == null)
				{
					__ClientNode = new cClientNode(App, _Ip, _Port);
					Clients.Add(__ClientNode);
				}
				return __ClientNode;
			}			
		}
	}
}
