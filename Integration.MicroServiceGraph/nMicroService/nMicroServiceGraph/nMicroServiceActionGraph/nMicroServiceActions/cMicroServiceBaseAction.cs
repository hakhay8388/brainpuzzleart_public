using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph;
using System;
using Base.Core.nCore;
using Base.Core.nApplication;
using System.Collections.Generic;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceIDs;
using Integration.MicroServiceGraph.nMicroService;
using Base.Data.nDataServiceManager;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions
{
    public abstract class cMicroServiceBaseAction<TActionProps> : cCoreObject, IMicroServiceActionWithProps<TActionProps>, IMicroServiceAction
		where TActionProps : IMicroServiceActionProps
	{
		protected IDataServiceManager DataServiceManager { get; set; }

		public MicroServiceIDs MicroServiceID { get; set; }
        public IMicroService MicroService { get; set; }
		public cMicroServiceActionGraph MicroServiceActionGraph { get; set; }

		public cMicroServiceBaseAction(cApp _App, IDataServiceManager _DataServiceManager, IMicroService _MicroService, MicroServiceIDs _MicroServiceID)
            :base(_App)
        {
			DataServiceManager = _DataServiceManager;
			MicroServiceID = _MicroServiceID;
			MicroService = _MicroService;
			MicroService.MicroServiceActionGraph.MicroServiceActionList.Add(this);
			MicroService.MicroServiceCommandGraph.ConnectToCommands(this);
        }


		public virtual void BroadcastAction(TActionProps _ActionData)
		{
			_ActionData.Host = DataServiceManager.GetDataHost();
			cMicroServiceApi __MicroServiceApi = new cMicroServiceApi(_ActionData.Host, true);
			__MicroServiceApi.MicroServiceActionJson.Add(PrepereObject(_ActionData.SerializeObject()));
			MicroService.BroadcastMessage(__MicroServiceApi);
		}

		public virtual void ResultAction(cMicroServiceApi _MicroServiceApi, TActionProps _ActionData)
		{
			_ActionData.Host = _MicroServiceApi.Host;
			_MicroServiceApi.MicroServiceActionJson.Add(PrepereObject(_ActionData.SerializeObject()));
		}

		public virtual void Action(string _Ip, string _Port, TActionProps _ActionData)
		{
			_ActionData.Host = DataServiceManager.GetDataHost();

			cMicroServiceApi __MicroServiceApi = new cMicroServiceApi(_ActionData.Host, true);
			__MicroServiceApi.MicroServiceActionJson.Add(PrepereObject(_ActionData.SerializeObject()));

			JArray __Result = MicroService.ActionMessage(_Ip, _Port, __MicroServiceApi);
			__MicroServiceApi = new cMicroServiceApi(_ActionData.Host, false);
			__MicroServiceApi.MicroServiceCommandJson = __Result;
			MicroService.MicroServiceCommandGraph.InterpreterCommand(__MicroServiceApi);
		}

		private JObject PrepereObject(JObject _Object)
        {
            JObject __JsonObject = new JObject();
            __JsonObject["MicroServiceID"] = JObject.FromObject(MicroServiceID);
            __JsonObject["Data"] = _Object;
            return __JsonObject;
        }

    }
}
