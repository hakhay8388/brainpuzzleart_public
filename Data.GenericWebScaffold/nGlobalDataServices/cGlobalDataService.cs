using Base.Boundary.nCore.nBootType;
using Base.Boundary.nCore.nObjectLifeTime;
using Base.Boundary.nData;
using Base.Core.nAttributes;
using Base.Data.nConfiguration;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nGlobalDataServices.nDataManagers;
using Data.GenericWebScaffold.nGlobalDataServices.nEntityServices.nEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nGlobalDataServices
{
    [Register(typeof(IGlobalDataService), false, false, false, false, LifeTime.PerResolveLifetimeManager)]
    public class cGlobalDataService : cDataService<cGlobalDataServiceContext, cBaseGlobalEntity>, IGlobalDataService
    {
        public cGlobalDataService(cGlobalDataServiceContext _CoreServiceContext)
            : base(_CoreServiceContext)
        {
            LoadDB(ServiceContext.Configuration.DBVendor, ServiceContext.Configuration.DBServer, ServiceContext.Configuration.DBUserName, ServiceContext.Configuration.DBPassword, ServiceContext.Configuration.GlobalDBName, ServiceContext.Configuration.MaxConnectCount);
        }

        public override void SetDBParams(string _Database)
        {
            Database.Catalogs.DatabaseOperationsSQLCatalog.SetDbLevelParams(_Database);
        }
        public string GetHostName()
        {
            string __HostName = "";
            if (App.Cfg<cDataConfiguration>().BootType == EBootType.Console
              || App.Cfg<cDataConfiguration>().BootType == EBootType.Batch)
            {
                __HostName = App.Cfg<cDataConfiguration>().TargetHostName;
            }
            else
            {
                __HostName = App.Handlers.ContextHandler.CurrentContextItem.Context.Request.Host.Host;
                __HostName = App.Handlers.StringHandler.GetRootDomain(__HostName);
            }
            return __HostName;
        }
        public string GetProxyedSiteUrl()
        {
            string __HostName = "";
            if (App.Cfg<cDataConfiguration>().BootType == EBootType.Console
              || App.Cfg<cDataConfiguration>().BootType == EBootType.Batch)
            {
                __HostName = App.Cfg<cDataConfiguration>().ProxyedSiteUrl;
            }
            else
            {
                __HostName = App.Handlers.ContextHandler.CurrentContextItem.Context.Request.Host.Host;
                __HostName = App.Handlers.StringHandler.GetRootDomain(__HostName);
            }
            return __HostName;
        }
        public void LockPofile(Action _ServiceMethod)
        {
            cProfileDataManager __ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();

            string __HostName = GetHostName();

            cProfileEntity __Profile = __ProfileDataManager.GetProfileByHostName(__HostName);

            App.Loggers.SqlLogger.LogInfo("Profile Lock Begin (Locked Profile)");

            this.Perform(() =>
            {
                __Profile.LockAndRefresh();
                _ServiceMethod.Invoke();
            });

            App.Loggers.SqlLogger.LogInfo("Profile Lock End (Unlocked Profile)");
        }

        public bool IsProfileLocked()
        {
            string __HostName = "";
            if (App.Cfg<cDataConfiguration>().BootType == EBootType.Console
              || App.Cfg<cDataConfiguration>().BootType == EBootType.Batch)
            {
                __HostName = App.Cfg<cDataConfiguration>().TargetHostName;
            }
            else
            {
                __HostName = App.Handlers.ContextHandler.CurrentContextItem.Context.Request.Host.Host;
                __HostName = App.Handlers.StringHandler.GetRootDomain(__HostName);
            }

            cProfileDataManager __ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();
            cProfileEntity __Profile = __ProfileDataManager.GetProfileByHostName(__HostName);
            return __Profile.IsLocked();
        }

        public void LockPofileByHostName(string _HostName, Action _ServiceMethod)
        {
            cProfileDataManager __ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();

            cProfileEntity __Profile = __ProfileDataManager.GetProfileByHostName(_HostName);

            App.Loggers.SqlLogger.LogInfo("Profile Lock Begin (Locked Profile)");

            this.Perform(() =>
            {
                __Profile.LockAndRefresh();
                _ServiceMethod.Invoke();
            });

            App.Loggers.SqlLogger.LogInfo("Profile Lock End (Unlocked Profile)");
        }

        public bool IsProfileLockedByHost(string _HostName)
        {
            cProfileDataManager __ProfileDataManager = App.Factories.ObjectFactory.ResolveInstance<cProfileDataManager>();
            cProfileEntity __Profile = __ProfileDataManager.GetProfileByHostName(_HostName);
            return __Profile.IsLocked();
        }

        public override void InvokeTransactionalAction(Func<bool> _ServiceMethod)
        {
            try
            {
                List<MethodBase> __Methods = App.Handlers.StackHandler.GetMethods("InvokeTransactionalAction", 0);

                if (__Methods.Where(__Item => __Item.DeclaringType.Name == this.GetType().Name).ToList().Count < 2)
                {
                    if (_ServiceMethod())
                    {
                        Database.DefaultConnection.Commit();
                    }
                    else
                    {
                        Database.DefaultConnection.Rollback();
                    }
                }
                else
                {
                    throw new Exception("ic ice aclilan transation mevcut..!");
                }
            }
            catch (Exception ex)
            {
                App.Loggers.SqlLogger.LogError(ex);
                Database.DefaultConnection.Rollback();
                throw;
            }
        }
    }
}
