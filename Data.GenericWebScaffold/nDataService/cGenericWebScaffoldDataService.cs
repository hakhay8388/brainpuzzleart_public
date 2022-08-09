using Base.Boundary.nCore.nObjectLifeTime;
using Base.Boundary.nData;
using Base.Core.nAttributes;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDataService
{
    [Register(typeof(IDataService), false, false, false, false, LifeTime.PerResolveLifetimeManager)]
    public class cGenericWebScaffoldDataService : cDataService<cGenericWebScaffoldDataServiceContext, cBaseGenericWebScaffoldEntity>
    {
        public int IndividualLessonMinumumCredit { get; set; }
        public int IndividualLessonMaximumCredit { get; set; }
        public int ClassLessonMinumumCredit { get; set; }
        public int ClassLessonMaximumCredit { get; set; }
        public int MaximumClassCount { get; set; }
        public int SearchPagingCount { get; set; }
        public int CanReservationBeforeDay { get; set; }

        public int LessonMinute { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string EmailDisplayName { get; set; }
        public bool SmtpSslEnabled { get; set; }
        public string MailFrom { get; set; }

        public string SiteUrl { get; set; }

        public int RatingShowMinimumLimit { get; set; }
        public int LessonCanStartBeforeMinute { get; set; }
        public string SiteShortName { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public int Percent { get; set; }
        public int PayTime { get; set; }
        public string CompanyTitle { get; set; }
        public string Mersis { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string ProviderCompany { get; set; }
        public string PostAddress { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string VKLink { get; set; }
        public string YoutubeLink { get; set; }
        public string CookieAlert { get; set; }
        public bool HeaderShow { get; set; }
        public bool FooterShow { get; set; }
        public bool LanguageSelection { get; set; }
        public string IyzicoApiKey { get; set; }
        public string IyzicoSecretKey { get; set; }
        public string IyzicoBaseUrl { get; set; }
        public string IyzicoApiKeySandBox { get; set; }
        public string IyzicoSecretKeySandBox { get; set; }
        public string IyzicoBaseUrlSandBox { get; set; }
        public int CreditExpireTime { get; set; }
        public int ManuelPackageId { get; set; }
        public double TeacherLessonOneCreditToTL { get; set; }
        public int PaymentAdminUserId { get; set; }
        public int PaymentTestAdminUserId { get; set; }
        public bool MultipleAddressDefination { get; set; }
        public int WaitForFlowCheck { get; set; }
        public int ActivationReminderDay { get; set; }
        public int ActivationReminderDeadline { get; set; }
        public string MssqlBackupPath { get; set; }
        public string EFaturaPath { get; set; }
        public string WhatsAppNumber { get; set; }
        public string ConfigBackupPath { get; set; }
        public long UploadFileMaxSize { get; set; }
        public string SupportMail { get; set; }

        public int AutoCompleteSimilarityTest { get; set; }

        public int SearchSimilarityTest { get; set; }


        public bool ShowTagChipsInSellerLessonPage { get; set; }
        public string DefaultVideoLink { get; set; }
        public int SellerAgeLimit { get; set; }
        public int CustomerAgeLimit { get; set; }
        public bool UnloginSearchPermission { get; set; }
        public int NameCharacterLimit { get; set; }
        public int SurnameCharacterLimit { get; set; }

        public int PaymentApproveDay { get; set; }
        public int PaymentRefundDay { get; set; }
        public string BatchJobUrl { get; set; }
        public int CreditExample { get; set; }
        public int PasswordLimit { get; set; }
        public int CancelReservationHourAgo { get; set; }

        public List<object> GlobalParamList = null;
        public List<object> PublicParamList = null;
        public List<object> PrivateParamList = null;

        public bool FrontEndDebugMessage { get; set; }

        public bool BackendDebugMessageShowToUser { get; set; }
        public string Parasut_BaseUrl { get; set; }
        public string Parasut_Username { get; set; }
        public string Parasut_Password { get; set; }
        public string Parasut_ClientID { get; set; }
        public string Parasut_ClientSecret { get; set; }
        public string Parasut_MerchantID { get; set; }
        public int NonPaidReservationDeletionTimeSec { get; set; }
        public bool InvoicePaymentState { get; set; }
        public cGenericWebScaffoldDataService(cGenericWebScaffoldDataServiceContext _DatabaseContext)
            : base(_DatabaseContext)
        {
            //             LoadDB();
        }

        public override void LoadGlobalParams()
        {
            GlobalParamList = new List<object>();
            PublicParamList= new List<object>();
            PrivateParamList = new List<object>();

            cParamsDataManager __ParamsDataManager = App.Factories.ObjectFactory.ResolveInstance<cParamsDataManager>();
            for (int i = 0; i < DefaultGlobalParamsIDs.TypeList.Count; i++)
            {
                cGlobalParamEntity __GlobalParamEntity = __ParamsDataManager.GetParamByCode(DefaultGlobalParamsIDs.TypeList[i].Code);
                Type __Type = Type.GetType(__GlobalParamEntity.TypeFullName);
                try
                {
                    object __TempValue = Convert.ChangeType(__GlobalParamEntity.Value, __Type);
                    var __ThisType = this.GetType();
                    __ThisType.SetPropertyValue(this, __GlobalParamEntity.Code, __TempValue);

                    GlobalParamList.Add(new
                    {
                        ParamName = DefaultGlobalParamsIDs.TypeList[i].Code,
                        ParamValue = __TempValue
                    });
                    if (DefaultGlobalParamsIDs.TypeList[i].IsPrivate)
                    {
                        PrivateParamList.Add(new
                        {
                            ParamName = DefaultGlobalParamsIDs.TypeList[i].Code,
                            ParamValue = __TempValue
                        });
                    }
                    else
                    {
                        PublicParamList.Add(new
                        {
                            ParamName = DefaultGlobalParamsIDs.TypeList[i].Code,
                            ParamValue = __TempValue
                        });
                    }
                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    throw _Ex;
                }
            }

        }

        public override void SetDBParams(string _Database)
        {
            Database.Catalogs.DatabaseOperationsSQLCatalog.SetDbLevelParams(_Database);
        }

        public override void InvokeTransactionalAction(Func<bool> _ServiceMethod)
        {
            try
            {
                List<MethodBase> __Methods = App.Handlers.StackHandler.GetMethods("InvokeTransactionalAction", 0);

                if (__Methods.Where(__Item => __Item.DeclaringType.Name == this.GetType().Name).ToList().Count < 2)
                {
                    if (_ServiceMethod())
                    {
                        Database.DefaultConnection.Commit();
                    }
                    else
                    {
                        Database.DefaultConnection.Rollback();
                    }
                }
                else
                {
                    throw new Exception("ic ice aclilan transation mevcut..!");
                }
            }
            catch (Exception ex)
            {
                App.Loggers.SqlLogger.LogError(ex);
                Database.DefaultConnection.Rollback();
                throw;
            }
        }
    }
}
;