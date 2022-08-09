using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.Boundary.nData;
using Base.Data.nDataServiceManager;

namespace Data.GenericWebScaffold.nGlobalDataServices.nDataManagers.nLoaders
{
    public class cAdminDataLoader : cBaseDataLoader
    {
        public cProfileDataManager ProfileDataManager { get; set; }
        public cAdminDataLoader(cGlobalDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService, cProfileDataManager _ProfileDataManager)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
            ProfileDataManager = _ProfileDataManager;
        }

        public void Init(IDataService _DataService)
        {
            if (ProfileDataManager.GetProfileByHostName("localhost") == null)
            {
                ProfileDataManager.CreateProfile("localhost", "admin@admin.com", "admin", "admin", "", DateTime.Now.AddYears(10), ServiceContext.Configuration.DBUserName, ServiceContext.Configuration.DBPassword, ServiceContext.Configuration.DBServer, "BPA", ServiceContext.Configuration.MaxConnectCount);
            }
        }
    }
}
