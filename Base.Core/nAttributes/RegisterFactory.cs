using Base.Boundary.nCore.nObjectLifeTime;

using System;

namespace Base.Core.nAttributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class RegisterFactory : Attribute
    {
        public Type BindFrom { get; private set; }
        public string FuctionName { get; private set; }
        public LifeTime LifetimeManager { get; private set; }

        public RegisterFactory(Type _BindFrom = null, string _FuctionName = "", LifeTime _LifetimeManager = LifeTime.ContainerControlledLifetimeManager)
        {
            BindFrom = _BindFrom;
            FuctionName = _FuctionName;
            LifetimeManager = _LifetimeManager;
        }
    }
}
