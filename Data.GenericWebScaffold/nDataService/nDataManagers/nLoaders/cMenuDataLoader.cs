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
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders.nLoaderIDs;

namespace Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders
{
    public class cMenuDataLoader : cBaseDataLoader
    {
        public cPageDataManager PageDataManager { get; set; }

        public cMenuDataManager MenuDataManager { get; set; }

        public cMenuDataLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
			, cChecksumDataManager _ChecksumDataManager
			, cPageDataManager _PageDataManager
            , cMenuDataManager _MenuDataManager
         )
          : base(LoaderIDs.MenuDataLoader,  _CoreServiceContext, _DataServiceManager, _FileDataService, _ChecksumDataManager)
        {
            PageDataManager = _PageDataManager;
            MenuDataManager = _MenuDataManager;
        }

        public void Init(IDataService _DataService)
        {
			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
			string __TotalString = GetTotalString<MenuIDs>(MenuIDs.TypeList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				for (int i = 0; i < MenuIDs.TypeList.Count; i++)
				{
					if (!MenuIDs.TypeList[i].IsMainMenu)
					{
						if (MenuIDs.TypeList[i].RootMenu == null)
						{
							MenuDataManager.CreateMenuIfNotExists(MenuIDs.TypeList[i], PageDataManager.GetPageByUrl(PageIDs.GetByCode(MenuIDs.TypeList[i].Code, null).Url));
						}
						else
						{
							MenuDataManager.CreateSubMenuIfNotExists(MenuIDs.TypeList[i].RootMenu, MenuIDs.TypeList[i], PageDataManager.GetPageByUrl(PageIDs.GetByCode(MenuIDs.TypeList[i].Code, null).Url));
						}
					}
					else
					{
						MenuDataManager.CreateMenuIfNotExists(MenuIDs.TypeList[i]);
					}
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
			}

        }
    }
}
