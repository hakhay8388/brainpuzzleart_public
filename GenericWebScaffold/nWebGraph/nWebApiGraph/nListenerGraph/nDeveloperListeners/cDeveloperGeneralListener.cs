using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDataSourceRefreshAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nBatchJobStartCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nForceLogoutCommand;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Integration.MicroServiceGraph.nMicroService;
using RestSharp;
using System.Collections.Generic;
using System.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nDeveloperListeners
{
    public class cDeveloperGeneralListener : cBaseListener
        , IBatchJobStartReceiver
        , IForceLogoutReceiver

    {
        public cDeveloperGeneralListener(cApp _App
			, IMicroService _MicroService
			, cWebGraph _WebGraph
            , IDataServiceManager _DataServiceManager
            )
        : base(_App, _MicroService, _WebGraph, _DataServiceManager)
        {
            WebGraph = _WebGraph;
        }

        public void ReceiveBatchJobStartData(cListenerEvent _ListenerEvent, IController _Controller, cBatchJobStartCommandData _ReceivedData)
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
                    WebGraph.ActionGraph.ShowMessageAction
                                                                     .SuccessAction(_Controller,
                                                                         new cMessageProps()
                                                                         {
                                                                             Header = _Controller.GetWordValue(
                                                                                 "OperationComplete"),
                                                                             Message = response.Content
                                                                         });
                }
                else
                {
                    WebGraph.ActionGraph.ShowMessageAction.ErrorAction(
                                                                 _Controller,
                                                                 new cMessageProps()
                                                                 {
                                                                     Header = _Controller.GetWordValue("Error"),
                                                                     Message = response.Content
                                                                 });
                }

            }
            else
            {
                WebGraph.ActionGraph.ShowMessageAction.ErrorAction(
                                                                 _Controller,
                                                                 new cMessageProps()
                                                                 {
                                                                     Header = _Controller.GetWordValue("Error"),
                                                                     Message = _Controller.GetWordValue("ColumnIsNull", App.Handlers.LambdaHandler.GetObjectPropName(() => __DataService.BatchJobUrl))
                                                                 });
            }
        }

        public void ReceiveForceLogoutData(cListenerEvent _ListenerEvent, IController _Controller, cForceLogoutCommandData _ReceivedData)
        {

            List<cSession> __SessionList = WebGraph.SessionManager.GetSessionList();
            List<cSession> __Sessions = __SessionList.Where(__Item => __Item.SessionID == _ReceivedData.SessionID).ToList();
            IDataService __DataService = DataServiceManager.GetDataService(); 
            __DataService.Perform(() =>
            {
                WebGraph.SessionManager.ForceLogout(__Sessions);
            });
            
            WebGraph.ActionGraph.ForceLogoutAction.Action(_Controller, __Sessions, true);

            WebGraph.ActionGraph.DataSourceRefreshAction.Action(_Controller,
                                                           new cDataSourceRefreshProps()
                                                           {
                                                               DataSource = DataSourceIDs.LiveSessionEmails
                                                           });
        }
    }
}