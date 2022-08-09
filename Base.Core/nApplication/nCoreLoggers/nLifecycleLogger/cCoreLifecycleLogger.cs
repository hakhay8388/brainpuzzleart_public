using System;
using System.IO;

namespace Base.Core.nApplication.nCoreLoggers.nLifecycleLogger
{
    public class cCoreLifecycleLogger : cBaseLogger
    {
        public cCoreLifecycleLogger(cApp _App)
            : base(_App)
        {
        }
        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cCoreLifecycleLogger>(this);
        }

        protected override string LogPath()
        {
            return App.Configuration.LifecycleLoggerPath;
		}
		protected override bool IsEnabled()
		{
			return App.Configuration.LogLifecycleEnabled;
		}
	}
}
