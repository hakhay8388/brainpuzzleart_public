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
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;

namespace Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders
{
    public class cRoleDataLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cRoleDataLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
			, cChecksumDataManager _ChecksumDataManager
			, cRoleDataManager _RoleDataManager)
          : base(LoaderIDs.RoleDataLoader, _CoreServiceContext, _DataServiceManager, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
        }

        public void Init(IDataService _DataService)
        {
			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
			string __TotalString = GetTotalString<RoleIDs>(RoleIDs.TypeList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				for (int i = 0; i < RoleIDs.TypeList.Count; i++)
				{
					RoleDataManager.CreateRuleByCodeAndNameIfNotExists(RoleIDs.TypeList[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
			}

			//RoleDataManager.CreateRuleByCodeAndNameIfNotExists(RoleIDs.Admin);
            //RoleDataManager.CreateRuleByCodeAndNameIfNotExists(RoleIDs.Seller);
            //RoleDataManager.CreateRuleByCodeAndNameIfNotExists(RoleIDs.Customer);
        
        }
    }
}
