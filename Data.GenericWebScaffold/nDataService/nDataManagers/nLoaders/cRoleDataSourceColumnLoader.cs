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
    public class cRoleDataSourceColumnLoader : cBaseDataLoader
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cDataSourceDataManager DataSourceDataManager { get; set; }

        public cRoleDataSourceColumnLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
			, cChecksumDataManager _ChecksumDataManager
			, cRoleDataManager _RoleDataManager
            , cMenuDataManager _MenuDataManager
            , cPageDataManager _PageDataManager
            , cDataSourceDataManager _DataSourceDataManager
            )
          : base(LoaderIDs.RoleDataSourceColumnLoader, _CoreServiceContext, _DataServiceManager, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            DataSourceDataManager = _DataSourceDataManager;
        }

        public void Init(IDataService _DataService)
        {
			AddAdminRoleDataSourceColumns();
			AddSellerRoleDataSourceColumns();
			AddCustomerRoleDataSourceColumns();
			AddDeveloperRoleDataSourceColumns();

		}


        public void AddAdminRoleDataSourceColumns()
        { 
			List<DataSourceIDs> __DataSources = new List<DataSourceIDs>();

            __DataSources.Add(DataSourceIDs.BatchJobList);
			__DataSources.Add(DataSourceIDs.CreditPackageDefinationList);
			__DataSources.Add(DataSourceIDs.BatchJobExecutionList);
            __DataSources.Add(DataSourceIDs.UserList);
            __DataSources.Add(DataSourceIDs.UserList_CustomQuery);
            __DataSources.Add(DataSourceIDs.WaitingTeacherList);
            __DataSources.Add(DataSourceIDs.TeacherSellerRequestTicketList);
            __DataSources.Add(DataSourceIDs.TicketList);
            __DataSources.Add(DataSourceIDs.AccountNotActivatedUserList);
            __DataSources.Add(DataSourceIDs.ZoomRequestResponseLog);
			__DataSources.Add(DataSourceIDs.ZoomRequestResponseLogDetail);
			__DataSources.Add(DataSourceIDs.ZoomWebHookRequestLog);
			__DataSources.Add(DataSourceIDs.ZoomMeeting);
			__DataSources.Add(DataSourceIDs.ZoomMeetingEvents);
			__DataSources.Add(DataSourceIDs.ZoomUserEvents);
			__DataSources.Add(DataSourceIDs.ZoomMeetingParticipants);
			__DataSources.Add(DataSourceIDs.ZoomParticipantEvents);
			__DataSources.Add(DataSourceIDs.LanguageList);
			__DataSources.Add(DataSourceIDs.PendingOrders);
            __DataSources.Add(DataSourceIDs.SearchTagListDataSources);
            __DataSources.Add(DataSourceIDs.ConfigBackups);
			__DataSources.Add(DataSourceIDs.MarqueeList);
			__DataSources.Add(DataSourceIDs.CancelledSubscriptions);

			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Admin");
			string __TotalString = GetTotalString<DataSourceIDs>(__DataSources);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
				for (int i = 0; i < __DataSources.Count; i++)
				{
					DataSourceDataManager.AddAllDatasourceColumnToRole(__Role, __DataSources[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Admin", __StringCheckSum);
			}

		} 

        public void AddSellerRoleDataSourceColumns()
        {
 			List<DataSourceIDs> __DataSources = new List<DataSourceIDs>();

			__DataSources.Add(DataSourceIDs.UserList);
			__DataSources.Add(DataSourceIDs.SellerLessonList);
			__DataSources.Add(DataSourceIDs.SellerClassLessonList);
			__DataSources.Add(DataSourceIDs.SellerOldReservationList);
			__DataSources.Add(DataSourceIDs.SellerFutureReservationList);
			__DataSources.Add(DataSourceIDs.UserTicketList);
			__DataSources.Add(DataSourceIDs.NewSellerClassLessonList);


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Seller");
			string __TotalString = GetTotalString<DataSourceIDs>(__DataSources);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Seller.Code);
				for (int i = 0; i < __DataSources.Count; i++)
				{
					DataSourceDataManager.AddAllDatasourceColumnToRole(__Role, __DataSources[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Seller", __StringCheckSum);
			}

		}

        public void AddCustomerRoleDataSourceColumns()
        {
			List<DataSourceIDs> __DataSources = new List<DataSourceIDs>();

			__DataSources.Add(DataSourceIDs.UserList);
			__DataSources.Add(DataSourceIDs.CustomerOldReservationList);
			__DataSources.Add(DataSourceIDs.CustomerFutureReservationList);
			__DataSources.Add(DataSourceIDs.CustomerOldCreditAddedList);
			__DataSources.Add(DataSourceIDs.UserTicketList);
			


			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Customer");
			string __TotalString = GetTotalString<DataSourceIDs>(__DataSources);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Customer.Code);
				for (int i = 0; i < __DataSources.Count; i++)
				{
					DataSourceDataManager.AddAllDatasourceColumnToRole(__Role, __DataSources[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Customer", __StringCheckSum);
			}
		}
		public void AddDeveloperRoleDataSourceColumns()
		{
			List<DataSourceIDs> __DataSources = new List<DataSourceIDs>();
			__DataSources.Add(DataSourceIDs.LiveSessionEmails);
			__DataSources.Add(DataSourceIDs.LiveSessionEmailSessions);
			__DataSources.Add(DataSourceIDs.LiveSessionEmailSessionSignals);
			



			cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code + "_Developer");
			string __TotalString = GetTotalString<DataSourceIDs>(__DataSources);
			string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

			if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
			{
				cRoleEntity __Role = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
				for (int i = 0; i < __DataSources.Count; i++)
				{
					DataSourceDataManager.AddAllDatasourceColumnToRole(__Role, __DataSources[i]);
				}

				ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code + "_Developer", __StringCheckSum);
			}
		}
	}
}
