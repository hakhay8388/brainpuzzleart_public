using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;

using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery.nResult;
using Data.Boundary.nData;
using Base.Data.nDataUtils;
using System.IO;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cConfigBackupDataManager : cBaseDataManager
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cConfigBackupDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager
            , IFileDateService _FileDataService
            , cRoleDataManager _RoleDataManager
            )
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
            RoleDataManager = _RoleDataManager;
        }

        public cQuery<cConfigBackupEntity> GetListFromFile(string[] __Files)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cConfigBackupEntity __Test = null;

            cBaseHardCodedValues __HardCodedValues = __DataService.Database.Catalogs.DataToolOperationSQLCatalog.GetHardCodedValues(__DataService);

            __HardCodedValues.DefineColumns("ID", "FullPath", "ConfigBackupFile");
            for (int i = 0; i < __Files.Length; i++)
            {

                __HardCodedValues.AddValue((i + 1), __Files[i], Path.GetFileName(__Files[i]));
            }


            cQuery<cConfigBackupEntity> __Query =
                __DataService.Database.Query<cConfigBackupEntity>(__HardCodedValues, () => __Test)
                .SelectAll().Where().ToQuery();
            return __Query;
        }

    }

}