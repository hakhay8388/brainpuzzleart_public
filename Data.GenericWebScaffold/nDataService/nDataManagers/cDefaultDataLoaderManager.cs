using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nAttributes;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    [Register(typeof(IDefaultDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager, IDefaultDataLoader
    {
        public cLanguageDataLoader LanguageDataLoader { get; set; }
        public cGlobalParamsDataLoader GlobalParamsDataLoader { get; set; }
        public cDefaultUsersDataLoader DefaultUsersDataLoader { get; set; }
        public cRoleMenuLoader RoleMenuLoader { get; set; }
        public cPageDataLoader PageDataLoader { get; set; }
        public cRolePageLoader RolePageLoader { get; set; }
        public cRoleDataLoader RoleDataLoader { get; set; }
        public cMenuDataLoader MenuDataLoader { get; set; }

        public cRoleDataSourcePermissionLoader RoleDataSourcePermissionLoader { get; set; }
        public cRoleDataSourceColumnLoader RoleDataSourceColumnLoader { get; set; }

        public cLanguageDataManager LanguageDataManager { get; set; }
        public cDefaultDataLoaderManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cLanguageDataLoader _LanguageDataLoader
            , cGlobalParamsDataLoader _GlobalParamsDataLoader
            , cDefaultUsersDataLoader _DefaultUsersDataLoader
            , cRoleMenuLoader _RoleMenuLoader
            , cPageDataLoader _PageDataLoader
            , cRolePageLoader _RolePageLoader
            , cMenuDataLoader _MenuDataLoader
            , cRoleDataLoader _RoleDataLoader
            , cRoleDataSourcePermissionLoader _RoleDataSourcePermissionLoader
            , cRoleDataSourceColumnLoader _RoleDataSourceColumnLoader
            , cLanguageDataManager _LanguageDataManager
            )

          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
            LanguageDataLoader = _LanguageDataLoader;
            LanguageDataManager = _LanguageDataManager;
            GlobalParamsDataLoader = _GlobalParamsDataLoader;
            DefaultUsersDataLoader = _DefaultUsersDataLoader;
            RoleMenuLoader = _RoleMenuLoader;
            PageDataLoader = _PageDataLoader;
            RolePageLoader = _RolePageLoader;
            MenuDataLoader = _MenuDataLoader;
            RoleDataLoader = _RoleDataLoader;
            RoleDataSourcePermissionLoader = _RoleDataSourcePermissionLoader;
            RoleDataSourceColumnLoader = _RoleDataSourceColumnLoader;

        }

        public void Load(IDataService _DataService)
        {
            _DataService.Perform(() => { LanguageDataLoader.Init(_DataService); });
            _DataService.Perform(() => { GlobalParamsDataLoader.Init(_DataService); });
            _DataService.Perform(() => { RoleDataLoader.Init(_DataService); });
            _DataService.Perform(() => { PageDataLoader.Init(_DataService); });
            _DataService.Perform(() => { RolePageLoader.Init(_DataService); });
            _DataService.Perform(() => { MenuDataLoader.Init(_DataService); });
            _DataService.Perform(() => { RoleMenuLoader.Init(_DataService); });
            _DataService.Perform(() => { RoleDataSourcePermissionLoader.Init(_DataService); });
            _DataService.Perform(() => { RoleDataSourceColumnLoader.Init(_DataService); });
            _DataService.Perform(() => { DefaultUsersDataLoader.Init(_DataService); });
            _DataService.Perform(() => { LanguageDataManager.RefreshLanguageFromDB(_DataService); });
        }
    }
}

