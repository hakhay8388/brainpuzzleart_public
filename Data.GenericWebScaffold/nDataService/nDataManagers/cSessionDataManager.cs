using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Base.Data.nDataServiceManager;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cSessionDataManager : cBaseDataManager
    {
        public cSessionDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }


        public cUserEntity GetUserBySessionID(string _SessionID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserEntity __Result = __DataService.Database.Query<cUserEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.ID).Eq
                (
                    __DataService.Database.Query<cUserSessionEntity>().SelectColumn<cUserEntity>().Where().Operand(__Item => __Item.SessionHash).Eq(_SessionID).ToQuery()
                )
                .ToQuery()
                .ToList()
                .FirstOrDefault();

            return __Result;
        }
        public cUserEntity GetUserBySessionIDFromTemp(string _SessionID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserEntity __Result = __DataService.Database.Query<cUserEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.ID).Eq
                (
                    __DataService.Database.Query<cUserSessionTempEntity>().SelectColumn<cUserEntity>().Where().Operand(__Item => __Item.SessionHash).Eq(_SessionID).ToQuery()
                )
                .ToQuery()
                .ToList()
                .FirstOrDefault();

            return __Result;
        }
        public cUserSessionTempEntity GetUserSessionBySessionIDFromTemp(string _SessionID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cUserSessionTempEntity __UserSessionTempEntity = __DataService.Database.Query<cUserSessionTempEntity>().SelectAll().Where().Operand(__Item => __Item.SessionHash).Eq(_SessionID).ToQuery().ToList().FirstOrDefault();


            return __UserSessionTempEntity;
        }
        public int DeleteOldSessionTempDate(DateTime _Date)
        {
            IDataService __DataService = DataServiceManager.GetDataService();


            return __DataService.Database.Delete<cUserSessionTempEntity>()
                   .Operand(__Item => __Item.CreateDate).Lt(_Date)
                   .ToQuery()
                   .ExecuteForDeleteAndUpdate();
        }
        public void DeleteSession(string _SessionID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            __DataService.Database.Delete<cUserSessionTempEntity>()
                   .Operand(__Item => __Item.SessionHash).Eq(_SessionID)
                   .ToQuery()
                   .ExecuteForDeleteAndUpdate();

            __DataService.Database.Delete<cUserSessionEntity>()
                   .Operand(__Item => __Item.SessionHash).Eq(_SessionID)
                   .ToQuery()
                   .ExecuteForDeleteAndUpdate();         }
        public cUserSessionEntity AddUserSession(cUserEntity _UserEntity, string _SessionID, string _IpAddress)
        {

            IDataService __DataService = DataServiceManager.GetDataService();

            cUserSessionEntity __SessionEntity = __DataService.Database.CreateNew<cUserSessionEntity>();
            __SessionEntity.SessionHash = _SessionID;
            __SessionEntity.IpAddress = _IpAddress;
            __SessionEntity.Save(_UserEntity);
            return __SessionEntity;
        }
        public cUserSessionTempEntity AddUserSessionTemp(cUserEntity _UserEntity, string _SessionID, string _IpAddress)
        {

            IDataService __DataService = DataServiceManager.GetDataService();
            cUserSessionTempEntity __OldUserSessionTempEntity = GetUserSessionBySessionIDFromTemp(_SessionID);
            if (__OldUserSessionTempEntity == null)
            {
                cUserSessionTempEntity __SessionEntity = __DataService.Database.CreateNew<cUserSessionTempEntity>();
                __SessionEntity.SessionHash = _SessionID;
                __SessionEntity.IpAddress = _IpAddress;
                __SessionEntity.Save(_UserEntity);
                return __SessionEntity;
            }
            else
            {
                return __OldUserSessionTempEntity;
            }

        }


        /*int blue = 0;


        //DataService.Perform<cPowerLineDataService, string>(Deneme, DataService);
        cSessionEntity __TempAlias = null;

        cSql __SqlStr = DataService.Database.Query<cSessionEntity>(() => __TempAlias).SelectAll().Where().Exists(
            DataService.Database.Query<cUserEntity>().SelectID().Where().Operand(__Item => __Item.ID).Eq(() => __TempAlias).ToQuery()
            ).ToQuery().ToSql();

        DataTable __Table3 = DataService.Database.DefaultConnection.Query(__SqlStr);

        //DataService.Database.Query<cSessionEntity>().Left().Join().On().EndFilter().Where().Operand().Ge().EndFilter().



        cSql __Sql = DataService.Database.Query<cSessionEntity>(() => __TempAlias).Max(__Item => __Item.ID).SelectColumn<cUserEntity>(__Item => __Item.ID, __Item => __Item.SessionHash)
           .Inner<cUserEntity>().Join
           (
                DataService.Database.Query<cUserEntity>().SelectColumn(__Item => __Item.Surname, __Item => __Item.ID)
           )
           .On()
           .Operand(__User => __User.ID).Eq(() => __TempAlias)
           .ToQuery()
           .Where()
           .Operand<cUserEntity>().Between(1, () => __TempAlias.ID)
           .ToQuery()
           .GroupBy<cUserEntity>(__Item => __Item.ID, __Item => __Item.SessionHash).Having().Count<cUserEntity>().Lt(10).ToQuery()
           .OrderBy().Asc(__Item => __Item.ID, __Item => __Item.SessionHash).ToQuery()
           //.RowNumber().OrderBy().Asc<cUserEntity>().ToQuery()
           //.Take(2, 5)
           //.Take(3, 4)
           .ToSql();

        DataTable __Table2 = DataService.Database.DefaultConnection.Query(__Sql);


        cUserEntity __TempUserAlias = null;

        IQuery __Query = DataService.Database.Query<cUserEntity>().SelectAll();
        //IQuery __Query2 = DataService.Database.Query<cUserEntity>(__Query).SelectAll();
        cQuery<cUserEntity> __Query2 = (cQuery<cUserEntity>)DataService.Database.Query<cUserEntity>(() => __TempUserAlias, __Query)
            .Max(__Item => __Item.ID)
            .Where()
            .Operand(__User => __User.ID).Eq(2)
            .And
            .Operand(__User => __User.Name).Eq("Hakan")
            .And
            .Exists(DataService.Database.Query<cSessionEntity>().SelectAll().Where().Operand<cUserEntity>().Eq(() => __TempUserAlias.ID).ToQuery())
            .ToQuery();
        __Query2.Left<cSessionEntity>().Join().On()
                .Operand("UserID").EqAny("aa","bb")
                .And
                .Operand("UserID").EqAny(3, 5, 7)
                .And
                .Operand(__Item=> __Item.SessionHash).Like("%5")
            .ToQuery();
        cSql __aa = __Query2.Cross().Apply(
            DataService.Database.Query<cSessionEntity>().SelectAll().Where().Operand<cUserEntity>().Eq(() => __TempUserAlias.ID).ToQuery()
            ).EndApply()

            //.GroupBy().End()



            .ToSql();
        //cSql __aa2 = __Query.ToSql();

        //IQuery __Query3 = DataService.Database.Query<cSessionEntity>(__Query2).SelectAll().Where().Operand("SessionID").Eq(15).End();

        // cSql __aa = __Query3.ToSql();
        DataTable __Table = DataService.Database.DefaultConnection.Query(__aa);
        //cSql __Sql = DataService.Database.Query<cUserEntity>(__Query).SelectAllColumns().ToSql();*/
    }
}
