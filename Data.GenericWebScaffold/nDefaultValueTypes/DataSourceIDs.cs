using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDefaultValueTypes
{
    public class DataSourceIDs : cBaseConstType<DataSourceIDs>
    {
        public static List<DataSourceIDs> TypeList { get; set; }

        public static DataSourceIDs UserList = new DataSourceIDs(GetVariableName(() => UserList), "TUserList", "UserList", 1);
        public static DataSourceIDs SellerLessonList = new DataSourceIDs(GetVariableName(() => SellerLessonList), "TSellerLessonList", "SellerLessonList", 2);
        public static DataSourceIDs SellerClassLessonList = new DataSourceIDs(GetVariableName(() => SellerClassLessonList), "TSellerClassLessonList", "SellerClassLessonList", 3);
        public static DataSourceIDs SellerOldReservationList = new DataSourceIDs(GetVariableName(() => SellerOldReservationList), "TSellerOldReservationList", "SellerOldReservationList", 4);
        public static DataSourceIDs CustomerOldReservationList = new DataSourceIDs(GetVariableName(() => CustomerOldReservationList), "TCustomerOldReservationList", "CustomerOldReservationList", 5);
        public static DataSourceIDs UserList_CustomQuery = new DataSourceIDs(GetVariableName(() => UserList_CustomQuery), "TUserList_CustomQuery", "UserList_CustomQuery", 6, true);

        public static DataSourceIDs SellerFutureReservationList = new DataSourceIDs(GetVariableName(() => SellerFutureReservationList), "TSellerFutureReservationList", "SellerFutureReservationList", 7);
        public static DataSourceIDs CustomerFutureReservationList = new DataSourceIDs(GetVariableName(() => CustomerFutureReservationList), "TCustomerFutureReservationList", "CustomerFutureReservationList", 8);

        public static DataSourceIDs BatchJobList = new DataSourceIDs(GetVariableName(() => BatchJobList), "TBatchJobList", "BatchJobList", 9);
        public static DataSourceIDs BatchJobExecutionList = new DataSourceIDs(GetVariableName(() => BatchJobExecutionList), "TBatchJobExecutionList", "BatchJobExecutionList", 10);
        public static DataSourceIDs CustomerOldCreditAddedList = new DataSourceIDs(GetVariableName(() => CustomerOldCreditAddedList), "TCustomerOldCreditAddedList", "CustomerOldCreditAddedList", 11);
        public static DataSourceIDs WaitingTeacherList = new DataSourceIDs(GetVariableName(() => WaitingTeacherList), "TWaitingTeacherList", "WaitingTeacherList", 12);
        public static DataSourceIDs TeacherSellerRequestTicketList = new DataSourceIDs(GetVariableName(() => TeacherSellerRequestTicketList), "TTeacherSellerRequestTicketList", "TeacherSellerRequestTicketList", 13);
        //////////////////////////////////Muhammed////////////////////////


        public static DataSourceIDs TicketList = new DataSourceIDs(GetVariableName(() => TicketList), "TTicketList", "TicketList", 14);
        public static DataSourceIDs UserTicketList = new DataSourceIDs(GetVariableName(() => UserTicketList), "TUserTicketList", "UserTicketList", 15);
        public static DataSourceIDs NewSellerClassLessonList = new DataSourceIDs(GetVariableName(() => NewSellerClassLessonList), "TNewSellerClassLessonList", "NewSellerClassLessonList", 16);
        public static DataSourceIDs AccountNotActivatedUserList = new DataSourceIDs(GetVariableName(() => AccountNotActivatedUserList), "TAccountNotActivatedUserList", "AccountNotActivatedUserList", 17);
        public static DataSourceIDs LanguageList = new DataSourceIDs(GetVariableName(() => LanguageList), "TLanguageList", "LanguageList", 18);

        public static DataSourceIDs MarqueeList = new DataSourceIDs(GetVariableName(() => MarqueeList), "TMarqueeList", "MarqueeList", 19);
        public static DataSourceIDs ConfigBackups = new DataSourceIDs(GetVariableName(() => ConfigBackups), "TConfigBackups", "ConfigBackups", 21);
        public static DataSourceIDs PendingOrders = new DataSourceIDs(GetVariableName(() => PendingOrders), "TPendingOrders", "PendingOrders", 22);
        public static DataSourceIDs LiveSessionEmails = new DataSourceIDs(GetVariableName(() => LiveSessionEmails), "TLiveSessionEmails", "LiveSessionEmails", 23);
        public static DataSourceIDs LiveSessionEmailSessions = new DataSourceIDs(GetVariableName(() => LiveSessionEmailSessions), "TLiveSessionEmailSessions", "LiveSessionEmailSessions", 24);
        public static DataSourceIDs LiveSessionEmailSessionSignals = new DataSourceIDs(GetVariableName(() => LiveSessionEmailSessionSignals), "TLiveSessionEmailSessionSignals", "LiveSessionEmailSessionSignals", 25);
        public static DataSourceIDs CreditPackageDefinationList = new DataSourceIDs(GetVariableName(() => CreditPackageDefinationList), "TCreditPackageDefinationList", "CreditPackageDefinationList", 26);
        
        public static DataSourceIDs CancelledSubscriptions = new DataSourceIDs(GetVariableName(() => CancelledSubscriptions), "TCancelledSubscriptions", "CancelledSubscriptions", 27);


        /// <summary>
        /// //////////////Hayri
        /// </summary>
        public static DataSourceIDs SearchTagListDataSources = new DataSourceIDs(GetVariableName(() => SearchTagListDataSources), "TSearchTagListDataSources", "SearchTagListDataSources", 100);
		public static DataSourceIDs ZoomRequestResponseLog = new DataSourceIDs(GetVariableName(() => ZoomRequestResponseLog), "TZoomRequestResponseLog", "ZoomRequestResponseLog", 101);
		public static DataSourceIDs ZoomRequestResponseLogDetail = new DataSourceIDs(GetVariableName(() => ZoomRequestResponseLogDetail), "TZoomRequestResponseLogDetail", "ZoomRequestResponseLogDetail", 100001);

		public static DataSourceIDs ZoomMeeting = new DataSourceIDs(GetVariableName(() => ZoomMeeting), "TZoomMeeting", "ZoomMeeting", 102);
		public static DataSourceIDs ZoomMeetingEvents = new DataSourceIDs(GetVariableName(() => ZoomMeetingEvents), "TZoomMeetingEvents", "ZoomMeetingEvents", 103);
		public static DataSourceIDs ZoomUserEvents = new DataSourceIDs(GetVariableName(() => ZoomUserEvents), "TZoomUserEvents", "ZoomUserEvents", 104);
		public static DataSourceIDs ZoomMeetingParticipants = new DataSourceIDs(GetVariableName(() => ZoomMeetingParticipants), "TZoomMeetingParticipants", "ZoomMeetingParticipants", 105);
		public static DataSourceIDs ZoomParticipantEvents = new DataSourceIDs(GetVariableName(() => ZoomParticipantEvents), "TZoomParticipantEvents", "ZoomParticipantEvents", 106);
		public static DataSourceIDs ZoomWebHookRequestLog = new DataSourceIDs(GetVariableName(() => ZoomWebHookRequestLog), "TZoomWebHookRequestLog", "ZoomWebHookRequestLog", 107);

		


		public string ClientComponentName { get; set; }
        public bool IsPublic { get; set; }

        public DataSourceIDs(string _Code, string _ClientComponentName, string _Name, int _ID, bool _IsPublic = false)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<DataSourceIDs>();
            TypeList.Add(this);
            ClientComponentName = _ClientComponentName;
            IsPublic = _IsPublic;
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static DataSourceIDs GetByID(int _ID, DataSourceIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static DataSourceIDs GetByName(string _Name, DataSourceIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static DataSourceIDs GetByCode(string _Code, DataSourceIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
