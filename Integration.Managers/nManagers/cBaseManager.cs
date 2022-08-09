using Base.Core.nApplication;
using Base.Core.nCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Integration.Managers.nManagers
{
    public class cBaseManager : cCoreObject
    {
        public IManagers Managers { get; set; }
        public cBaseManager(cApp _App) : base(_App)
        {
        }
        
    }
}
