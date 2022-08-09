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
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders.nLoaderIDs;

namespace Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders
{
    public class cRoleMenuLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cMenuDataManager MenuDataManager { get; set; }
        public cPageDataManager PageDataManager { get; set; }
        public cDataSourceDataManager DataSourceDataManager { get; set; }
        public cRoleMenuLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
			, cChecksumDataManager _ChecksumDataManager
			, cRoleDataManager _RoleDataManager
            , cMenuDataManager _MenuDataManager
            , cPageDataManager _PageDataManager
            , cDataSourceDataManager _DataSourceDataManager
            )
          : base(LoaderIDs.RoleMenuLoader, _CoreServiceContext, _DataServiceManager, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            MenuDataManager = _MenuDataManager;
            PageDataManager = _PageDataManager;
            DataSourceDataManager = _DataSourceDataManager;
        }

        public void Init(IDataService _DataService)
        {
            AddAdminMenus();
			AddSellerMenus();
			AddCustomerMenus();
			AddDeveloperMenus();

		}

        protected void AddMenuToRole(cRoleEntity _Role, cMenuEntity _MenuEntity)
        {
            RoleDataManager.AddMenuToRole(_Role, _MenuEntity);
        }

        public void AddAdminMenus()
        {

			List<MenuIDs> __Menus = new List<MenuIDs>();

			/*__Menus.Add(MenuIDs.AdminMainPage);
			__Menus.Add(MenuIDs.BatchJobPage);
			__Menus.Add(MenuIDs.TemplatesPage);
			__Menus.Add(MenuIDs.ConfigurationPage);
			
			
			__Menus.Add(MenuIDs.ZoomRequestLogPage);
			__Menus.Add(MenuIDs.ZoomMeetingsPage);
			__Menus.Add(MenuIDs.ZoomWebHookRequestPage);
			__Menus.Add(MenuIDs.ZoomMenu);

			__Menus.Add(MenuIDs.UserList);
			__Menus.Add(MenuIDs.TeacherPage);
			__Menus.Add(MenuIDs.AccountNotActivated);
			__Menus.Add(MenuIDs.UsersMenu);

			__Menus.Add(MenuIDs.ConfigBackupsPage);
			__Menus.Add(MenuIDs.LanguagePage);			
			__Menus.Add(MenuIDs.SearchPage);			
			__Menus.Add(MenuIDs.SupportPage);
			__Menus.Add(MenuIDs.SearchTagPage);
			__Menus.Add(MenuIDs.MarqueeListPage);
			__Menus.Add(MenuIDs.PendingOrdersPage);
			__Menus.Add(MenuIDs.CreditPackageDefinationPage);
			__Menus.Add(MenuIDs.CancelledSubscriptionsPage);*/

			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Admin");
			string __TotalString = GetTotalString<MenuIDs>(__Menus);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
				for (int i = 0; i < __Menus.Count; i++)
				{
					AddMenuToRole(__Role, MenuDataManager.GetMenuByCode(__Menus[i].Code));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Admin", __StringCheckSum);
			}

		}

        public void AddSellerMenus()
        {
			List<MenuIDs> __Menus = new List<MenuIDs>();

			/*__Menus.Add(MenuIDs.SellerMainPage);
			__Menus.Add(MenuIDs.SellerLessonPage);
			__Menus.Add(MenuIDs.SearchPage);
			__Menus.Add(MenuIDs.SellerReservationListPage);
			__Menus.Add(MenuIDs.SellerLessonCountDownPage);
			__Menus.Add(MenuIDs.SupportPage);*/


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Seller");
			string __TotalString = GetTotalString<MenuIDs>(__Menus);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Seller.Code);
				for (int i = 0; i < __Menus.Count; i++)
				{
					AddMenuToRole(__Role, MenuDataManager.GetMenuByCode(__Menus[i].Code));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Seller", __StringCheckSum);
			}
		}

        public void AddCustomerMenus()
        {

			List<MenuIDs> __Menus = new List<MenuIDs>();

			/*__Menus.Add(MenuIDs.CustomerMainPage);
			__Menus.Add(MenuIDs.SearchPage);
			__Menus.Add(MenuIDs.CustomerReservationListPage);
			__Menus.Add(MenuIDs.CustomerCreditAddedListPage);
			__Menus.Add(MenuIDs.CreditPackageListPage);
			__Menus.Add(MenuIDs.CustomerRatingPage);
			__Menus.Add(MenuIDs.CustomerLessonCountDownPage);
			__Menus.Add(MenuIDs.SupportPage);*/


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Customer");
			string __TotalString = GetTotalString<MenuIDs>(__Menus);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Customer.Code);
				for (int i = 0; i < __Menus.Count; i++)
				{
					AddMenuToRole(__Role, MenuDataManager.GetMenuByCode(__Menus[i].Code));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Customer", __StringCheckSum);
			}
		}
		public void AddDeveloperMenus()
		{

			List<MenuIDs> __Menus = new List<MenuIDs>();

			/*__Menus.Add(MenuIDs.DeveloperMainPage);
			__Menus.Add(MenuIDs.SharedSessionPage);
			__Menus.Add(MenuIDs.SystemSettingsPage);
			__Menus.Add(MenuIDs.LiveSessionsPage);*/



			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Developer");
			string __TotalString = GetTotalString<MenuIDs>(__Menus);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
				for (int i = 0; i < __Menus.Count; i++)
				{
					AddMenuToRole(__Role, MenuDataManager.GetMenuByCode(__Menus[i].Code));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Developer", __StringCheckSum);
			}
		}
	}
}
