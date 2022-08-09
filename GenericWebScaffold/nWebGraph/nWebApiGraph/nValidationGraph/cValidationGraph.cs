using Base.Core.nApplication;
using Base.Core.nCore;
using Core.GenericWebScaffold.nUtils;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nValidationGraph
{
    public class cValidationGraph : cCoreObject
    {
        static List<cBaseValidation> ListenerList = null;

        public cValidationGraph(cApp _App)
            : base(_App)
        {
            ListenerList = new List<cBaseValidation>();
        }

        public void Init()
        {
            List<Type> __Listeners = App.Handlers.AssemblyHandler.GetTypesFromBaseType<cBaseValidation>();
            __Listeners.ForEach(__Type =>
            {
                cBaseValidation __Listener = (cBaseValidation)App.Factories.ObjectFactory.ResolveInstance(__Type);
                __Listener.Init();
                ListenerList.Add(__Listener);
            });
        }
        /*public TListenerType GetListenerByType<TListenerType>()
            where TListenerType : cBaseValidation
        {
            TListenerType __Listener = (TListenerType)ListenerList.Where(__Item => typeof(TListenerType).IsAssignableFrom(__Item.GetType())).FirstOrDefault();
            return __Listener;
        }
        */
        /*public TReceiverInterface GetValidationByReceiverInterface<TReceiverInterface>()
        {
            cBaseValidation __BaseValidation  = ListenerList.Where(__Item => typeof(TReceiverInterface).IsAssignableFrom(__Item.GetInterfaceType())).FirstOrDefault();
            if (__BaseValidation == null)
            {
                return default(TReceiverInterface);
            }
            else
            {
                TReceiverInterface __Result = (TReceiverInterface)__BaseValidation.CastObject();
                return __Result;
            }   
        }*/

        public object GetValidationByReceiverInterface(Type _ReceiverInterface)
        {
            cBaseValidation __BaseValidation = ListenerList.Where(__Item => _ReceiverInterface.IsAssignableFrom(__Item.GetInterfaceType())).FirstOrDefault();
            if (__BaseValidation == null)
            {
                return null;
            }
            else
            {
                object __Result = __BaseValidation.CastObject();
                return __Result;
            }
        }
    }
}
