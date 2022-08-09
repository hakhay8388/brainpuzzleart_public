using Base.Core.nApplication;
using Base.Core.nCore;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nActionListAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nAsyncLoadAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCacheItAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCommandListAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDataSourceRefreshAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDebugAlertAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoCheckLoginRequestAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nDoReconnectSignalRequestAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nForceLogoutAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nForceUpdateAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nGoPageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLanguageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLogInOutAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nModalOpenAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNoPermissionAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nNotificationAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nProgressStatusAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultListAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetClientLanguageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetGlobalParamListAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetServerDateTimeAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetStateAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetUserOnClientAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetVariableAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAndRunAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSuccessResultAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nValidationResultAction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph
{
    public class cActionGraph : cCoreObject
    {

        public List<IAction> ActionList;


        public cSuccessResultAction SuccessResultAction { get; set; }
        public cCacheItAction CacheItAction { get; set; }

        public cCommandListAction CommandListAction { get; set; }
        public cActionListAction ActionListAction { get; set; }
        public cLogInOutAction LogInOutAction { get; set; }
        public cForceLogoutAction ForceLogoutAction { get; set; }
        public cSetStateAction SetStateAction { get; set; }
        public cSetVariableAction SetVariableAction { get; set; }
        public cShowMessageAction ShowMessageAction { get; set; }
        public cShowMessageAndRunCommandAction ShowMessageAndRunCommandAction { get; set; }
        public cHotSpotMessageAction HotSpotMessageAction { get; set; }
        public cHotSpotMessageAndRunCommandAction HotSpotMessageAndRunCommandAction { get; set; }
        public cResultListAction ResultListAction { get; set; }

        public cSetGlobalParamListAction SetGlobalParamListAction { get; set; }

        public cResultItemAction ResultItemAction { get; set; }

        public cGoPageAction GoPageAction { get; set; }
        public cModalOpenAction ModalOpenAction { get; set; }
        public cNoPermissionAction NoPermissionAction { get; set; }

        public cLanguageAction LanguageAction { get; set; }
        public cSetClientLanguageAction SetClientLanguageAction { get; set; }
        
        public cSetUserOnClientAction SetUserOnClientAction { get; set; }

        public cSetServerDateTimeAction SetServerDateTimeAction { get; set; }
        public cProgressStatusAction ProgressStatusAction { get; set; }

        public cNotificationAction NotificationAction { get; set; }
		public cDataSourceRefreshAction DataSourceRefreshAction { get; set; }

		public cAsyncLoadAction AsyncLoadAction { get; set; }
		public cForceUpdateAction ForceUpdateAction { get; set; }


		public cDoReconnectSignalRequestAction DoReconnectSignalRequestAction { get; set; }

		public cDebugAlertAction DebugAlertAction { get; set; }

		public cDoCheckLoginRequestAction DoCheckLoginRequestAction { get; set; }
		public cValidationResultAction ValidationResultAction { get; set; }

		public cActionGraph(cApp _App, cWebGraph _WebGraph)
            : base(_App)
        {
            ActionList = new List<IAction>();
        }

        public override void Init()
        {
			Type __ThisType = this.GetType();
			List<Type> __Templates = App.Handlers.AssemblyHandler.GetTypesFromBaseInterface<IAction>();
			__Templates.ForEach(__Type =>
            {
                IAction __Action = (IAction)App.Factories.ObjectFactory.ResolveInstance(__Type);
                PropertyInfo __PropertyInfo = __ThisType.GetAllProperties().Where(__Item => __Item.Name.StartsWith(__Action.ActionID.Name + "Action")).FirstOrDefault();
                if (__PropertyInfo == null)
                {
                    throw new Exception($"{__Action.ActionID.Name} Action ismi ActionIDs ile eşleşmiyor.");
                }
                __PropertyInfo.GetSetMethod().Invoke(this, new object[] { __Action });

            });
		}
    }
}