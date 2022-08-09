using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.Core.nCore;
using Base.Core.nApplication;

using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceListenerGraph;
using System.Diagnostics;
using Base.Data.nDataServiceManager;
using Base.Data.nDataService;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceIDs;
using Integration.MicroServiceGraph.nMicroService;
using Base.Communication.nCommunicationService.nCommunicationComponents.nPublisherAndSubscriber.nPublisher.nSubscriber;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph
{

	public class cMicroServiceCommandGraph : cCoreObject, ISubscribeListener
	{
		public List<IMicroServiceCommand> CommandList { get; set; }
		public IMicroService MicroService { get; set; }

		public IDataServiceManager DataServiceManager { get; set; }

		cSubscriberNode SubscriberNode { get; set; }

		public cMicroServiceCommandGraph(cApp _App, IMicroService _MicroService, IDataServiceManager _DataServiceManager)
			: base(_App)
		{
			CommandList = new List<IMicroServiceCommand>();
			MicroService = _MicroService;
			DataServiceManager = _DataServiceManager;
		}

		public void ReceivePublishedData(cSubscriberNode _SubscriberNode, string _Message)
		{
			throw new NotImplementedException();
		}

		public override void Init()
		{
			/*cLoginCommand __LoginCommand = new cLoginCommand();
            CommandList.Add(__LoginCommand);*/
			for (int i = 0; i < MicroServiceIDs.TypeList.Count; i++)
			{
				Type __MicroServicePropClass = Type.GetType("Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroService" + MicroServiceIDs.TypeList[i].Name + "Action.cMicroService" + MicroServiceIDs.TypeList[i].Name + "Props");

				// generic List with no parameters
				Type __GenericCommandType = typeof(cMicroServiceGenericCommand<>);

				// To create a List<string>
				Type[] __Arguments = { __MicroServicePropClass };
				Type __TargetGenericCommand = __GenericCommandType.MakeGenericType(__Arguments);

				ConstructorInfo __GenericCommandConstructorInfo = __TargetGenericCommand.GetConstructor(new Type[]{ typeof(cApp), typeof(IMicroService), typeof(MicroServiceIDs)});

				IMicroServiceCommand __Command = (IMicroServiceCommand)__GenericCommandConstructorInfo.Invoke(new object[] { App, MicroService, MicroServiceIDs.TypeList[i] });
				CommandList.Add(__Command);
			}
		}

		public IMicroServiceCommand GetCommandByID(MicroServiceIDs _MicroServiceID)
		{
			return CommandList.Find(__Item => __Item.MicroServiceID.ID == _MicroServiceID.ID);
		}

		protected void Log(MicroServiceIDs _MicroServiceID, decimal _ElapsedTime, JToken _Data)
		{
			cApp __App = cApp.App;
			List<string> __BulkLog = new List<string>();
			__BulkLog.Add("/////////////////////////////////// ");
			__BulkLog.Add("RequestID : Microservice Request");
			__BulkLog.Add("Requested Command : " + _MicroServiceID.Name);
			__BulkLog.Add("ElapsedTime : " + _ElapsedTime);
			__BulkLog.Add("Data : " + _Data.ToString());
			__BulkLog.Add("/////////////////////////////////// ");
			__App.Loggers.RequestPerformanceLogger.LogInfo(__BulkLog);
		}


		public void InterpreterCommand(cMicroServiceApi _WebApi)
		{
			bool __CommandFound = false;

			foreach (JToken __CommandItemJToken in _WebApi.MicroServiceCommandJson)
			{
				JObject __CommandItem = (JObject)__CommandItemJToken;

				try
				{
					if (__CommandItem.ContainsKey("MicroServiceID"))
					{
						int __CID = (int)__CommandItem["MicroServiceID"]["ID"];
						MicroServiceIDs __CommandID = MicroServiceIDs.GetByID(__CID, null);
						if (__CommandItem.ContainsKey("Data"))
						{
							JToken __Data = __CommandItem["Data"];
							for (int i = 0; i < CommandList.Count; i++)
							{
								if (CommandList[i].MicroServiceID.ID == __CID)
								{
									Stopwatch __StopWatch = new Stopwatch();
									__StopWatch.Start();

									__CommandFound = true;
									CommandList[i].Interpret(_WebApi, __Data);

									__StopWatch.Stop();
									// Get the elapsed time as a TimeSpan value.
									TimeSpan __TimeSpan = __StopWatch.Elapsed;

									decimal __ElapsedTime = Convert.ToDecimal(__TimeSpan.TotalMilliseconds) / 1000;

									if (__ElapsedTime > 2)
									{
										Log(CommandList[i].MicroServiceID, __ElapsedTime, __Data);
									}


									return;
								}
							}
						}
					}
				}
				catch(Exception _Ex)
				{

				}
			}
		}

		public void DisconnectToCommands(Object _Object)
		{
			CommandMethods(_Object, "Disconnect");
		}

		public void ConnectToCommands(Object _Object)
		{
			CommandMethods(_Object, "Connect");
		}

		public void CommandMethods(Object _Object, string _MethodName)
		{


			Type[] __Interfaces = _Object.GetType().GetInterfaces();
			for (int i = 0; i < __Interfaces.Length; i++)
			{
				for (int j = 0; j < CommandList.Count; j++)
				{
					IMicroServiceCommand __Command = CommandList[j];
					if (__Interfaces[i].Name == "IMicroService" + __Command.MicroServiceID.Name + "Receiver")
					{
						try
						{
							if (_MethodName == "Connect")
							{
								MethodInfo __Method = __Command.GetType().GetMethod(_MethodName, new Type[] { typeof(object), typeof(int) });
								__Method.Invoke(__Command, new object[] { _Object, 0 });

							}
							else
							{
								MethodInfo __Method = __Command.GetType().GetMethod(_MethodName, new Type[] { typeof(object) });
								__Method.Invoke(__Command, new object[] { _Object });
							}
						}
						catch (Exception _Ex)
						{
							App.Loggers.CoreLogger.LogError(_Ex);
							Console.WriteLine(_Ex);
						}
					}
				}
			}
		}

		
	}

}
