using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nGlobalDataServices.nEntityServices.nEntities;

namespace Data.GenericWebScaffold.nGlobalDataServices.nDataManagers
{
    public class cProfileDataManager : cBaseDataManager
    {
        public cProfileDataManager(cGlobalDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }


        public cProfileEntity GetProfileByHostName(string _HostName)
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();
            cProfileEntity __Result = __DataService.Database.Query<cProfileEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.HostName).Eq(_HostName)
                .ToQuery()
                .ToList()
                .FirstOrDefault();

            return __Result;
        }

        public void CreateProfile(
            string _HostName
            , string _Email
            , string _Name
            , string _Surname
            , string _Telephone
            , DateTime _EndDate
            , string _DBUserId
            , string _DBPassword
            , string _DBServer
            , string _DBName
            , int _DBMaxConnectionCount
            )
        {
            IDataService __DataService = DataServiceManager.GetGlobalDataService();

            cProfileEntity __ProfileEntity = GetProfileByHostName(_HostName);
            if (__ProfileEntity == null)
            {
                __ProfileEntity = __DataService.Database.CreateNew<cProfileEntity>();
                __ProfileEntity.Email = "";
                __ProfileEntity.HostName = _HostName;
                __ProfileEntity.Name = _Name;
                __ProfileEntity.Surname = _Surname;
                __ProfileEntity.Telephone = _Telephone;
                __ProfileEntity.EndDate = _EndDate;
                __ProfileEntity.Save();

                if (!__ProfileEntity.DBSetting.IsValid)
                {
                    __ProfileEntity.DBSetting.Password = _DBPassword;
                    __ProfileEntity.DBSetting.UserId = _DBUserId;
                    __ProfileEntity.DBSetting.Server = _DBServer;
                    __ProfileEntity.DBSetting.DBName = _DBName;
                    __ProfileEntity.DBSetting.MaxConnectionCount = _DBMaxConnectionCount;
                    __ProfileEntity.DBSetting.Save(__ProfileEntity);

                }
            }
        }
    }
}
