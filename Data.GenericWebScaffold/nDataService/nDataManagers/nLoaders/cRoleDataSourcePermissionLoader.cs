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
	public class cRoleDataSourcePermissionCheckSum
	{
		public DataSourceIDs DataSourceID { get; set; }
		public bool CanCreate { get; set; }
		public bool CanRead { get; set; }
		public bool CanUpdate { get; set; }
		public bool CanDelete { get; set; }

		public cRoleDataSourcePermissionCheckSum(DataSourceIDs _DataSourceID, bool _CanCreate, bool _CanRead, bool _CanUpdate, bool _CanDelete)
		{
			DataSourceID = _DataSourceID;
			CanCreate = _CanCreate;
			CanRead = _CanRead;
			CanUpdate = _CanUpdate;
			CanDelete = _CanDelete;
		}

	}

	public class cRoleDataSourcePermissionLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cDataSourceDataManager DataSourceDataManager { get; set; }

        public cRoleDataSourcePermissionLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
			, cChecksumDataManager _ChecksumDataManager
			, cRoleDataManager _RoleDataManager
            , cMenuDataManager _MenuDataManager
            , cPageDataManager _PageDataManager
            , cDataSourceDataManager _DataSourceDataManager
            )
          : base(LoaderIDs.RoleDataSourcePermissionLoader, _CoreServiceContext, _DataServiceManager, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            DataSourceDataManager = _DataSourceDataManager;
        }

        public void Init(IDataService _DataService)
        {
            AddAdminPages();
            AddSellerPages();
            AddCustomerPages();
			AddDeveloperPages();
        }


        public void AddAdminPages()
        { 
			List<cRoleDataSourcePermissionCheckSum> __RoleDataSourcePermissionCheckSumList = new List<cRoleDataSourcePermissionCheckSum>();

			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.BatchJobList, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.CreditPackageDefinationList, true, true, false, false));
			
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.BatchJobExecutionList, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.UserList, true, true, true, true));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.UserList_CustomQuery, true, true, true, false)); 
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.WaitingTeacherList, true, true, true, true));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.TeacherSellerRequestTicketList, true, true, true, true));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.AccountNotActivatedUserList, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ZoomRequestResponseLog, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ZoomRequestResponseLogDetail, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ZoomWebHookRequestLog, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ZoomMeeting, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ZoomMeetingEvents, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ZoomUserEvents, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ZoomMeetingParticipants, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ZoomParticipantEvents, true, true, false, false));

			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.ConfigBackups, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.MarqueeList, true, true, true, true));
			


			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.TicketList, true, true, true, true));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.LanguageList, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.PendingOrders, true, true, false, false));
			
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.SearchTagListDataSources, true, true, true, true));

			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.CancelledSubscriptions, true, true, false, false));

			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Admin");
			string __TotalString = GetTotalString<cRoleDataSourcePermissionCheckSum>(__RoleDataSourcePermissionCheckSumList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
				foreach (var __Item in __RoleDataSourcePermissionCheckSumList)
				{
					DataSourceDataManager.AddDataSourceToRole(__Role, __Item.DataSourceID, __Item.CanCreate, __Item.CanRead, __Item.CanUpdate, __Item.CanDelete);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Admin", __StringCheckSum);
			}

		}

        public void AddSellerPages()
        {
			List<cRoleDataSourcePermissionCheckSum> __RoleDataSourcePermissionCheckSumList = new List<cRoleDataSourcePermissionCheckSum>();

			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.UserList, false, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.SellerLessonList, true, true, true, true));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.SellerClassLessonList, true, true, true, true));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.SellerOldReservationList, false, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.SellerFutureReservationList, false, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.UserTicketList, false, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.NewSellerClassLessonList, true, true, true, true));


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Seller");
			string __TotalString = GetTotalString<cRoleDataSourcePermissionCheckSum>(__RoleDataSourcePermissionCheckSumList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Seller.Code);
				foreach (var __Item in __RoleDataSourcePermissionCheckSumList)
				{
					DataSourceDataManager.AddDataSourceToRole(__Role, __Item.DataSourceID, __Item.CanCreate, __Item.CanRead, __Item.CanUpdate, __Item.CanDelete);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Seller", __StringCheckSum);
			}

		}

        public void AddCustomerPages()
        {
			List<cRoleDataSourcePermissionCheckSum> __RoleDataSourcePermissionCheckSumList = new List<cRoleDataSourcePermissionCheckSum>();

			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.UserList, false, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.CustomerOldReservationList, false, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.CustomerFutureReservationList, false, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.CustomerOldCreditAddedList, false, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.UserTicketList, false, true, false, false));


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Customer");
			string __TotalString = GetTotalString<cRoleDataSourcePermissionCheckSum>(__RoleDataSourcePermissionCheckSumList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Customer.Code);
				foreach (var __Item in __RoleDataSourcePermissionCheckSumList)
				{
					DataSourceDataManager.AddDataSourceToRole(__Role, __Item.DataSourceID, __Item.CanCreate, __Item.CanRead, __Item.CanUpdate, __Item.CanDelete);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Customer", __StringCheckSum);
			}

		}
		public void AddDeveloperPages()
		{
			List<cRoleDataSourcePermissionCheckSum> __RoleDataSourcePermissionCheckSumList = new List<cRoleDataSourcePermissionCheckSum>();
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.LiveSessionEmails, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.LiveSessionEmailSessions, true, true, false, false));
			__RoleDataSourcePermissionCheckSumList.Add(new cRoleDataSourcePermissionCheckSum(DataSourceIDs.LiveSessionEmailSessionSignals, true, true, false, false));
			

			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Developer");
			string __TotalString = GetTotalString<cRoleDataSourcePermissionCheckSum>(__RoleDataSourcePermissionCheckSumList);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
				foreach (var __Item in __RoleDataSourcePermissionCheckSumList)
				{
					DataSourceDataManager.AddDataSourceToRole(__Role, __Item.DataSourceID, __Item.CanCreate, __Item.CanRead, __Item.CanUpdate, __Item.CanDelete);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Developer", __StringCheckSum);
			}

		}
	}
}
