using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

using Base.Core.nApplication;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceIDs;
using Integration.MicroServiceGraph.nMicroService;
using Base.Data.nDataServiceManager;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceNotificationAction
{
    public class cMicroServiceNotificationAction : cMicroServiceBaseAction<cMicroServiceNotificationProps>
	{

        public cMicroServiceNotificationAction(cApp _App, IDataServiceManager _DataServiceManager, IMicroService _MicroService)
           : base(_App, _DataServiceManager, _MicroService, MicroServiceIDs.Notification)
        {
        }
    }
}
