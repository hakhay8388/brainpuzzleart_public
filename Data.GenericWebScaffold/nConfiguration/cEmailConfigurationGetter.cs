using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nApplication;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Core.nHandlers.nEmailHandler;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nConfiguration
{
    [Register(typeof(IEmailConfiguration), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cEmailConfigurationGetter : cCoreObject, IEmailConfiguration
    {
        cEmailConfiguration EmailConfiguration { get; set; }
        IDataServiceManager DataServiceManager { get; set; }
        public cEmailConfigurationGetter(cApp _App, IDataServiceManager _DataServiceManager)
            : base(_App)
        {
            DataServiceManager = _DataServiceManager;
        }
        public cEmailConfiguration GetEmailConfiguration()
        {
            cGenericWebScaffoldDataService __GenericWebScaffoldDataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            __GenericWebScaffoldDataService.LoadGlobalParams();
            EmailConfiguration = new cEmailConfiguration(App, __GenericWebScaffoldDataService.SmtpHost, __GenericWebScaffoldDataService.SmtpPort, __GenericWebScaffoldDataService.SmtpUsername, __GenericWebScaffoldDataService.SmtpPassword, __GenericWebScaffoldDataService.EmailDisplayName, __GenericWebScaffoldDataService.SmtpSslEnabled, __GenericWebScaffoldDataService.MailFrom);
            return EmailConfiguration;
        }
    }
}
