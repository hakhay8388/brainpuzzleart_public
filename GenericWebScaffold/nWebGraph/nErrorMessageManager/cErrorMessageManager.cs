using Base.Boundary.nCore.nBootType;
using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nConfiguration;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Data.GenericWebScaffold.nDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nErrorMessageManager
{
    public class cErrorMessageManager : cCoreObject
    {
		public cWebGraph WebGraph { get; set; }
		public IDataServiceManager DataServiceManager { get; set; }
		public cErrorMessageManager(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager)
           : base(_App)
        {
			WebGraph = _WebGraph;
			DataServiceManager = _DataServiceManager;
		}

		public void ErrorAction(Exception _Ex, IController _Controller, string _Header, string _Message)
		{
			cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

			if (__DataService.BackendDebugMessageShowToUser)
			{
				WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Ex, _Controller,
					   new cMessageProps()
					   {
						   Header = _Header,
						   Message = _Message
					   });
			}
			else
			{
				WebGraph.ActionGraph.DebugAlertAction.ErrorAction(_Ex, _Controller,
				   new cDebugAlertProps()
				   {
					   Header = _Header,
					   Message = _Message
				   });
			}
		}

		public void ErrorAction(IController _Controller, string _Header, string _Message)
		{
			cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

			if (__DataService.BackendDebugMessageShowToUser)
			{
				WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller,
					   new cMessageProps()
					   {
						   Header = _Header,
						   Message = _Message
					   });
			}
			else
			{
				WebGraph.ActionGraph.DebugAlertAction.ErrorAction(_Controller,
				   new cDebugAlertProps()
				   {
					   Header = _Header,
					   Message = _Message
				   });
			}
		}
	}
}

