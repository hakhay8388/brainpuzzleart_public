using Base.Core.nApplication;
using Base.Core.nCore;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nCacheItAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph
{

    public class cCommandGraph : cCoreObject
	{
		public List<cBaseCommand> CommandList { get; set; }
		public cWebGraph WebGraph { get; set; }

		public IDataServiceManager DataServiceManager { get; set; }


		public cCommandGraph(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager)
			: base(_App)
		{
			CommandList = new List<cBaseCommand>();
			WebGraph = _WebGraph;
			DataServiceManager = _DataServiceManager;
		}


		public override void Init()
		{
			/*cLoginCommand __LoginCommand = new cLoginCommand();
            CommandList.Add(__LoginCommand);*/
			for (int i = 0; i < CommandIDs.TypeList.Count; i++)
			{
				cBaseCommand __Command = new cGenericCommand(App, WebGraph, CommandIDs.TypeList[i]);
				CommandList.Add(__Command);
			}
		}

		public cBaseCommand GetCommandByID(CommandIDs _CommandID)
		{
			return CommandList.Find(__Item => __Item.CommandID.ID == _CommandID.ID);
		}

		protected void Log(CommandIDs _CommandID, decimal _ElapsedTime, JToken _Data)
		{
			cApp __App = cApp.App;
			List<string> __BulkLog = new List<string>();
			__BulkLog.Add("/////////////////////////////////// ");
			__BulkLog.Add("RequestID : " + __App.Handlers.ContextHandler.CurrentContextItem.RequestID);
			__BulkLog.Add("Requested Command : " + _CommandID.Code);
			__BulkLog.Add("ElapsedTime : " + _ElapsedTime);
			__BulkLog.Add("Data : " + _Data.ToString());
			__BulkLog.Add("/////////////////////////////////// ");
			__App.Loggers.RequestPerformanceLogger.LogInfo(__BulkLog);
		}


		public void InterpreterCommand(IController _Controller)
		{
			try
			{
				bool __IsUsableForMe = false;
				bool __CommandFound = false;

				if (_Controller.CommandJson.ContainsKey("CID"))
				{
					int __CID = (int)_Controller.CommandJson["CID"];
					CommandIDs __CommandID = CommandIDs.GetByID(__CID, null);

					__IsUsableForMe = _Controller.ClientSession.IsUsableForMe(__CommandID);

					if (__IsUsableForMe)
					{
						if (_Controller.CommandJson.ContainsKey("Data"))
						{
							JToken __Data = _Controller.CommandJson["Data"];
							for (int i = 0; i < CommandList.Count; i++)
							{
								if (CommandList[i].CommandID.ID == __CID)
								{
									Stopwatch __StopWatch = new Stopwatch();
									__StopWatch.Start();

									__CommandFound = true;
									CommandList[i].Interpret(_Controller, __Data);
									if (CommandList[i].CommandID.CacheIt)
									{
										JArray __ResultActions = _Controller.ActionJson;
										_Controller.SignalSessions = new List<cSignalSessionMatcher>();
										_Controller.ActionJson = new JArray();
										WebGraph.ActionGraph.CacheItAction.Action(_Controller, new cCacheItProps() { CacheActionList = __ResultActions });
									}


									__StopWatch.Stop();
									// Get the elapsed time as a TimeSpan value.
									TimeSpan __TimeSpan = __StopWatch.Elapsed;

									decimal __ElapsedTime = Convert.ToDecimal(__TimeSpan.TotalMilliseconds) / 1000;

									if (__ElapsedTime > 2)
									{
										Log(CommandList[i].CommandID, __ElapsedTime, __Data);
									}


									return;
								}
							}
						}
					}
					else
					{
						//WebGraph.ActionGraph.ShowMessageAction.ErrorAction(_Controller, new cMessageProps() { Header = "Hata", Message = "Bu komut için yetkiniz bulunmuyor!" });
						if (!__CommandID.MainRoles.Exists(__Item => __Item.ID == RoleIDs.Unlogined.ID) && !_Controller.ClientSession.IsLogined)
						{
							WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
						}
						else
						{
							WebGraph.ErrorMessageManager.ErrorAction(new Exception("NoPermissionAction...."), _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("NoPermission"));
							WebGraph.ActionGraph.LogInOutAction.Action(_Controller);
						}
					}
				}

				if (!__CommandFound && __IsUsableForMe)
				{
					try
					{
						App.Loggers.CoreLogger.LogError("Command Not Found....");
						App.Loggers.CoreLogger.LogError(_Controller.CommandJson.ToString());
						WebGraph.ErrorMessageManager.ErrorAction(new Exception("Command Not Found...."), _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("WrongCommand"));
					}
					catch(Exception _Ex)
					{
					}
				}

			}
			catch (Exception _Ex)
			{
				WebGraph.ErrorMessageManager.ErrorAction(_Ex, _Controller, _Controller.GetWordValue("Error"), _Controller.GetWordValue("UnknownError"));
			}
			//cLoginCommandData __LoginData = JsonConvert.DeserializeObject<cLoginCommandData>(_Session.CommandJson["Data"].ToString()); 
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
			Dictionary<Type, int> __ListenerOrders = new Dictionary<Type, int>();
			if (typeof(cBaseListener).IsAssignableFrom(_Object.GetType()))
			{
				__ListenerOrders = ((cBaseListener)_Object).ListenerOrders;
			}


			Type[] __Interfaces = _Object.GetType().GetInterfaces();
			for (int i = 0; i < __Interfaces.Length; i++)
			{
				for (int j = 0; j < CommandList.Count; j++)
				{
					cBaseCommand __Command = CommandList[j];
					if (__Interfaces[i].Name == "I" + __Command.CommandID.Name + "Receiver")
					{
						try
						{
							if (_MethodName == "Connect")
							{
								MethodInfo __Method = __Command.GetType().GetMethod(_MethodName, new Type[] { typeof(object), typeof(int) });
								if (__ListenerOrders.ContainsKey(__Interfaces[i]))
								{
									__Method.Invoke(__Command, new object[] { _Object, __ListenerOrders[__Interfaces[i]] });
								}
								else
								{
									__Method.Invoke(__Command, new object[] { _Object, 0 });
								}

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
