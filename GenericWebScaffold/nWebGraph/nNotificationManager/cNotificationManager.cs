using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nNotificationManager.nNotifications.nCancelLessonsNotification;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nNotificationManager
{
    public class cNotificationManager : cCoreObject
    {
        DateTime LastExecutionTime { get; set; }
        cWebGraph WebGraph { get; set; }
        IDataServiceManager DataServiceManager { get; set; }
        cNotificationDataManager NotificationDataManager { get; set; }


        

        public cTestNotification CancelLessonsNotification { get; set; }



        public cNotificationManager(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, cNotificationDataManager _NotificationDataManager)
            : base(_App)
        {
            WebGraph = _WebGraph;
            DataServiceManager = _DataServiceManager;
            NotificationDataManager = _NotificationDataManager;
            LastExecutionTime = DateTime.Now;
        }

        public override void Init()
        {
            CancelLessonsNotification = App.Factories.ObjectFactory.ResolveInstance<cTestNotification>();
        }


        public List<dynamic> GetLastNotifications(cActorEntity _Actor)
        {
            List<dynamic> __Notifications = NotificationDataManager.GetTopNotificationsForAllChannel(_Actor, 5, __Item => {

                __Item.NotificationID = __Item.ID;
                __Item.ParameterObjects = JObject.Parse(__Item.ParameterObjects.Replace("{{", "{").Replace("}}", "}"));
            });
            return __Notifications;
        }


		public void StartNotificationBroadCaster(IController _Controller)
        {
            try
            {
                if ((DateTime.Now - LastExecutionTime).TotalSeconds > 15)
                {
                    lock (this)
                    {
                        if ((DateTime.Now - LastExecutionTime).TotalSeconds > 15)
                        {
                            lock (this)
                            {
                                LastExecutionTime = DateTime.Now;


                                List<cNotificationEntity> __Notifications = NotificationDataManager.GetNotBroadcasstedNotification();
                                if (__Notifications.Count > 0)
                                {
                                    IDataService __DataService = DataServiceManager.GetDataService();
                                    __DataService.Perform(() =>
                                    {
                                        for (int i = 0; i < __Notifications.Count; i++)
                                        {
                                            string __ParameterObjectsString = __Notifications[i].ParameterObjects.Replace("{{", "{").Replace("}}", "}");
                                            object __ParameterObjects = JsonConvert.DeserializeObject(__ParameterObjectsString);

                                            cNotificationProps __NotificationProps = new cNotificationProps(__Notifications[i].ID,
                                                ENotificationChannel.GetByID(__Notifications[i].ChannelID, ENotificationChannel.GlobalChannel),
                                                ENotificationType.GetByID(__Notifications[i].Type, ENotificationType.None),
                                                __ParameterObjects);

                                            List<long> __ActorIDList = new List<long>();

											foreach (var __ActorDetail in __Notifications[i].ActorDetails.ToList())
											{
												__ActorIDList.Add(__ActorDetail.Actor.GetValue().ID);
											} 

											List<cSession> __Sessions = WebGraph.SessionManager.GetSessionByActorID(__ActorIDList).Where(__Item => __Item.SignalRIDList.Count > 0).ToList();

                                            if (__Sessions.Count > 0)
                                            {
                                                WebGraph.ActionGraph.NotificationAction.Action(_Controller, __NotificationProps, __Sessions, true);
                                            }

                                            __Notifications[i].NotificationBroadcasted = true;
                                            __Notifications[i].Save();
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception _Ex)
            {
				List<string> __List = new List<string>();
				__List.Add("####################################################");
				__List.Add("###### NotificationBroadCaster Error   #############");
				__List.Add("####################################################");
				App.Loggers.BatchJobLogger.LogError(__List, _Ex, null);
            }
        }
    }
}
