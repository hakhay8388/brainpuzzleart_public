using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Base.Core.nApplication;


using Integration.MicroServiceGraph.nUtils.nJsonConverter;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceIDs;
using Integration.MicroServiceGraph.nMicroService;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph
{
    public class cMicroServiceGenericCommand<TActionProps> : cBaseMicroServiceCommand<TActionProps>
		where TActionProps : cMicroServiceBaseProps
	{
        List<cRecieverItem> ReceiverList = new List<cRecieverItem>();
        Type CommandDataClass = null;
        Type CommandReceiverClass = null;

        public cMicroServiceGenericCommand(cApp _App, IMicroService _MicroService, MicroServiceIDs _MicroServiceID)
                : base(_App, _MicroService, _MicroServiceID)
        {
            try
            {

				CommandDataClass = typeof(TActionProps);
				CommandReceiverClass = Type.GetType("Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceCommandGraph.nMicroServiceCommands.nMicroService"+ MicroServiceID.Name + "Command.IMicroService" + MicroServiceID.Name + "Receiver");
            }
            catch (Exception _Ex)
            {
				App.Loggers.CoreLogger.LogError(_Ex);
				Console.WriteLine(MicroServiceID.Name + " MicroService Komutunun Data Class'ı yada Receiver Interfaci Oluşturulmamış..!");
                Console.WriteLine(_Ex.StackTrace);
            }
        }

		public override void Interpret(cMicroServiceApi _WebApi, JToken _JToken)
		{
			JsonSerializerSettings __Settings = new JsonSerializerSettings
			{
				Converters = new List<JsonConverter> { new cBadDateFixingConverter() },
				DateParseHandling = DateParseHandling.None
			};

			Object __Data = JsonConvert.DeserializeObject(_JToken.ToString(), CommandDataClass, __Settings);

			ReceiverList = ReceiverList.OrderBy(__Item => __Item.Order).ToList();
			cMicroServiceListenerEvent __Event = new cMicroServiceListenerEvent(this);


			if (!__Event.IsPropogationStoped)
			{
				for (int i = 0; i < ReceiverList.Count; i++)
				{
					Object __ReceiverObject = ReceiverList[i].Receiver;
					try
					{
						MethodInfo __Receiver = CommandReceiverClass.GetMethod("ReceiveMicroService" + MicroServiceID.Name + "Data", 0, new Type[] { typeof(cMicroServiceListenerEvent), typeof(cMicroServiceApi), __Data.GetType() });
						__Receiver.Invoke(__ReceiverObject, new object[] { __Event, _WebApi, __Data });
						if (__Event.IsPropogationStoped)
						{
							break;
						}
					}
					catch (Exception _Ex)
					{
						Console.WriteLine(_Ex.StackTrace);
						Console.WriteLine("IMicroService" + MicroServiceID.Name + "Receiver Class'ının içinde ReceiveMicroService" + MicroServiceID.Name + "Data methodunda sıkıntı var..!");
					}
				}
			}
		}


        public void Connect(Object _Receiver, int _Order = 0)
        {
            ReceiverList.Add(new cRecieverItem(_Receiver, _Order));
        }

        public void Disconnect(Object _Receiver)
        {
            cRecieverItem __RecieverItem = ReceiverList.Where(__Item => __Item.Receiver == _Receiver).ToList().FirstOrDefault();
            if (__RecieverItem != null)
            {
                ReceiverList.Remove(__RecieverItem);
            }            
        }
    }
}
