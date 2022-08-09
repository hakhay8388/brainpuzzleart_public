using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.nWebGraph;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.Controllers
{
    public interface IController
    {
        List<cSignalSessionMatcher> SignalSessions { get; set; }
        HttpContext CurrentContext { get; set; }
        IHubContext<SignalRHub> SignalHub { get; set; }
        cSession ClientSession { get; set; }
        JObject CommandJson { get; set; }
        JArray ActionJson { get; set; }
        cWebGraph WebGraph { get; set; }
        cApp App { get; set; }
        IDataServiceManager DataServiceManager { get; set; }
        IDataService DataService { get; set; }
        string GetWordValue(string _Word, params object[] _Parameters);
        void Logout();
        void AddSignal(List<cSession> _Sessionlist, JObject _Object);
        void InstantSendSignal(List<cSession> _Sessionlist, JObject _Object);

		void InstantSendSignalAll(JObject _Object);

		bool IsSignal { get; }
	}
}
