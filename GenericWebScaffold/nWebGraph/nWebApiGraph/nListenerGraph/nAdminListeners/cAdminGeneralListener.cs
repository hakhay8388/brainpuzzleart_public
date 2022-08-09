using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.BatchJobService.nBatchJobManager;
using Core.BatchJobService.nBatchJobManager.nJobs.nMailSenderJob;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultListAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetConfigParamListCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveConfigParamListCommand;
using Data.Boundary.nDataTransferObjects;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using System;
using System.Collections.Generic;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nAdminListeners
{
    public class cAdminGeneralListener : cBaseListener
        , IGetConfigParamListReceiver
        , ISaveConfigParamListReceiver
    {
        public cActorDataManager ActorDataManager { get; set; }
        public cUserDataManager UserDataManager { get; set; }
        public cBatchJobManager BatchJobManager { get; set; }
        public IManagers Managers { get; set; }
        public cParamsDataManager ParamsDataManager { get; set; }

        public cAdminGeneralListener(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager
            , cActorDataManager _ActorDataManager
            , cUserDataManager _UserDataManager
            , cBatchJobManager _BatchJobManager
            , IManagers _Managers
            , cParamsDataManager _ParamsDataManager
            )
            : base(_App, _MicroService, _WebGraph, _DataServiceManager)
        {
            WebGraph = _WebGraph;
            ActorDataManager = _ActorDataManager;
            UserDataManager = _UserDataManager;
            BatchJobManager = _BatchJobManager;
            Managers = _Managers;
            ParamsDataManager = _ParamsDataManager;

        }

        public void ReceiveGetConfigParamListData(cListenerEvent _ListenerEvent, IController _Controller, cGetConfigParamListCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();

                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {
                        IDataService __DataService = DataServiceManager.GetDataService();
                        List<dynamic> __GlobalParams = ParamsDataManager.GetAllParams();

                        WebGraph.ActionGraph.ResultListAction.Action(_Controller, new cResultListProps() { ResultList = __GlobalParams, Page = 1, Total = __GlobalParams.Count });
                    }
                    else
                    {
                        WebGraph.ActionGraph.NoPermissionAction.Action(_Controller);
                    }
                }
                catch (Exception _Ex)
                {
                    WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
                }
            }
            else
            {
                WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
            }
        }

        public void ReceiveSaveConfigParamListData(cListenerEvent _ListenerEvent, IController _Controller, cSaveConfigParamListCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();

                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {
                        IDataService __DataService = DataServiceManager.GetDataService();

                        List<cGlobalParamsItem> __NewGlobalParams = _ReceivedData.GlobalParamsItems;
                        try
                        {
                            __DataService.Perform(() =>
                            {
                                for (int i = 0; i < __NewGlobalParams.Count; i++)
                                {
                                    ParamsDataManager.UpdateGlobalParam(__NewGlobalParams[i].ID, __NewGlobalParams[i].Value);
                                }
                            });

                            __DataService.LoadGlobalParams();
                            __DataService.Perform(() =>
                            {

                                BatchJobManager.MailSenderJob.AddQueue(new cMailSenderJobProps() { ReLoadConfig = true });
                            });

                            __DataService.LoadGlobalParams();

                            __DataService.Perform(() =>
                            {
                                BatchJobManager.MailSenderJob.AddQueue(new cMailSenderJobProps() { ReLoadConfig = true });
                            });
                            WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                            WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Success"), Message = _Controller.GetWordValue("OperationComplete"), ColorType = EColorTypes.Success, DurationMS = 2500 });
                        }
                        catch (Exception _Ex)
                        {

                            WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
                        }

                    }
                    else
                    {
                        WebGraph.ActionGraph.NoPermissionAction.Action(_Controller);
                    }
                }
                catch (Exception _Ex)
                {
                    WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
                }
            }
            else
            {
                WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
            }
        }
    }
}