using Base.Boundary.nCore.nBootType;
using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nApplication.nBootstrapper.nConventionOverrider;
using Base.Core.nApplication.nConfiguration;
using Base.Core.nApplication.nFactories;
using Base.Core.nApplication.nStarter;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Core.nHandlers.nAssemblyHandler;

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Base.Core.nApplication.nBootstrapper
{
    public class cBootstrapper : cCoreObject
    {
        public cBootstrapper(cApp _App)
            : base(_App)
        {
        }

        public override void Init()
        {
            App.Factories.ObjectFactory.RegisterInstance<cBootstrapper>(this);
        }

        private List<Type> GetTypesByNamespaceAndDeclerationType(Assembly _Assembly, string _NamespaceStartWith, bool _IsInterface, bool _IsAbstract, bool _IsClass, bool _IgnoreClassType)
        {
            Type[] __Array = _Assembly.GetTypes();
            if (_IgnoreClassType) return __Array.ToList();
            IEnumerable<Type> __Result = __Array.Where((_Type) =>
            {
                return _Type.IsInterface == _IsInterface && _Type.IsAbstract == _IsAbstract && _Type.IsClass == _IsClass && (!String.IsNullOrEmpty(_Type.Namespace) && _Type.Namespace.StartsWith(_NamespaceStartWith));
            });
            return __Result.ToList();
        }

        public void Init<TStarter>(List<cOverrideTypeItem> _Overrides = null) where TStarter : IStarter
        {
            LoadPluginDlls();
            RegisterAllTypes();
            OverrideConvention(_Overrides);
            InitInstanceFromObjectList(CreateInstanceFromRegisteredTypes());
            StartInvoker<TStarter>();
        }

        protected void LoadPluginDlls()
        {
            String __BinPath = App.Configuration.BinPath;
            foreach (string __DomainName in App.Configuration.DomainNames)
            {
                string[] __Plugins = App.Handlers.FileHandler.FindFileStartWith(__BinPath, __DomainName + ".", true);
                foreach (string __File in __Plugins)
                {
                    string __FileName = Path.GetFileName(__File);
                    App.Handlers.AssemblyHandler.IsInApplicationDomain(App.Configuration.DomainNames, __FileName);
                    {
                        App.Handlers.AssemblyHandler.LoadFromAssemblyPath(__File);
                    }
                }
            }
        }

        protected void OverrideConvention(List<cOverrideTypeItem> _Overrides)
        {
            if (_Overrides != null)
            {
                foreach (cOverrideTypeItem __Item in _Overrides)
                {
                    if (__Item.From.IsAssignableFrom(__Item.To))
                    {
                        App.Factories.ObjectFactory.RegisterType(__Item.From, __Item.To, __Item.LifetimeManager);
                    }
                    else
                    {
                        throw new Exception("Tip Register Edilemiyor: From " + __Item.From.Name + "==> To " + __Item.To.Name);
                    }
                }
            }
        }

        public int GetTypeInheritLevel(Type _Type, int _CurrentLevel)
        {
            if (_Type.BaseType == null)
            {
                return _CurrentLevel;
            }
            else
            {
                return GetTypeInheritLevel(_Type.BaseType, _CurrentLevel + 1);
            }
        }

        public List<cTypeInheritLevel> GetTypeInheritLevel(List<Type> _Types)
        {
            List<cTypeInheritLevel> __Result = new List<cTypeInheritLevel>();
            foreach (Type __Type in _Types)
            {
                __Result.Add(new cTypeInheritLevel(__Type, GetTypeInheritLevel(__Type, 0)));
            }
            __Result.Sort((_Item1, _Item2) => _Item1.InheritLevel.CompareTo(_Item2.InheritLevel));
            return __Result;
        }

        protected void RegisterAllTypes()
        {
            List<cDomainTypeList> __AllTypes = new List<cDomainTypeList>();
            foreach (string __DomainName in App.Configuration.DomainNames)
            {
                __AllTypes.Add(new cDomainTypeList(App, this, __DomainName));
                //__AllTypes = Enumerable.Union(__AllTypes, App.Handlers.AssemblyHandler.GetLoadedApplicationTypesByCustomAttribute<Register>(__DomainName)).ToList();
            }
            //List<cTypeInheritLevel> __SortedTypeList = GetTypeInheritLevel(__AllTypes);
            foreach (cDomainTypeList __DomainTypeList in __AllTypes) 
            {
                foreach (cTypeInheritLevel __TypeInheritLevel in __DomainTypeList.SortedTypeList)

                {
                    if (!App.Factories.ObjectFactory.IsRegistered(__TypeInheritLevel.Type))
                    {
                        Register __Register = (Register)__TypeInheritLevel.Type.GetCustomAttribute(typeof(Register));
                        RegisterFactory __RegisterFactory = (RegisterFactory)__TypeInheritLevel.Type.GetCustomAttribute(typeof(RegisterFactory));
                        if (__RegisterFactory == null)
                        {
                            if (__Register.BindFrom == null)
                            {
                                App.Factories.ObjectFactory.RegisterType(__TypeInheritLevel.Type, __TypeInheritLevel.Type, __Register.LifetimeManager);
                            }
                            else
                            {
                                if (__Register.BindFrom.IsAssignableFrom(__TypeInheritLevel.Type))
                                {
                                    App.Factories.ObjectFactory.RegisterType(__Register.BindFrom, __TypeInheritLevel.Type, __Register.LifetimeManager);
                                    if (__Register.BindThisAllBaseTypes)
                                    {
                                        BindAllBaseTypes(__TypeInheritLevel.Type.BaseType, __TypeInheritLevel.Type, __Register.LifetimeManager);
                                    }
                                }
                                else
                                {
                                    throw new Exception("Tip Register Edilemiyor: " + __TypeInheritLevel.Type.Name);
                                }
                            }
                        }
                    }
                }
            }

            /*__AllTypes = new List<Type>();
            foreach (string __DomainName in App.Configuration.DomainNames)
            {
                __AllTypes = Enumerable.Union(__AllTypes, App.Handlers.AssemblyHandler.GetLoadedApplicationTypesByCustomAttribute<RegisterFactory>(__DomainName)).ToList();
            }

            __SortedTypeList = GetTypeInheritLevel(__AllTypes);*/
            foreach (cDomainTypeList __DomainTypeList in __AllTypes)
            {
                foreach (cTypeInheritLevel __TypeInheritLevel in __DomainTypeList.RegisterFactorySortedTypeList)
                {
                    if (!App.Factories.ObjectFactory.IsRegistered(__TypeInheritLevel.Type))
                    {

                        RegisterFactory[] __RegisterFactory = (RegisterFactory[])__TypeInheritLevel.Type.GetCustomAttributes(typeof(RegisterFactory));

                        if (__RegisterFactory.Length > 0)
                        {
                            object __Intance = __TypeInheritLevel.Type.GetConstructor(new Type[] { typeof(cApp) }).Invoke(new object[] { App });

                            for (int i = 0; i < __RegisterFactory.Length; i++)
                            {
                                MethodInfo __MethodInfo = __TypeInheritLevel.Type.GetMethod(__RegisterFactory[i].FuctionName);

                                MethodInfo __RegisterFactoryMethod = App.Factories.ObjectFactory.GetType().SearchMethod("RegisterFactory");
                                __RegisterFactoryMethod = __RegisterFactoryMethod.MakeGenericMethod(new Type[] { __RegisterFactory[i].BindFrom });

                                object __TempObject = __RegisterFactoryMethod.Invoke(App.Factories.ObjectFactory, new object[] { GetFactory(__RegisterFactory[i].BindFrom, __Intance, __MethodInfo), __RegisterFactory[i].LifetimeManager });

                            }
                        }
                    }
                }
            }

        }

        public object GetFactory(Type _Type, object _Instance, MethodInfo _MethodInfo)
        {
            Type __FuncType = typeof(Func<>).MakeGenericType(_Type);
            return Delegate.CreateDelegate(__FuncType, _Instance, _MethodInfo);
        }

        public void BindAllBaseTypes(Type _FromType, Type _ToType, LifeTime _LifetimeManager)
        {
            if (_ToType != null && _FromType != typeof(Object))
            {
                if (_FromType.IsAssignableFrom(_ToType))
                {
                    Register __Register = (Register)_FromType.GetCustomAttribute(typeof(Register));
                    RegisterFactory __RegisterFactory = (RegisterFactory)_FromType.GetCustomAttribute(typeof(RegisterFactory));
                    if (__RegisterFactory == null)
                    {
                        if (__Register == null)
                        {
                            App.Factories.ObjectFactory.RegisterType(_FromType, _ToType, _LifetimeManager);
                        }
                        else if (__Register.BindFrom.IsAssignableFrom(_ToType))
                        {
                            App.Factories.ObjectFactory.RegisterType(__Register.BindFrom, _ToType, _LifetimeManager);
                        }

                        BindAllBaseTypes(_FromType.BaseType, _ToType, _LifetimeManager);
                    }
                }
            }
        }


        protected List<Object> CreateInstanceFromRegisteredTypes()
        {
            List<object> __InitList = new List<object>();
            foreach (string __DomainName in App.Configuration.DomainNames)
            {
                List<Type> __AllTypes = App.Handlers.AssemblyHandler.GetLoadedApplicationTypesByCustomAttribute<Register>(__DomainName);
                foreach (Type __Type in __AllTypes)
                {
                    Register __Register = (Register)__Type.GetCustomAttribute(typeof(Register));
                    if (__Register.CreateInstance)
                    {
                        Object __Item = App.Factories.ObjectFactory.ResolveInstance(__Register.BindFrom);
                        if (__Register.RegisterInstance)
                        {
                            App.Factories.ObjectFactory.RegisterInstance(__Register.BindFrom, __Item);
                        }

                        if (__Register.CallInit)
                        {
                            __InitList.Add(__Item);
                        }
                    }
                }
            }
            return __InitList;
        }

        protected void InitInstanceFromObjectList(List<object> _InitList)
        {
            foreach (object __Object in _InitList)
            {
                MethodInfo __Method = __Object.GetType().SearchMethod("Init");
                if (__Method != null)
                {
                    __Method.Invoke(__Object, new object[] { });
                }
            }
        }

        protected void StartInvoker<TStarter>() where TStarter : IStarter
        {
            cStartup<TStarter> __Startup = App.Factories.ObjectFactory.ResolveInstance<cStartup<TStarter>>();
            __Startup.Start(App);
        }
    }
}
