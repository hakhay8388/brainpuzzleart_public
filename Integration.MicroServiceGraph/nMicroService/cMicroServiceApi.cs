using Base.Communication.nCommunicationService.nCommunicationComponents.nPublisherAndSubscriber.nPublisher.nSubscriber;
using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph
{
    public class cMicroServiceApi
	{
		public bool InstantAction { get; private set; }

		public string Host { get; set; }
		public JArray MicroServiceCommandJson { get; set; }
		public JArray MicroServiceActionJson { get; set; }
		public cMicroServiceApi(string _Host, bool _InstantAction)
		{
			InstantAction = _InstantAction;
			Host = _Host;
			MicroServiceActionJson = new JArray();
			MicroServiceCommandJson = new JArray();
		}
	}
}
