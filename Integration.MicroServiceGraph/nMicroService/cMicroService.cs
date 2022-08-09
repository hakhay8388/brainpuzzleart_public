using Base.Boundary.nCore.nBootType;
using Base.Boundary.nCore.nObjectLifeTime;
using Base.Boundary.nData;
using Base.Communication.nCommunicationService.nCommunicationComponents.nOneToOne.nClient;
using Base.Communication.nCommunicationService.nCommunicationComponents.nOneToOne.nServer;
using Base.Communication.nCommunicationService.nCommunicationComponents.nPublisherAndSubscriber.nPublisher.nSubscriber;
using Base.Communication.nCommunicationService.nCommunicationComponents.nPushAndPull.nClient;
using Base.Core.nApplication;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Data.nConfiguration;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase;
using Integration.MicroServiceGraph.nConfiguration;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceIDs;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceListenerGraph;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceWhichServersAmIAClientOfPoolManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService
{
	[Register(typeof(IMicroService), false, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
	public class cMicroService : cCoreService<cMicroServiceContext>, IMicroService, ISubscribeListener , IServerListener
	{
		public cSubscriberNode SubscriberNode { get; set; }
		public cPushNode PublishTriggerClientNode { get; set; }

		public cServerNode MyServer { get; set; }
		public cMicroServiceWhichServersAmIAClientOfPoolManager MicroServiceWhichServersAmIAClientOfPoolManager { get; set; }

		public cMicroServiceActionGraph MicroServiceActionGraph { get; set; }
		public cMicroServiceCommandGraph MicroServiceCommandGraph { get; set; }

		public cMicroServiceListenerGraph MicroServiceListenerGraph { get; set; }

		public cMicroService(cMicroServiceContext _MicroServiceContext)
			: base(_MicroServiceContext)
		{
			MicroServiceWhichServersAmIAClientOfPoolManager = new cMicroServiceWhichServersAmIAClientOfPoolManager(_MicroServiceContext.App);
		}

		public override void Init()
		{
			MicroServiceActionGraph = App.Factories.ObjectFactory.ResolveInstance<cMicroServiceActionGraph>();
			MicroServiceCommandGraph = App.Factories.ObjectFactory.ResolveInstance<cMicroServiceCommandGraph>();
			MicroServiceListenerGraph = App.Factories.ObjectFactory.ResolveInstance<cMicroServiceListenerGraph>();

			MicroServiceActionGraph.Init();
			MicroServiceCommandGraph.Init();
			MicroServiceListenerGraph.Init();

			if (!ServiceContext.Configuration.PublisherServerIP.IsNullOrEmpty())
			{
				SubscriberNode = new cSubscriberNode(App, this, ServiceContext.Configuration.PublisherServerIP, ServiceContext.Configuration.PublisherServerPort, "MicroService");
				SubscriberNode.Start();
				PublishTriggerClientNode = new cPushNode(App, ServiceContext.Configuration.MicroServiceTriggerServerIP, ServiceContext.Configuration.MicroServiceTriggerServerPort);
			}

			if (!ServiceContext.Configuration.MicroServiceListenServerIP.IsNullOrEmpty())
			{
                //MicroService ihtiyacı için burası açılacak
				//MyServer = new cServerNode(App, this, ServiceContext.Configuration.MicroServiceListenServerIP, ServiceContext.Configuration.MicroServiceListenServerPort);
				//MyServer.Start();
			}

		}

		public void ReceivePublishedData(cSubscriberNode _SubscriberNode, string _Message)
		{
			JArray __CommandJson = JArray.Parse(_Message);

			
			cMicroServiceApi __MicroServiceApi = new cMicroServiceApi("", true);
			__MicroServiceApi.MicroServiceCommandJson = __CommandJson;
			if (__MicroServiceApi.MicroServiceCommandJson.Count > 0)
			{
				JObject __CommandItem = (JObject)__MicroServiceApi.MicroServiceCommandJson[0];

				if (__CommandItem.ContainsKey("MicroServiceID"))
				{
					int __CID = (int)__CommandItem["MicroServiceID"]["ID"];
					MicroServiceIDs __CommandID = MicroServiceIDs.GetByID(__CID, null);
					if (__CommandItem.ContainsKey("Data"))
					{
						JObject __Data = (JObject)__CommandItem["Data"];
						__MicroServiceApi.Host = __Data["Host"].ToString();
						MicroServiceCommandGraph.InterpreterCommand(__MicroServiceApi);
					}
				}

			}
			
		}

		public void BroadcastMessage(cMicroServiceApi _MicroServiceApi)
		{
			if (PublishTriggerClientNode != null)
			{
				string __Message = JsonConvert.SerializeObject(_MicroServiceApi.MicroServiceActionJson);
				PublishTriggerClientNode.SendMessage(__Message);
			}
		}


		public JArray ActionMessage(string _Ip, string _Port, cMicroServiceApi _MicroServiceApi)
		{
			cClientNode __ClientNode = MicroServiceWhichServersAmIAClientOfPoolManager.GetClientNode(_Ip, _Port);
			string __Message = JsonConvert.SerializeObject(_MicroServiceApi.MicroServiceActionJson);
			string __Result = __ClientNode.SendMessage(__Message);

			return JArray.Parse(__Result);
		}

		public string ReceiveServerData(cServerNode _ServerNode, string _Message)
		{
			JArray __CommandJson = JArray.Parse(_Message);

			cMicroServiceApi __MicroServiceApi = new cMicroServiceApi("", true);
			__MicroServiceApi.MicroServiceCommandJson = __CommandJson;
			if (__MicroServiceApi.MicroServiceCommandJson.Count > 0)
			{
				JObject __CommandItem = (JObject)__MicroServiceApi.MicroServiceCommandJson[0];

				if (__CommandItem.ContainsKey("MicroServiceID"))
				{
					int __CID = (int)__CommandItem["MicroServiceID"]["ID"];
					MicroServiceIDs __CommandID = MicroServiceIDs.GetByID(__CID, null);
					if (__CommandItem.ContainsKey("Data"))
					{
						JObject __Data = (JObject)__CommandItem["Data"];
						__MicroServiceApi.Host = __Data["Host"].ToString();
						MicroServiceCommandGraph.InterpreterCommand(__MicroServiceApi);
						return __MicroServiceApi.MicroServiceActionJson.ToString();
					}
				}

			}

			return __MicroServiceApi.MicroServiceActionJson.ToString();
		}
	}
}
