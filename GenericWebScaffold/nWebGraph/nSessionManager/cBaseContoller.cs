using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Base.Core.nApplication;
using Base.Data.nDataServiceManager;
using Base.Data.nDataService;
using Core.GenericWebScaffold.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using Data.GenericWebScaffold.nDataService;
using RestSharp;

namespace Core.GenericWebScaffold.nWebGraph.nSessionManager
{
    public abstract class cBaseController : Controller, IController
    {
        public List<cSignalSessionMatcher> SignalSessions { get; set; }
        public cSession ClientSession { get; set; }
        public JObject CommandJson { get; set; }
        public JArray ActionJson { get; set; }

        public cWebGraph WebGraph { get; set; }
        public cApp App { get; set; }
        public string IpAddress { get; set; }
        public IDataServiceManager DataServiceManager { get; set; }
        public IDataService DataService { get; set; }
        public HttpContext CurrentContext { get; set; }
        public IHubContext<SignalRHub> SignalHub { get; set; }
        public bool IsSignal { get { return false; } }

        public cBaseController(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, IHubContext<SignalRHub> _SignalHub)
        {
            App = _App;
            WebGraph = _WebGraph;
            DataServiceManager = _DataServiceManager;
            SignalHub = _SignalHub;


        }
        public string GetWordValue(string _Word, params object[] _Parameters)
        {
            return App.Handlers.LanguageHandler.GetWordValue(ClientSession.Language, _Word, _Parameters);
        }
        static bool __BatchJobExecution = false;
        public override void OnActionExecuting(ActionExecutingContext _Context)
        {
            base.OnActionExecuting(_Context);
            CurrentContext = _Context.HttpContext;
            IpAddress = CurrentContext.Connection.RemoteIpAddress.ToString();
            var __Watch = new Stopwatch();
            __Watch.Start();

            CurrentContext.Response.OnStarting(_Props =>
            {
                string __Total = __Watch.ElapsedMilliseconds.ToString();
                CurrentContext.Response.Headers.Add("X-Response-Time-Milliseconds", __Total);

                return Task.CompletedTask;
            }, CurrentContext);


            InitializeClientSession(_Context);
            SignalSessions = new List<cSignalSessionMatcher>();
#if !DEBUG
            if (!__BatchJobExecution)
            { 
                BatchJobStart();
            }
#endif
        }

        private void BatchJobStart()
        {
            cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            if (!__DataService.BatchJobUrl.IsNullOrEmpty())
            {
                var client = new RestClient(__DataService.BatchJobUrl);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    __BatchJobExecution = true;
                }

            }
        }

        protected void InitializeClientSession(ActionExecutingContext _Context)
        {
            ActionJson = new JArray();
            App.Handlers.ContextHandler.AddContext(_Context.HttpContext);
            ClientSession = WebGraph.SessionManager.CreateSession(this);
        }

        public void Logout()
        {
            WebGraph.SessionManager.Logout(this);
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
                    //if (__Item.Session.IsLogined)
                    //{
                        foreach (string __SignalRID in __Item.Session.SignalRIDList)
                        {
                            SignalHub.Clients.Client(__SignalRID).SendAsync("CommandChannel", __Item.ActionJson.ToString());
                        }
                    //}
                }
            });
        }

        public void InstantSendSignalAll(JObject _Object)
        {
            List<cSignalSessionMatcher> __SignalSessions = new List<cSignalSessionMatcher>();
            cSignalSessionMatcher __FindItem = new cSignalSessionMatcher(null);
            __FindItem.ActionJson.Add(_Object);
            __SignalSessions.Add(__FindItem);
            SendMessageToSessions(__SignalSessions);
        }

        public void InstantSendSignal(List<cSession> _Sessionlist, JObject _Object)
        {
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
        //burası düzelecek
        public void AddSignal(List<cSession> _Sessionlist, JObject _Object)
        {
            if (_Sessionlist != null)
            {
                if (_Sessionlist.Count > 0)
                {
                    _Sessionlist.ForEach(__Item =>
                    {
                        cSignalSessionMatcher __FindItem = SignalSessions.Find(__Main => __Main.Session != null && __Main.Session.SessionID == __Item.SessionID);
                        if (__FindItem == null)
                        {
                            __FindItem = new cSignalSessionMatcher(__Item);
                            SignalSessions.Add(__FindItem);
                        }
                        __FindItem.ActionJson.Add(_Object);
                    });
                }
                else
                {
                    cSignalSessionMatcher __FindItem = new cSignalSessionMatcher(null);
                    __FindItem.ActionJson.Add(_Object);
                    SignalSessions.Add(__FindItem);
                }
            }
        }
    }
}
