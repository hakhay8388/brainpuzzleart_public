using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Integration.MicroServiceGraph;
using Integration.MicroServiceGraph.nMicroService;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceListenerGraph;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph
{
    public class cBaseListener : cBaseMicroServiceListener
	{
        public cWebGraph WebGraph { get; set; }

        public IDataServiceManager DataServiceManager { get; set; }

        public Dictionary<Type, int> ListenerOrders { get; set; }

		protected IHubContext<SignalRHub> SignalHub { get; set; }


		public cBaseListener(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, Dictionary<Type, int> _ListenerOrders = null)
            : base(_App, _MicroService)
        {
            if (_ListenerOrders != null)
            {
                ListenerOrders = _ListenerOrders;
            }
            else
            {
                ListenerOrders = new Dictionary<Type, int>();
            }
            
            WebGraph = _WebGraph;
            DataServiceManager = _DataServiceManager;
        }

        public override void Init()
        {
			base.Init();
			WebGraph.CommandGraph.ConnectToCommands(this); 
        }


		private void SendMessageToSessions(List<cSignalSessionMatcher> _SignalSessions)
		{
			_SignalSessions.ForEach(__Item =>
			{
				if (__Item.Session == null)
				{
					WebGraph.SessionManager.GetSessionList().ForEach(__SessionItems =>
					{
						//SignalHub.Clients.All.SendAsync("CommandChannel", __Item.ActionJson.ToString());
						if (__SessionItems.IsLogined)
						{
							foreach (string __SignalRID in __SessionItems.SignalRIDList)
							{
								SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
							}
						}
					});
				}
				else
				{
					if (__Item.Session.IsLogined)
					{
						foreach (string __SignalRID in __Item.Session.SignalRIDList)
						{
							SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
						}
					}
				}
			});
		}

	
		public void InstantSendSignal(List<cSession> _Sessionlist, JObject _Object)
		{
			if (SignalHub == null)
			{
				SignalHub = App.Factories.ObjectFactory.ResolveInstance<IHubContext<SignalRHub>>();
			}			
			if (_Sessionlist != null)
			{
				List<cSignalSessionMatcher> __SignalSessions = new List<cSignalSessionMatcher>();
				if (_Sessionlist.Count > 0)
				{
					_Sessionlist.ForEach(__Item =>
					{
						cSignalSessionMatcher __FindItem = __SignalSessions.Find(__Main => __Main.Session.SessionID == __Item.SessionID);
						if (__FindItem == null)
						{
							__FindItem = new cSignalSessionMatcher(__Item);
							__SignalSessions.Add(__FindItem);
						}
						__FindItem.ActionJson.Add(_Object);
					});
				}

				SendMessageToSessions(__SignalSessions);
			}
		}


	}
}
