using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataServiceManager;
using Base.Core.nAttributes;
using Base.Boundary.nCore.nObjectLifeTime;
using Data.GenericWebScaffold.nGlobalDataServices.nDataManagers.nLoaders;
using Base.Data.nDataFileEntity;
using Data.GenericWebScaffold.nGlobalDataServices.nEntityServices.nEntities;

namespace Data.GenericWebScaffold.nGlobalDataServices.nDataManagers
{
    [Register(typeof(IGlobalDefaultDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager , IGlobalDefaultDataLoader
    {
        public cProfileDataManager ProfileDataManager { get; set; }
        public cAdminDataLoader AdminDataLoader { get; set; }
        public cDefaultDataLoaderManager(cGlobalDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService, cAdminDataLoader _AdminDataLoader, cProfileDataManager _ProfileDataManager)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
            AdminDataLoader = _AdminDataLoader;
            ProfileDataManager = _ProfileDataManager;
        }

        public void Load(IDataService _DataService)
        {
            _DataService.Perform<IDataService>(LoadDefaultData, _DataService);
        }

        public void LoadDefaultData(IDataService _DataService)
        {
            AdminDataLoader.Init(_DataService);
        }

        public cDBConnectionSettingEntity GetConnectionSettingByHostName(string _HostName)
        {
            cProfileEntity __ProfileEntity = ProfileDataManager.GetProfileByHostName(_HostName);
            if (__ProfileEntity != null)
            {
                cDBConnectionSettingEntity __Result = new cDBConnectionSettingEntity();
                __Result.GlobalDBName = __ProfileEntity.DBSetting.DBName;
                __Result.UserName = __ProfileEntity.DBSetting.UserId;
                __Result.Password = __ProfileEntity.DBSetting.Password;
                __Result.Server = __ProfileEntity.DBSetting.Server;
                __Result.MaxConnectCount = __ProfileEntity.DBSetting.MaxConnectionCount;
                return __Result;
            }
            return null;
        }
    }
}
