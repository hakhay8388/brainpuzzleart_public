using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Newtonsoft.Json.Linq;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceNotificationAction
{
    public class cMicroServiceNotificationProps : cMicroServiceBaseProps
    {
		public virtual List<long> ActorIDs { get; set; }

		public virtual long NotificationID { get; set; }

        public virtual int ChannelID { get; set; }

        public virtual int Type { get; set; }

        public virtual  object ParameterObjects { get; set; }
        public cMicroServiceNotificationProps(List<long> _ActorIDs, long _NotificationID, ENotificationChannel _NotificationChannel, ENotificationType _NotificationType, object _ParameterObjects)
            :base()
        {
			ActorIDs = _ActorIDs;
			NotificationID = _NotificationID;
            ChannelID = _NotificationChannel.ID;
            Type = _NotificationType.ID;
            ParameterObjects = _ParameterObjects;
        }

		public cMicroServiceNotificationProps()
			: base()
		{ 
		}
	}
}
