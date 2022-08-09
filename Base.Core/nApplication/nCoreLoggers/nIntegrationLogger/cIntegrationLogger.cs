using System;
using System.IO;

namespace Base.Core.nApplication.nCoreLoggers.nIntegrationLogger
{
    public class cIntegrationLogger : cBaseLogger
    {
        public cIntegrationLogger(cApp _App)
            : base(_App)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cIntegrationLogger>(this);
        }

        protected override string LogPath()
        {
            return App.Configuration.IntegrationLoggerPath;
		}
		protected override bool IsEnabled()
		{
			return App.Configuration.LogIntegrationsEnabled;
		}
	}
}
