using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Data.Boundary.nData;
using Data.Boundary.nNotificationProps;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Integration.MicroServiceGraph.nMicroService;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceNotificationAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nNotificationManager
{
    public abstract class cBaseNotification<TNotificationProps> : cCoreObject
        where TNotificationProps : cBaseNotificationProps
    {
        public ENotificationType NotificationType { get; set; }
        public cWebGraph WebGraph { get; set; }

        public IMicroService MicroService { get; set; }


        public cNotificationDataManager NotificationDataManager { get; set; }

        public cBaseNotification(ENotificationType _NotificationType, cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, cNotificationDataManager _NotificationDataManager)
            : base(_App)
        {
            NotificationType = _NotificationType;
            WebGraph = _WebGraph;
            NotificationDataManager = _NotificationDataManager;
            MicroService = _MicroService;

        }

        public override void Init()
        {
        }

        public abstract int LiveSecond();

        public virtual void Notify(IController _Controller, List<cActorEntity> _ReceiverActors, ENotificationChannel _NotificationChannel, TNotificationProps _NotificationProps, bool _NoticationSendMeWithPostBack = true)
        {
            cNotificationEntity __NotificationEntity = NotificationDataManager.AddNotification(_ReceiverActors, _NotificationChannel, NotificationType, _NotificationProps.SerializeObject(), DateTime.Now.AddSeconds(LiveSecond()), true);

            List<long> __ActorIDList = _ReceiverActors.Select<cActorEntity, long>(__Item => __Item.ID).ToList();

            List<cSession> __Sessions = WebGraph.SessionManager.GetSessionByActorID(__ActorIDList).Where(__Item => __Item.SignalRIDList.Count > 0).ToList();

            cNotificationProps __NotificationActionProps = new cNotificationProps(__NotificationEntity.ID, _NotificationChannel, NotificationType, _NotificationProps);

            if (__Sessions.Count > 0)
            {
                MicroService.MicroServiceActionGraph.NotificationAction.BroadcastAction(new cMicroServiceNotificationProps(__ActorIDList, __NotificationEntity.ID, _NotificationChannel, NotificationType, _NotificationProps));

                WebGraph.ActionGraph.NotificationAction.Action(_Controller, __NotificationActionProps, __Sessions, true);
            }
            //todo: bildirim gelmiyorsa eski koda dön
        }
    }
}
