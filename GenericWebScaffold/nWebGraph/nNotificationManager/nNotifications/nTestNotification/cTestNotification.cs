using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nNotificationManager;
using Data.Boundary.nData;
using Data.Boundary.nNotificationProps;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Integration.MicroServiceGraph.nMicroService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nNotificationManager.nNotifications.nCancelLessonsNotification
{
    public class cTestNotification : cBaseNotification<cTestNotificationProps>
    {
        public cTestNotification(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, cNotificationDataManager _NotificationDataManager)
          : base(ENotificationType.TestNotification, _App, _MicroService, _WebGraph, _NotificationDataManager)
        {
        }

        public override int LiveSecond()
        {
            return (3600 * 24) * 7;
        }
    }
}
