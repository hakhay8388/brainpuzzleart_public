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
    public class cRolePageLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cPageDataManager PageDataManager { get; set; }
        public cDataSourceDataManager DataSourceDataManager { get; set; }
        public cRolePageLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
			, cChecksumDataManager _ChecksumDataManager
			, cRoleDataManager _RoleDataManager
            , cPageDataManager _PageDataManager
            , cDataSourceDataManager _DataSourceDataManager
            )
          : base(LoaderIDs.RolePageLoader, _CoreServiceContext, _DataServiceManager, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            PageDataManager = _PageDataManager;
            DataSourceDataManager = _DataSourceDataManager;
        }

        public void Init(IDataService _DataService)
        {
            AddAdminPages();
            AddSellerPages();
            AddCustomerPages();
			AddDeveloperPages();

		}

        protected void AddPageToRole(cRoleEntity _Role, cPageEntity _PageEntity)
        {
            PageDataManager.AddPageToRole(_Role, _PageEntity);
        }

        public void AddAdminPages()
        { 
			List<PageIDs> __Pages = new List<PageIDs>();

			/*__Pages.Add(PageIDs.AdminMainPage);
			__Pages.Add(PageIDs.BatchJobPage);
			__Pages.Add(PageIDs.CreditPackageDefinationPage);
			__Pages.Add(PageIDs.TemplatesPage);
			__Pages.Add(PageIDs.ConfigurationPage);
			__Pages.Add(PageIDs.AccountNotActivated);
			__Pages.Add(PageIDs.ZoomRequestLogPage);
			__Pages.Add(PageIDs.ZoomMeetingsPage);
			__Pages.Add(PageIDs.ZoomWebHookRequestPage);
			__Pages.Add(PageIDs.MenuPage);
			__Pages.Add(PageIDs.ConfigBackupsPage);

			__Pages.Add(PageIDs.LanguagePage);
			__Pages.Add(PageIDs.OnlineTrainingMenu);
			__Pages.Add(PageIDs.UserList);
			__Pages.Add(PageIDs.OnlineTraining);
			__Pages.Add(PageIDs.ActiveTrainingList);
			__Pages.Add(PageIDs.TrainingList);
			__Pages.Add(PageIDs.CoachOnlineTraining);
			__Pages.Add(PageIDs.OnlineTrainingWithCoach);
			__Pages.Add(PageIDs.SearchPage);
			__Pages.Add(PageIDs.SellerDetailForCustomerPage);
			__Pages.Add(PageIDs.ContractsPage);
			__Pages.Add(PageIDs.AboutPage);
			__Pages.Add(PageIDs.UserConfirmationPage);
			__Pages.Add(PageIDs.ChangePasswordConfirmationPage);
			__Pages.Add(PageIDs.TeacherPage);
			__Pages.Add(PageIDs.SupportPage);
			__Pages.Add(PageIDs.SearchTagPage);
			__Pages.Add(PageIDs.MarqueeListPage);
			__Pages.Add(PageIDs.PendingOrdersPage);
			__Pages.Add(PageIDs.CancelledSubscriptionsPage);
			__Pages.Add(PageIDs.Egitmen);*/


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Admin");
			string __TotalString = GetTotalString<PageIDs>(__Pages);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
				for (int i = 0; i < __Pages.Count; i++)
				{
					AddPageToRole(__Role, PageDataManager.GetPageByUrl(__Pages[i].Url));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Admin", __StringCheckSum);
			} 

        }

        public void AddSellerPages()
        { 
			List<PageIDs> __Pages = new List<PageIDs>(); 

			/*__Pages.Add(PageIDs.SellerMainPage);
            __Pages.Add(PageIDs.SellerProfilePage);
            __Pages.Add(PageIDs.UserList);
            __Pages.Add(PageIDs.SellerLessonPage);
            __Pages.Add(PageIDs.SearchPage);
            __Pages.Add(PageIDs.SellerDetailForCustomerPage);
            __Pages.Add(PageIDs.MessagingPage);
            __Pages.Add(PageIDs.SellerReservationListPage);
            __Pages.Add(PageIDs.SellerLessonCountDownPage);
            __Pages.Add(PageIDs.ContractsPage);
            __Pages.Add(PageIDs.AboutPage);
			__Pages.Add(PageIDs.UserConfirmationPage);
			__Pages.Add(PageIDs.ChangePasswordConfirmationPage);
			__Pages.Add(PageIDs.SupportPage);
			__Pages.Add(PageIDs.VideoPage); 
			__Pages.Add(PageIDs.Egitmen);*/


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Seller");
			string __TotalString = GetTotalString<PageIDs>(__Pages);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Seller.Code);
				for (int i = 0; i < __Pages.Count; i++)
				{
					AddPageToRole(__Role, PageDataManager.GetPageByUrl(__Pages[i].Url));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Seller", __StringCheckSum);
			}

		}

        public void AddCustomerPages()
        { 
			List<PageIDs> __Pages = new List<PageIDs>();

			/*__Pages.Add(PageIDs.CustomerMainPage);
            __Pages.Add(PageIDs.CustomerProfilePage);
            __Pages.Add(PageIDs.CustomerAvailableLessonsPage);
            __Pages.Add(PageIDs.SearchPage);
            __Pages.Add(PageIDs.SellerDetailForCustomerPage);
            __Pages.Add(PageIDs.MessagingPage);
            __Pages.Add(PageIDs.CustomerReservationListPage);
            __Pages.Add(PageIDs.CustomerRatingPage);
            __Pages.Add(PageIDs.CreditPackageListPage);
            __Pages.Add(PageIDs.CustomerCreditAddedListPage);
            __Pages.Add(PageIDs.CustomerLessonCountDownPage); 
            __Pages.Add(PageIDs.ContractsPage);
            __Pages.Add(PageIDs.AboutPage);
			__Pages.Add(PageIDs.UserConfirmationPage);
			__Pages.Add(PageIDs.ChangePasswordConfirmationPage);
			__Pages.Add(PageIDs.CustomerOrderPage);
            __Pages.Add(PageIDs.CustomerPaymentResultPage);  
			__Pages.Add(PageIDs.SupportPage);
			__Pages.Add(PageIDs.CustomerSubscriptionPage);
			__Pages.Add(PageIDs.VideoPage);
			__Pages.Add(PageIDs.Egitmen);*/


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Customer");
			string __TotalString = GetTotalString<PageIDs>(__Pages);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Customer.Code);
				for (int i = 0; i < __Pages.Count; i++)
				{
					AddPageToRole(__Role, PageDataManager.GetPageByUrl(__Pages[i].Url));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Customer", __StringCheckSum);
			}
		}
		public void AddDeveloperPages()
		{
			List<PageIDs> __Pages = new List<PageIDs>();

			/*__Pages.Add(PageIDs.DeveloperMainPage);
			__Pages.Add(PageIDs.SharedSessionPage);
			__Pages.Add(PageIDs.LiveSessionsPage);
			__Pages.Add(PageIDs.SystemSettingsPage);
			__Pages.Add(PageIDs.Egitmen);*/


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Developer");
			string __TotalString = GetTotalString<PageIDs>(__Pages);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
				for (int i = 0; i < __Pages.Count; i++)
				{
					AddPageToRole(__Role, PageDataManager.GetPageByUrl(__Pages[i].Url));
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Developer", __StringCheckSum);
			}
		}
	}
}
