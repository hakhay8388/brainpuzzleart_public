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
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.Boundary.nData;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders.nLoaderIDs;

namespace Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders
{
    public class cDefaultUsersDataLoader : cBaseDataLoader
    {
        public int AdminCount = 10;
        public int SellerCount = 100;
        public int CustomerCount = 100;
        public int DeveloperCount = 100;

        public cRoleDataManager RoleDataManager { get; set; }
        public cUserDataManager UserDataManager { get; set; }
        public cDefaultUsersDataLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cChecksumDataManager _ChecksumDataManager
            , cRoleDataManager _RoleDataManager
            , cUserDataManager _UserDataManager
            )
          : base(LoaderIDs.DefaultUsersDataLoader, _CoreServiceContext, _DataServiceManager, _FileDataService, _ChecksumDataManager)
        {
            RoleDataManager = _RoleDataManager;
            UserDataManager = _UserDataManager;
        }

        public void Init(IDataService _DataService)
        {
            cDefaultDataChecksumEntity __DBCheckSum = ChecksumDataManager.GetCheckSumByCode(LoaderID.Code);
            string __TotalString = "AdminCount=" + AdminCount.ToString() + ";" + "SellerCount=" + SellerCount.ToString() + ";" + "CustomerCount=" + CustomerCount.ToString() + ";" + "DeveloperCount=" + DeveloperCount.ToString() + ";";
            string __StringCheckSum = App.Handlers.StringHandler.ComputeHashAsHex(__TotalString);

            if (__DBCheckSum == null || __StringCheckSum != __DBCheckSum.CheckSum)
            {
                InitAdmins(_DataService);
                InitSeller(_DataService);
                InitCustomer(_DataService);
                InitDeveloper(_DataService);

                ChecksumDataManager.CreateCheckSumIfNotExists(LoaderID.Code, __StringCheckSum);
            }
        }



        public void InitAdmins(IDataService _DataService)
        {
            for (var i = 0; i < AdminCount; i++)
            {
                string __Add = "";
                if (i > 0)
                {
                    __Add = i.ToString();
                }
                cUserEntity __Admin = null;
                try
                {
                    if (UserDataManager.GetUserByEmail("admin" + __Add + "@admin" + __Add + ".com") == null)
                    {
                        __Admin = _DataService.Database.CreateNew<cUserEntity>();
                        __Admin.Name = "Sistem" + __Add;
                        __Admin.Surname = "Sistem" + __Add;
                        __Admin.RealSurname = "Sistem" + __Add;
                        __Admin.Email = "admin" + __Add + "@admin" + __Add + ".com";
                        __Admin.Password = "1";
                        __Admin.State = EUserState.Confirmed.ID;
                        __Admin.Save();

                        cUserDetailEntity __UserDetail = _DataService.Database.CreateNew<cUserDetailEntity>();
                        __UserDetail.Telephone = (!__Add.IsNullOrEmpty() ? __Add + "-" + __Add + "-" + __Add : "");
                        __UserDetail.Save(__Admin);

                        cActorEntity __Actor = __Admin.Actor.CreateNew();
                        __Actor.Name = __Admin.Name;
                        __Actor.UserVisibleGroup = EUserVisibleGroups.AllUser.ID;
                        __Actor.Save();

                        if (__Actor.Roles.Count < 1)
                        {
                            cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.Admin.Code);
                            __Actor.Roles.AddValue(__RoleEntity);
                        }

                        __Admin.Actor.SetValue(__Actor);

                    }

                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    throw _Ex;
                }
            }
        }

        public void InitSeller(IDataService _DataService)
        {

            Random __RandomUniversities = new Random();
            Random __RandomEducationLevels = new Random();

            for (var i = 0; i < SellerCount; i++)
            {
                string __Add = "";
                if (i > 0)
                {
                    __Add = i.ToString();
                }
                cUserEntity __Seller = null;
                try
                {
                    if (UserDataManager.GetUserByEmail("seller" + __Add + "@seller" + __Add + ".com") == null)
                    {
                        __Seller = _DataService.Database.CreateNew<cUserEntity>();
                        __Seller.Name = "Satıcı" + __Add;
                        __Seller.Surname = "Satıcı" + __Add;
                        __Seller.RealSurname = "Satıcı" + __Add;
                        __Seller.Email = "seller" + __Add + "@seller" + __Add + ".com";
                        __Seller.Password = "1";
                        __Seller.State = EUserState.Confirmed.ID;
                        __Seller.Usernick = "tempseller" + __Add;
                        __Seller.Save();

                        cUserDetailEntity __UserDetail = _DataService.Database.CreateNew<cUserDetailEntity>();
                        __UserDetail.Telephone = (!__Add.IsNullOrEmpty() ? __Add + "-" + __Add + "-" + __Add : "");
                        __UserDetail.Save(__Seller);

                        cActorEntity __Actor = __Seller.Actor.CreateNew();
                        __Actor.Name = __Seller.Name;
                        __Actor.Save();

                        __Actor.SellerDetail.Save(__Actor);


                        /*cSellerConfirmationRequestEntity __SellerConfirmationRequestEntity = _DataService.Database.CreateNew<cSellerConfirmationRequestEntity>();
						__SellerConfirmationRequestEntity.RequestType = ESellerRequestType.ConfirmationRequest.ID;
						__SellerConfirmationRequestEntity.RequestState = ESellerRequestState.Open.ID;
						__SellerConfirmationRequestEntity.Save(__Actor.SellerDetail);

						cActorType_SellerDetailNotConfirmedEntity __ActorType_SellerDetailNotConfirmedEntity = _DataService.Database.CreateNew<cActorType_SellerDetailNotConfirmedEntity>();
						__ActorType_SellerDetailNotConfirmedEntity.Save();

						__SellerConfirmationRequestEntity.SellerDetailNotConfirmed.SetValue(__ActorType_SellerDetailNotConfirmedEntity);*/


                        if (__Actor.Roles.Count < 1)
                        {
                            cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.Seller.Code);
                            __Actor.Roles.AddValue(__RoleEntity);
                        }

                        __Seller.Actor.SetValue(__Actor);

                    }
                    else
                    {
                        cUserEntity __UserEntity = UserDataManager.GetUserByEmail("seller" + __Add + "@seller" + __Add + ".com");

                    }

                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    throw _Ex;
                }
            }
        }

        public void InitCustomer(IDataService _DataService)
        {
            for (var i = 0; i < CustomerCount; i++)
            {
                string __Add = "";
                if (i > 0)
                {
                    __Add = i.ToString();
                }
                cUserEntity __Customer = null;
                try
                {
                    if (UserDataManager.GetUserByEmail("customer" + __Add + "@customer" + __Add + ".com") == null)
                    {
                        __Customer = _DataService.Database.CreateNew<cUserEntity>();
                        __Customer.Name = "Müşteri" + __Add;
                        __Customer.Surname = "Müşteri" + __Add;
                        __Customer.RealSurname = "Müşteri" + __Add;
                        __Customer.Email = "customer" + __Add + "@customer" + __Add + ".com";
                        __Customer.Password = "1";
                        __Customer.State = EUserState.Confirmed.ID;
                        __Customer.Save();

                        cUserDetailEntity __UserDetail = _DataService.Database.CreateNew<cUserDetailEntity>();
                        __UserDetail.Telephone = (!__Add.IsNullOrEmpty() ? __Add + "-" + __Add + "-" + __Add : "");
                        __UserDetail.Save(__Customer);

                        cActorEntity __Actor = __Customer.Actor.CreateNew();
                        __Actor.Name = __Customer.Name;
                        __Actor.UserVisibleGroup = EUserVisibleGroups.TestUser.ID;
                        __Actor.Save();

                        if (__Actor.Roles.Count < 1)
                        {
                            cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.Customer.Code);
                            __Actor.Roles.AddValue(__RoleEntity);
                        }

                        __Customer.Actor.SetValue(__Actor);

                    }

                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    throw _Ex;
                }
            }

        }
        public void InitDeveloper(IDataService _DataService)
        {
            for (var i = 0; i < DeveloperCount; i++)
            {
                string __Add = "";
                if (i > 0)
                {
                    __Add = i.ToString();
                }
                cUserEntity __Developer = null;
                try
                {
                    if (UserDataManager.GetUserByEmail("developer" + __Add + "@developer" + __Add + ".com") == null)
                    {
                        __Developer = _DataService.Database.CreateNew<cUserEntity>();
                        __Developer.Name = "Developer" + __Add;
                        __Developer.Surname = "Developer" + __Add;
                        __Developer.RealSurname = "Developer" + __Add;
                        __Developer.Email = "developer" + __Add + "@developer" + __Add + ".com";
                        __Developer.Password = App.Handlers.StringHandler.ComputeHashAsHex(__Developer.Email);
                        __Developer.State = EUserState.Confirmed.ID;
                        __Developer.Save();

                        cUserDetailEntity __UserDetail = _DataService.Database.CreateNew<cUserDetailEntity>();
                        __UserDetail.Telephone = (!__Add.IsNullOrEmpty() ? __Add + "-" + __Add + "-" + __Add : "");
                        __UserDetail.Save(__Developer);

                        cActorEntity __Actor = __Developer.Actor.CreateNew();
                        __Actor.Name = __Developer.Name;
                        __Actor.UserVisibleGroup = EUserVisibleGroups.AllUser.ID;
                        __Actor.Save();

                        __Actor.DeveloperDetail.Save(__Actor);

                        if (__Actor.Roles.Count < 1)
                        {
                            cRoleEntity __RoleEntity = RoleDataManager.GetRoleByCode(RoleIDs.Developer.Code);
                            __Actor.Roles.AddValue(__RoleEntity);
                        }

                        __Developer.Actor.SetValue(__Actor);

                    }

                }
                catch (Exception _Ex)
                {
                    App.Loggers.CoreLogger.LogError(_Ex);
                    throw _Ex;
                }
            }
        }
    }
}
