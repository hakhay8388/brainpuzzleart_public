using Base.Core. nCore;
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
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders.nLoaderIDs;

namespace Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders
{
    public class cGlobalParamsDataLoader : cBaseDataLoader
    {
        public cParamsDataManager ParamsDataManager { get; set; }


        public cGlobalParamsDataLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
			, cChecksumDataManager _ChecksumDataManager
			, cParamsDataManager _ParamsDataManager
         )
          : base(LoaderIDs.GlobalParamsDataLoader, _CoreServiceContext, _DataServiceManager, _FileDataService, _ChecksumDataManager)
        {
            ParamsDataManager = _ParamsDataManager;
        }

        public void Init(IDataService _DataService)
        {
			////////////// Customer //////////////////

			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
			string __TotalString = GetTotalString< DefaultGlobalParamsIDs>(DefaultGlobalParamsIDs.TypeList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				for (int i = 0; i < DefaultGlobalParamsIDs.TypeList.Count; i++)
				{
					ParamsDataManager.CreateMenuIfNotExists(DefaultGlobalParamsIDs.TypeList[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
			}

            /////////////////////////////////////////////////////////////////////////////////
        }
    }
}
