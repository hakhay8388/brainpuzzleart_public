using Base.Core.nApplication;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.nWebGraph;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetServerDateTimeAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.Controllers
{
    [Route("api/[controller]")]
    public class WebApiController : cBaseController
    {
        public WebApiController(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, IHubContext<SignalRHub> _SignalHub)
            :base(_App, _WebGraph, _DataServiceManager, _SignalHub)
        {
        }

        [HttpPost("[action]")]
        public JsonResult WebApi()
        {
            Stream ___Request = Request.Body;
            if (___Request.CanSeek)
            {
                ___Request.Seek(0, System.IO.SeekOrigin.Begin);
            }
            string __JSON = new StreamReader(___Request).ReadToEnd();

            //dynamic x = JsonConvert.DeserializeObject(__JSON);
            CommandJson = JObject.Parse(__JSON);

            WebGraph.CommandGraph.InterpreterCommand(this);
			try
			{
				WebGraph.ActionGraph.SetServerDateTimeAction.Action(this, new cSetServerDateTimeProps() { ServerDate = App.Handlers.DateTimeHandler.Now });


				WebGraph.NotificationManager.StartNotificationBroadCaster(this);

				SignalSessions.ForEach(__Item =>
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
			catch(Exception _Ex)
			{
				WebGraph.ErrorMessageManager.ErrorAction(_Ex, this, this.GetWordValue("Error"), this.GetWordValue("UnknownError"));
			}
            

            return Json(ActionJson);
        }
    }
}
