using Base.Boundary.nValueTypes.nConstType;
using Base.Core.nApplication;
using Base.Core.nHandlers.nLanguageHandler;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.BatchJobService.nBatchJobManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nLanguageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nResultListAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nSetServerDateTimeAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nShowMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDeleteFileCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetActionListCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetCommandListCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetEnumVariableListCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetGlobalParamListCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetMenuListCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetNotificationsCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetServerDateTimeCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nReadNotificationCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nReloadConfigBackupCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveConfigBackupCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSetLanguageCommand;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nGeneralListener
{
    public class cGeneralListener : cBaseListener
        , IGetCommandListReceiver
        , IGetActionListReceiver
        , IGetMenuListReceiver
        , ISetLanguageReceiver
        , IGetEnumVariableListReceiver
        , IGetServerDateTimeReceiver
        , IGetGlobalParamListReceiver
        , IDeleteFileReceiver
        , IGetNotificationsReceiver
        , IReadNotificationReceiver
        , ISaveConfigBackupReceiver
        , IReloadConfigBackupReceiver
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cFileDataManager FileDataManager { get; set; }
        public cNotificationDataManager NotificationDataManager { get; set; }

        public cUserDataManager UserDataManager { get; set; }

        public cActorDataManager ActorDataManager { get; set; }

        public cLanguageDataManager LanguageDataManager { get; set; }
        public IManagers Managers { get; set; }
        public cBatchJobManager BatchJobManager { get; set; }
        public cPageDataManager PageDataManager { get; set; }

        public cSessionDataManager SessionDataManager { get; set; }

        public cGeneralListener(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, cRoleDataManager _RoleDataManager, cFileDataManager _FileDataManager, cNotificationDataManager _NotificationDataManager
            , cUserDataManager _UserDataManager, cActorDataManager _ActorDataManager
            , cLanguageDataManager _LanguageDataManager
            , IManagers _Managers
            , cBatchJobManager _BatchJobManager
            , cUserTempDataManager _UserTempDataManager
            , cPageDataManager _PageDataManager
            , cSessionDataManager _SessionDataManager

            )
          : base(_App, _MicroService, _WebGraph, _DataServiceManager)
        {
            WebGraph = _WebGraph;
            RoleDataManager = _RoleDataManager;
            FileDataManager = _FileDataManager;
            NotificationDataManager = _NotificationDataManager;
            ActorDataManager = _ActorDataManager;
            UserDataManager = _UserDataManager;
            LanguageDataManager = _LanguageDataManager;
            Managers = _Managers;
            BatchJobManager = _BatchJobManager;
            PageDataManager = _PageDataManager;
            SessionDataManager = _SessionDataManager;
        }

        public void ReceiveGetCommandListData(cListenerEvent _ListenerEvent, IController _Controller, cGetCommandListCommandData _ReceivedData)
        {
            WebGraph.ActionGraph.CommandListAction.Action(_Controller);
        }

        public void ReceiveGetActionListData(cListenerEvent _ListenerEvent, IController _Controller, cGetActionListCommandData _ReceivedData)
        {
            WebGraph.ActionGraph.ActionListAction.Action(_Controller);
        }

        public void ReceiveGetMenuListData(cListenerEvent _ListenerEvent, IController _Controller, cGetMenuListCommandData _ReceivedData)
        {

        }

        public void ReceiveSetLanguageData(cListenerEvent _ListenerEvent, IController _Controller, cSetLanguageCommandData _ReceivedData)
        {
            if (string.IsNullOrEmpty(_ReceivedData.LanguageCode))
            {
                if (_Controller.ClientSession.IsLogined)
                {
                    _ReceivedData.LanguageCode = _Controller.ClientSession.Language;
                }
                else
                {
                    _ReceivedData.LanguageCode = App.Handlers.LanguageHandler.LanguageNameList[0].Code;
                }
            }
            cLanguageItem __LanguageItem = App.Handlers.LanguageHandler.GetLanguageByCode(_ReceivedData.LanguageCode);
            List<string> __DefinedLanguages = new List<string>();
            foreach (KeyValuePair<string, cLanguageItem> __LanguageItemDictionary in App.Handlers.LanguageHandler.LanguageList)
            {
                __DefinedLanguages.Add(__LanguageItemDictionary.Key);
            }
            if (__LanguageItem != null)
            {
                if (_Controller.ClientSession.IsLogined)
                {
                    DataServiceManager.GetDataService().Perform(() =>
                    {
                        _Controller.ClientSession.User.Language = _ReceivedData.LanguageCode;
                        _Controller.ClientSession.User.Save();
                    });
                }

                WebGraph.ActionGraph.LanguageAction.Action(_Controller, new cLanguageProps() { Language = __LanguageItem.LanguageObject, LanguageCode = _ReceivedData.LanguageCode, DefinedLanguages = __DefinedLanguages });
            }
            else
            {
                WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = _Controller.GetWordValue("Error"), Message = _Controller.GetWordValue("LanguageNotFound", _ReceivedData.LanguageCode) });
            }
        }

        public void ReceiveGetEnumVariableListData(cListenerEvent _ListenerEvent, IController _Controller, cGetEnumVariableListCommandData _ReceivedData)
        {
            List<Type> __Types = App.Handlers.AssemblyHandler.GetTypesFromBaseInterface<IConstTypeType>();
            __Types = __Types.Where(__Item => __Item.Name == _ReceivedData.EnumTypeName).ToList();


            System.Collections.IList __List = (System.Collections.IList)__Types[0].GetStaticPropertyValue("TypeList");

            WebGraph.ActionGraph.ResultListAction.Action(_Controller, new nActionGraph.nActions.nResultListAction.cResultListProps() { ResultList = __List, Page = 1, Total = __Types.Count });
        }

        public void ReceiveGetServerDateTimeData(cListenerEvent _ListenerEvent, IController _Controller, cGetServerDateTimeCommandData _ReceivedData)
        {
            WebGraph.ActionGraph.SetServerDateTimeAction.Action(_Controller, new cSetServerDateTimeProps() { ServerDate = App.Handlers.DateTimeHandler.Now });
        }

        public void ReceiveGetGlobalParamListData(cListenerEvent _ListenerEvent, IController _Controller, cGetGlobalParamListCommandData _ReceivedData)
        {
            cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            WebGraph.ActionGraph.ResultListAction.Action(_Controller, new cResultListProps() { ResultList = __DataService.PublicParamList, Page = 1, Total = __DataService.PublicParamList.Count });

        }

        public void ReceiveDeleteFileData(cListenerEvent _ListenerEvent, IController _Controller, cDeleteFileCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();
                    cActorType_SellerDetailEntity __SellerDetail = __Actor.SellerDetail;

                    cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
                    __DataService.Perform(() =>
                    {

                        cFileEntity __File = FileDataManager.GetFileByID(_ReceivedData.FileID);
                        App.Handlers.FileHandler.DeleteFileIfExists(Path.Combine(App.Configuration.UserFilePath, __File.FileName));
                        FileDataManager.DeleteSellerFileByID(__SellerDetail, _ReceivedData.FileID);
                    });

                    WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                    WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Success"), Message = _Controller.GetWordValue("FileDeleted"), ColorType = EColorTypes.Success, DurationMS = 2500 });
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

        public void ReceiveGetNotificationsData(cListenerEvent _ListenerEvent, IController _Controller, cGetNotificationsCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    List<dynamic> __List = WebGraph.NotificationManager.GetLastNotifications(_Controller.ClientSession.User.Actor.GetValue());

                    WebGraph.ActionGraph.ResultListAction.Action(_Controller, new cResultListProps() { ResultList = __List, Page = 1, Total = __List.Count });
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
        public void ReceiveReadNotificationData(cListenerEvent _ListenerEvent, IController _Controller, cReadNotificationCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cNotificationActorDetailEntity __NotificationActorDetailEntity = NotificationDataManager.GetNotificationByIDAndActorID(_Controller.ClientSession.User.Actor.GetValue().ID, _ReceivedData.NotificationID);
                    if (__NotificationActorDetailEntity != null)
                    {
                        cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
                        __DataService.Perform(() =>
                        {
                            __NotificationActorDetailEntity.Readed = true;
                            __NotificationActorDetailEntity.Save();
                        });
                    }
                    WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
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

        public void ReceiveSaveConfigBackupData(cListenerEvent _ListenerEvent, IController _Controller, cSaveConfigBackupCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();
                if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                {
                    try
                    {
                        Managers.ConfigBackupManager.DoItBackup();
                        WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Operation"), Message = _Controller.GetWordValue("OperationComplete"), ColorType = EColorTypes.Success, DurationMS = 2500 });
                    }
                    catch (Exception _Ex)
                    {
                        App.Loggers.CoreLogger.LogError(_Ex);
                        WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Operation"), Message = _Controller.GetWordValue("OperationFailed"), ColorType = EColorTypes.Error, DurationMS = 2500 });
                    }
                }
                else
                {
                    WebGraph.ActionGraph.NoPermissionAction.Action(_Controller);
                }
            }
        }

        public void ReceiveReloadConfigBackupData(cListenerEvent _ListenerEvent, IController _Controller, cReloadConfigBackupCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();
                if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                {

                    try
                    {
                        if (_ReceivedData.ConfigList.Count == 0)
                        {
                            WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Operation"), Message = _Controller.GetWordValue("PleaseSelectAtleast1value"), ColorType = EColorTypes.Error, DurationMS = 2500 });
                            return;
                        }

                        Managers.ConfigBackupManager.ReloadBackup(_ReceivedData.Path, _ReceivedData.ConfigList);
                        WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Operation"), Message = _Controller.GetWordValue("OperationComplete"), ColorType = EColorTypes.Success, DurationMS = 2500 });
                        WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                    }
                    catch (Exception _Ex)
                    {
                        App.Loggers.CoreLogger.LogError(_Ex);
                        WebGraph.ActionGraph.HotSpotMessageAction.Action(_Controller, new cHotSpotProps() { Header = _Controller.GetWordValue("Operation"), Message = _Controller.GetWordValue("OperationFailed"), ColorType = EColorTypes.Error, DurationMS = 2500 });
                    }
                }
                else
                {
                    WebGraph.ActionGraph.NoPermissionAction.Action(_Controller);
                }
            }
        }
    }
}
