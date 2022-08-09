using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDefaultValueTypes
{
    public class DefaultGlobalParamsIDs : cBaseConstType<DefaultGlobalParamsIDs>
    {
        public static List<DefaultGlobalParamsIDs> TypeList { get; set; }



        public static DefaultGlobalParamsIDs SearchPagingCount = new DefaultGlobalParamsIDs(GetVariableName(() => SearchPagingCount), "SearchPagingCount", 7, 2, 7, false);
        public static DefaultGlobalParamsIDs SmtpHost = new DefaultGlobalParamsIDs(GetVariableName(() => SmtpHost), "SmtpHost", 10, "", 10, true);
        public static DefaultGlobalParamsIDs SmtpPort = new DefaultGlobalParamsIDs(GetVariableName(() => SmtpPort), "SmtpPort", 11, 0, 11, true);
        public static DefaultGlobalParamsIDs SmtpUsername = new DefaultGlobalParamsIDs(GetVariableName(() => SmtpUsername), "SmtpUsername", 12, "", 12, true);
        public static DefaultGlobalParamsIDs SmtpPassword = new DefaultGlobalParamsIDs(GetVariableName(() => SmtpPassword), "SmtpPassword", 13, "", 13, true);
        public static DefaultGlobalParamsIDs EmailDisplayName = new DefaultGlobalParamsIDs(GetVariableName(() => EmailDisplayName), "EmailDisplayName", 14, "", 14, true);
        public static DefaultGlobalParamsIDs SmtpSslEnabled = new DefaultGlobalParamsIDs(GetVariableName(() => SmtpSslEnabled), "SmtpSslEnabled", 15, false, 15, true);
        public static DefaultGlobalParamsIDs MailFrom = new DefaultGlobalParamsIDs(GetVariableName(() => MailFrom), "MailFrom", 16, "", 16, true);


		public static DefaultGlobalParamsIDs FrontEndDebugMessage = new DefaultGlobalParamsIDs(GetVariableName(() => FrontEndDebugMessage), "FrontEndDebugMessage", 29, false, 29, false);
        public static DefaultGlobalParamsIDs BackendDebugMessageShowToUser = new DefaultGlobalParamsIDs(GetVariableName(() => BackendDebugMessageShowToUser), "BackendDebugMessageShowToUser", 30, false, 30, false);



        public static DefaultGlobalParamsIDs AutoCompleteSimilarityTest = new DefaultGlobalParamsIDs(GetVariableName(() => AutoCompleteSimilarityTest), "AutoCompleteSimilarityTest", 2003, 4, 2003, false);
        public static DefaultGlobalParamsIDs SearchSimilarityTest = new DefaultGlobalParamsIDs(GetVariableName(() => SearchSimilarityTest), "SearchSimilarityTest", 2004, 4, 2004, false);


        public static DefaultGlobalParamsIDs MssqlBackupPath = new DefaultGlobalParamsIDs(GetVariableName(() => MssqlBackupPath), "MssqlBackupPath", 2005, $"C:\\BPABackup", 2005, true);
        public static DefaultGlobalParamsIDs UploadFileMaxSize = new DefaultGlobalParamsIDs(GetVariableName(() => UploadFileMaxSize), "UploadFileMaxSize", 2006, 10485760, 2006, false);
        public static DefaultGlobalParamsIDs ConfigBackupPath = new DefaultGlobalParamsIDs(GetVariableName(() => ConfigBackupPath), "ConfigBackupPath", 2007, $"C:\\BPAConfigBackup", 2007, true);


        public static DefaultGlobalParamsIDs SiteShortName = new DefaultGlobalParamsIDs(GetVariableName(() => SiteShortName), "SiteShortName", 3017, "BrainPuzzleArt", 3017, false);
        public static DefaultGlobalParamsIDs CompanyName = new DefaultGlobalParamsIDs(GetVariableName(() => CompanyName), "CompanyName", 3018, "Brain Puzzle Art", 3018, false);
        public static DefaultGlobalParamsIDs SiteUrl = new DefaultGlobalParamsIDs(GetVariableName(() => SiteUrl), "SiteUrl", 3019, "brainpuzzleart.io", 3019, false);
        public static DefaultGlobalParamsIDs HeaderShow = new DefaultGlobalParamsIDs(GetVariableName(() => HeaderShow), "HeaderShow", 3037, true, 3037, false);
        public static DefaultGlobalParamsIDs FooterShow = new DefaultGlobalParamsIDs(GetVariableName(() => FooterShow), "FooterShow", 3038, false, 3038, false);
        public static DefaultGlobalParamsIDs LanguageSelection = new DefaultGlobalParamsIDs(GetVariableName(() => LanguageSelection), "LanguageSelection", 3039, false, 3039, false);
        public static DefaultGlobalParamsIDs UnloginSearchPermission = new DefaultGlobalParamsIDs(GetVariableName(() => UnloginSearchPermission), "UnloginSearchPermission", 3043, false, 3043, false);
        public static DefaultGlobalParamsIDs NameCharacterLimit = new DefaultGlobalParamsIDs(GetVariableName(() => NameCharacterLimit), "NameCharacterLimit", 3044, 15, 3045, false);
        public static DefaultGlobalParamsIDs SurnameCharacterLimit = new DefaultGlobalParamsIDs(GetVariableName(() => SurnameCharacterLimit), "SurnameCharacterLimit", 3045, 20, 3045, false);
        public static DefaultGlobalParamsIDs WaitForFlowCheck = new DefaultGlobalParamsIDs(GetVariableName(() => WaitForFlowCheck), "WaitForFlowCheck", 38, 5000, 38, false);
        public static DefaultGlobalParamsIDs ActivationReminderDay = new DefaultGlobalParamsIDs(GetVariableName(() => ActivationReminderDay), "ActivationReminderDay", 39, 3, 39, false);
        public static DefaultGlobalParamsIDs ActivationReminderDeadline = new DefaultGlobalParamsIDs(GetVariableName(() => ActivationReminderDeadline), "ActivationReminderDeadline", 40, 10, 40, false);
        public static DefaultGlobalParamsIDs BatchJobUrl = new DefaultGlobalParamsIDs(GetVariableName(() => BatchJobUrl), "BatchJobUrl", 45, "", 45, true);


        public object Value { get; set; }
        public int Order { get; set; }
        public bool IsPrivate { get; set; }
        public DefaultGlobalParamsIDs(string _Code, string _Name, int _ID, object _Value, int _Order, bool _IsPrivate)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<DefaultGlobalParamsIDs>();
            Value = _Value;
            Order = _Order;
            IsPrivate = _IsPrivate;
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static DefaultGlobalParamsIDs GetByID(int _ID, DefaultGlobalParamsIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static DefaultGlobalParamsIDs GetByName(string _Name, DefaultGlobalParamsIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static DefaultGlobalParamsIDs GetByCode(string _Code, DefaultGlobalParamsIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
