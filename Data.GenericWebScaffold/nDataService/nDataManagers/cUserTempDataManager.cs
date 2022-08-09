using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.Boundary.nData;
using Base.Boundary.nData;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cUserTempDataManager : cBaseDataManager
    {
        public cRoleDataManager RoleDataManager { get; set; }
        public cUserTempDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService, cRoleDataManager _RoleDataManager)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
            RoleDataManager = _RoleDataManager;
        }
        

        public cUserTempEntity UpdateToken(cUserTempEntity _UserTempEntity)
        {
            cGenericWebScaffoldDataService __DataServiceGlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

            _UserTempEntity.Token = App.Handlers.StringHandler.EncodeGetUniqueTimeToken();
            _UserTempEntity.GuidStr = Guid.NewGuid().ToString();
            _UserTempEntity.LastSendMail = DateTime.Now;
            _UserTempEntity.SendMailEndDate = DateTime.Now.AddDays(__DataServiceGlobalParams.ActivationReminderDeadline);
            _UserTempEntity.Save();
            return _UserTempEntity;
        }
        public cUserTempEntity UpdateLastSendMail(long _UserTempID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserTempEntity __UserTempEntity = __DataService.Database.GetEntityByID<cUserTempEntity>(_UserTempID);
            __UserTempEntity.LastSendMail = DateTime.Now;
            __UserTempEntity.Save();
            return __UserTempEntity;
        }

        public int DeleteUserTemp(string _Token)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            return __DataService.Database.Delete<cUserTempEntity>()
                  .Operand(__Item => __Item.Token).Eq(_Token)
                  .ToQuery()
                  .ExecuteForDeleteAndUpdate();
        }
        public int DeleteUserTempByEmail(string _Email)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            return __DataService.Database.Delete<cUserTempEntity>()
                  .Operand(__Item => __Item.Email).Eq(_Email)
                  .ToQuery()
                  .ExecuteForDeleteAndUpdate();
        }
        public cUserTempEntity GetUserTempByUserID(long _UserTempID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserTempEntity __UserTempEntity = __DataService.Database.GetEntityByID<cUserTempEntity>(_UserTempID);
            return __UserTempEntity;
        }
        public List<cUserTempEntity> GetReminderUserList()
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cGenericWebScaffoldDataService __DataServiceGlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

            cUserTempEntity __UserTempAlias = null;

            cQuery<cUserTempEntity> __Query = __DataService.Database.Query<cUserTempEntity>(() => __UserTempAlias)
                .SelectAliasAllColumns<cUserTempEntity>(() => __UserTempAlias);

            __Query.Where()
                .Operand<cUserTempEntity>(() => __UserTempAlias, __Item => __Item.SendMailEndDate).Ge(DateTime.Now)
                .And
                .DateDiff<cUserTempEntity>(EMssqlDateInterval.DAY, () => __UserTempAlias, __Item => __Item.LastSendMail, DateTime.Now).Ge(__DataServiceGlobalParams.ActivationReminderDay)
                .ToQuery();


            return __Query.ToList();
        }
        public cUserTempEntity GetUserTempByGuid(string _GuidStr)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cUserTempEntity __UserTempAlias = null;

            cQuery<cUserTempEntity> __Query = __DataService.Database.Query<cUserTempEntity>(() => __UserTempAlias)
                .SelectAliasAllColumns<cUserTempEntity>(() => __UserTempAlias);

            __Query.Where()
                .Operand<cUserTempEntity>(() => __UserTempAlias, __Item => __Item.GuidStr).Eq(_GuidStr)
                .ToQuery();


            return __Query.ToList().FirstOrDefault();
        }

        public cUserTempEntity AddTempUser(string _Name, string _Surname, string _Email, string _Telephone, string _Password, int? _EducationLevel, DateTime _DateOfBirth, bool _IsSeller, long? _UniversitySection, string _OtherUniversity, string _OtherSection)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cGenericWebScaffoldDataService __DataServiceGlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            cUserTempEntity __UserTemp = __DataService.Database.CreateNew<cUserTempEntity>();
            __UserTemp.Name = _Name;
            __UserTemp.Surname = _Surname;
            __UserTemp.RealSurname = _Surname;
            __UserTemp.Email = _Email;
            __UserTemp.Password = _Password;
            __UserTemp.DateOfBirth = _DateOfBirth;
            __UserTemp.IsSeller = _IsSeller;
            __UserTemp.Telephone = _Telephone;
            __UserTemp.OtherUniversity = _OtherUniversity;
            __UserTemp.OtherSection = _OtherSection;

            __UserTemp.Token = App.Handlers.StringHandler.EncodeGetUniqueTimeToken();
            __UserTemp.GuidStr = Guid.NewGuid().ToString();
            __UserTemp.LastSendMail = DateTime.Now;
            __UserTemp.SendMailEndDate = DateTime.Now.AddDays(__DataServiceGlobalParams.ActivationReminderDeadline);

            __UserTemp.Save();
            return __UserTemp;
        }

        public cUserTempEntity GetRegistrationByMail(string _Mail)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserTempEntity __UserTempEntity = __DataService.Database.Query<cUserTempEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Email).Eq(_Mail)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __UserTempEntity;
        }


        public cUserTempEntity GetRegistrationByUserNick(string _Usernick)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserTempEntity __UserTempEntity = __DataService.Database.Query<cUserTempEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Usernick).Eq(_Usernick)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __UserTempEntity;
        }

        

    }
}
