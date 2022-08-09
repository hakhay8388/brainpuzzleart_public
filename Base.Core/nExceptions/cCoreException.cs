using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Core.nApplication;

namespace Base.Core.nExceptions
{
    public class cCoreException : Exception
    {
        public cCoreException(cApp _App, string _Message)
            : base(_Message)
        {
            _App.Loggers.CoreLogger.LogError("Hata : {0}", StackTrace.ToString());
        }
    }
}
