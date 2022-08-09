using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nApplication;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Integration.Managers.nManagers.nConfigBackupManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Managers.nManagers
{
    [Register(typeof(IManagers), false, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
    public class cManagers : cCoreObject, IManagers
    {
        public cConfigBackupManager ConfigBackupManager { get; set; }

        public cManagers(cApp _App) : base(_App)
        {            
        }
        public override void Init()
        {
            ConfigBackupManager = App.Factories.ObjectFactory.ResolveInstance<cConfigBackupManager>();
            ConfigBackupManager.Managers = this;
             
            ConfigBackupManager.Init();
        }

    }
}
