/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using System.Threading.Tasks;

namespace Base.Core.nApplication.nCoreLoggers.nMethodCallLogger
{
    public class cMethodLoggingProxy : RealProxy
    {
        readonly object m_Target;

        public cApp App { get; set; }
        public cMethodLoggingProxy(cApp _App, object _Target)
            : base(_Target.GetType())
        {
            App = _App;
            this.m_Target = _Target;
        }

        public override IMessage Invoke(IMessage _Msg)
        {
            var __MethodCall = _Msg as IMethodCallMessage;
            if (__MethodCall != null)
            {
                return HandleMethodCall(__MethodCall); // <- see further
            }
            return null;
        }

        IMessage HandleMethodCall(IMethodCallMessage _MethodCall)
        {
            try
            {
                var __Watch = System.Diagnostics.Stopwatch.StartNew();
                var __Result = _MethodCall.MethodBase.Invoke(m_Target, _MethodCall.InArgs);
                __Watch.Stop();
                var __ElapsedMs = __Watch.ElapsedMilliseconds;

                if (App.Loggers != null ) App.Loggers.CoreLogger.LogInfo("Called {0}.{1} : Duration {2} ms", m_Target.GetType().Name, _MethodCall.MethodName, __ElapsedMs.ToString());
                return new ReturnMessage(__Result, null, 0, _MethodCall.LogicalCallContext, _MethodCall);
            }
            catch (TargetInvocationException invocationException)
            {
                var __Exception = invocationException.InnerException;
                if (App.Loggers != null) App.Loggers.CoreLogger.LogError("Calling {0}.{1}... {2}", m_Target.GetType().Name, _MethodCall.MethodName, __Exception.GetType());
                return new ReturnMessage(__Exception, _MethodCall);
            }
        }
    }
}
*/