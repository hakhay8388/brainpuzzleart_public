using Base.Core.nApplication;
using Base.Core.nHandlers.nLanguageHandler;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultItemAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetClientLanguageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetBatchJobDetailCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetLanguageWordByCodeCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveBatchJobDetailCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveLanguageWordListCommand;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using System;
using System.Collections.Generic;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nDefinationListener
{
    public class cDefinationListener : cBaseListener
        , IGetBatchJobDetailReceiver
        , ISaveBatchJobDetailReceiver
        , IGetLanguageWordByCodeReceiver
        , ISaveLanguageWordListReceiver
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cFileDataManager FileDataManager { get; set; }
        public cNotificationDataManager NotificationDataManager { get; set; }
        public IManagers Managers { get; set; }
        public cBatchJobDataManager BatchJobDataManager { get; set; }
        public cLanguageDataManager LanguageDataManager { get; set; }
        public cDefinationListener(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph
            , IDataServiceManager _DataServiceManager
            , cRoleDataManager _RoleDataManager
            , cFileDataManager _FileDataManager
            , cNotificationDataManager _NotificationDataManager
            , IManagers _Managers
            , cBatchJobDataManager _BatchJobDataManager
            , cLanguageDataManager _LanguageDataManager
            )
          : base(_App, _MicroService, _WebGraph, _DataServiceManager)
        {
            WebGraph = _WebGraph;
            RoleDataManager = _RoleDataManager;
            FileDataManager = _FileDataManager;
            NotificationDataManager = _NotificationDataManager;
            BatchJobDataManager = _BatchJobDataManager;
            Managers = _Managers;
            LanguageDataManager = _LanguageDataManager;
        }

        public void ReceiveGetBatchJobDetailData(cListenerEvent _ListenerEvent, IController _Controller, cGetBatchJobDetailCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();

                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {

                        dynamic __BatchJobDetail = BatchJobDataManager.GetBatchJobByID(_ReceivedData.BatchJobId);

                        WebGraph.ActionGraph.ResultItemAction.Action(_Controller, new cResultItemProps()
                        {
                            Item = new
                            {
                                BatchJobDetail = __BatchJobDetail
                            }
                        });
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

        public void ReceiveSaveBatchJobDetailData(cListenerEvent _ListenerEvent, IController _Controller, cSaveBatchJobDetailCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();

                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {
                        IDataService __DataService = DataServiceManager.GetDataService();

                        try
                        {
                            __DataService.Perform(() =>
                            {
                                BatchJobDataManager.UpdateBatchJob(_ReceivedData.ID, _ReceivedData.TimePeriodMilisecond, _ReceivedData.AutoAddExecution, _ReceivedData.ExecuteFirstWithoutWait, _ReceivedData.StopAfterFirstExecution, _ReceivedData.MaxRetryCount);
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
        public void ReceiveGetLanguageWordByCodeData(cListenerEvent _ListenerEvent, IController _Controller, cGetLanguageWordByCodeCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();

                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {
                        IDataService __DataService = DataServiceManager.GetDataService();

                        try
                        {
                            List<dynamic> __Words = LanguageDataManager.GetWordByCode(_ReceivedData.LanguageCode);
                            WebGraph.ActionGraph.ResultItemAction.Action(_Controller, new cResultItemProps()
                            {
                                Item = new
                                {
                                    Words = __Words
                                }
                            });
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

        public void ReceiveSaveLanguageWordListData(cListenerEvent _ListenerEvent, IController _Controller, cSaveLanguageWordListCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();

                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {
                        IDataService __DataService = DataServiceManager.GetDataService();

                        List<dynamic> __NewWords = _ReceivedData.LanguageWords;
                        bool __IsValid = true;
                        __NewWords.ForEach(__Item =>
                        {
                            int __ParamCount = (int)__Item.ParamCount;
                            if (__ParamCount > 0)
                            {
                                for (int i = 0; i < __ParamCount; i++)
                                {
                                    if (!((string)__Item.Word).Contains("{" + i + "}"))
                                    {
                                        __IsValid = false;
                                    }
                                }
                            }
                        });
                        if (!__IsValid)
                        {

                            WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Warning"), Message = _Controller.GetWordValue("ParamCountNotValid"), ColorType = EColorTypes.Warning, DurationMS = 2500 });

                        }
                        else
                        {

                            try
                            {
                                __DataService.Perform(() =>
                                {
                                    __NewWords.ForEach(__Item =>
                                    {
                                        LanguageDataManager.UpdateLanguageWord((long)__Item.ID, (string)__Item.Word);

                                    });
                                });

                                LanguageDataManager.RefreshLanguageFromDB(__DataService);

                                cLanguageItem __Item = App.Handlers.LanguageHandler.GetLanguageByCode(_Controller.ClientSession.Language);

                                WebGraph.ActionGraph.SetClientLanguageAction.Action(_Controller, new cSetClientLanguageProps() { Language = __Item.LanguageObject, LanguageCode = _Controller.ClientSession.Language });



                                WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                                WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Success"), Message = _Controller.GetWordValue("OperationComplete"), ColorType = EColorTypes.Success, DurationMS = 2500 });
                            }
                            catch (Exception _Ex)
                            {

                                WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
                            }

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
