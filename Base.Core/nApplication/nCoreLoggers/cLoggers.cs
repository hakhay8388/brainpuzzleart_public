using Base.Core.nApplication.nCoreLoggers.nBatchJobLogger;
using Base.Core.nApplication.nCoreLoggers.nCoreLogger;
using Base.Core.nApplication.nCoreLoggers.nIntegrationLogger;
using Base.Core.nApplication.nCoreLoggers.nRequestPerformanceLogger;
using Base.Core.nApplication.nCoreLoggers.nSqlLogger;
using Base.Core.nApplication.nCoreLoggers.nWebHooksLogger;
using Base.Core.nAttributes;
using Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Core.nApplication.nCoreLoggers
{
    public class cLoggers :cCoreObject
    {
        public cCoreLogger CoreLogger {get;set;}

        public cCoreSqlLogger SqlLogger { get; set; }

		public cCoreRequestPerformanceLogger RequestPerformanceLogger { get; set; }

		public cCoreBatchJobLogger BatchJobLogger { get; set; }

		public cWebHooksLogger WebHooksLogger { get; set; }

		public cIntegrationLogger IntegrationLogger { get; set; }

		public cLoggers(cApp _App)
            :base(_App)
        {
            CoreLogger = new cCoreLogger(_App);
            SqlLogger = new cCoreSqlLogger(_App);
            BatchJobLogger = new cCoreBatchJobLogger(_App);
			RequestPerformanceLogger = new cCoreRequestPerformanceLogger(_App);
			WebHooksLogger = new cWebHooksLogger(_App);
			IntegrationLogger = new cIntegrationLogger(_App);

		}

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cLoggers>(this);

            CoreLogger.Init();
            SqlLogger.Init();
            BatchJobLogger.Init();
			RequestPerformanceLogger.Init();
			WebHooksLogger.Init();
			IntegrationLogger.Init();
		}
    }
}
