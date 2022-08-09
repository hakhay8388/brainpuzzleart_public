using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nChangeServiceStateCommand;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Integration.MicroServiceGraph.nMicroService;
using System;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nBatchJobListener
{
    public class cBatchJobListener : cBaseListener
        , IChangeServiceStateReceiver
    {
        public cBatchJobDataManager BatchJobDataManager { get; set; }
        public cBatchJobListener(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, cBatchJobDataManager _BatchJobDataManager)
          : base(_App, _MicroService, _WebGraph, _DataServiceManager)
        {
            WebGraph = _WebGraph;
            BatchJobDataManager = _BatchJobDataManager;
        }

        public void ReceiveChangeServiceStateData(cListenerEvent _ListenerEvent, IController _Controller, cChangeServiceStateCommandData _ReceivedData)
        {
            if (_Controller.ClientSession.IsLogined)
            {
                try
                {
                    cActorEntity __Actor = _Controller.ClientSession.User.Actor.GetValue();

                    if (__Actor.Roles.GetValue().Code == RoleIDs.Admin.Code)
                    {
                        IDataService __DataService = DataServiceManager.GetDataService();

                        cBatchJobEntity __BatchJobEntity = __DataService.Database.GetEntityByID<cBatchJobEntity>(_ReceivedData.BatchJobID);
                        if (__BatchJobEntity != null)
                        {
                            __DataService.Perform(() =>
                            {
                                __BatchJobEntity.State = EBatchJobState.GetByID(_ReceivedData.BatchJobState, EBatchJobState.Stopped).ID == EBatchJobState.Started.ID ? EBatchJobState.Stopped.ID : EBatchJobState.Started.ID;
                                __BatchJobEntity.Save();
                            });

                            WebGraph.ActionGraph.SuccessResultAction.Action(_Controller);
                        }
                        else
                        {
							WebGraph.ErrorMessageManager.ErrorAction(
								new Exception(__BatchJobEntity.ID + " ID li BatchJob Entity Bulunamadı..!"), 
								_Controller,
								_Controller.GetWordValue("Error"),
								_Controller.GetWordValue("UnknownError")
							);
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
