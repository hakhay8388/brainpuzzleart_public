using Base.Core.nApplication;
using Base.Data.nDataServiceManager;
using Integration.MicroServiceGraph;
using Integration.MicroServiceGraph.nMicroService;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceNotificationAction;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceTestMessageAction;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph.nMicroServiceCommands.nMicroServiceNotificationCommand;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph.nMicroServiceCommands.nMicroServiceTestMessageCommand;
using System;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nMicroServiceListeners
{
    public class cMicroServiceListeners : cBaseListener
        , IMicroServiceNotificationReceiver
		, IMicroServiceTestMessageReceiver
	{
        public cMicroServiceListeners(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager)
            : base(_App, _MicroService, _WebGraph, _DataServiceManager)
        {
        }

		public void ReceiveMicroServiceNotificationData(cMicroServiceListenerEvent _ListenerEvent, cMicroServiceApi _MicroServiceApi, cMicroServiceNotificationProps _ReceivedData)
		{
			MicroService.MicroServiceActionGraph.TestMessageAction.ResultAction(_MicroServiceApi, new cMicroServiceTestMessageProps("Helallll.. :)"));

			//MicroService.MicroServiceActionGraph.NotificationAction.ResultAction(_MicroServiceApi,
		    //new cMicroServiceNotificationProps(new List<long>() { 10 }, 1, ENotificationChannel.GlobalChannel, ENotificationType.CancelLessonsNotification, new { Test = "Test Dönüşü" }));

			//MicroService.MicroServiceActionGraph.NotificationAction.ResultAction(_MicroServiceApi,
			//new cMicroServiceNotificationProps(new List<long>() { 10 }, 1, ENotificationChannel.GlobalChannel, ENotificationType.CancelLessonsNotification, new { Test = "Test Dönüşü" }));

			/*cSessionManager __SessionManager = WebGraph.SessionManagerServices.GetSessionManagerByHost(_ReceivedData.Host);
			if (__SessionManager != null)
			{
				List<cSession> __Sessions = __SessionManager.GetSessionByActorID(_ReceivedData.ActorIDs).Where(__Item => __Item.SignalRIDList.Count > 0).ToList();
				cNotificationProps __NotificationActionProps = new cNotificationProps(_ReceivedData.NotificationID, ENotificationChannel.GetByID(_ReceivedData.ChannelID, ENotificationChannel.GlobalChannel), ENotificationType.GetByID(_ReceivedData.Type, ENotificationType.None), _ReceivedData.ParameterObjects);

				if (__Sessions.Count > 0)
				{
					WebGraph.ActionGraph.NotificationAction.Action(this, __NotificationActionProps, __Sessions);
					WebGraph.ActionGraph.HotSpotMessageAction.Action(this, new cHotSpotProps() { Header = "Başlık", Message = ((dynamic)_ReceivedData.ParameterObjects).Test, ColorType = EColorTypes.Warning, DurationMS = 1000 }, __Sessions);
				}
			}	*/
		}

		public void ReceiveMicroServiceTestMessageData(cMicroServiceListenerEvent _ListenerEvent, cMicroServiceApi _MicroServiceApi, cMicroServiceTestMessageProps _ReceivedData)
		{
			Console.WriteLine(_ReceivedData.Message);
		}
	}
}