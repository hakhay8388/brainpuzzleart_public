using System;
using System.IO;

namespace Base.Core.nApplication.nCoreLoggers.nWebHooksLogger
{
    public class cWebHooksLogger : cBaseLogger
    {
        public cWebHooksLogger(cApp _App)
            : base(_App)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cWebHooksLogger>(this);
        }

        protected override string LogPath()
        {
            return App.Configuration.WebHooksLoggerPath;
		}
		protected override bool IsEnabled()
		{
			return App.Configuration.LogWebHooksEnabled;
		}
	}
}
