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
using Data.GenericWebScaffold.nDefaultValueTypes;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cRoleDataManager : cBaseDataManager
    {
        public cRoleDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

        public cRoleEntity GetRoleByCode(string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cRoleEntity __User = __DataService.Database.Query<cRoleEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Code).Eq(_Code)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __User;
        }


        public void CreateRuleByCodeAndNameIfNotExists(RoleIDs _RoleID)
        {
            CreateRuleByCodeAndNameIfNotExists(_RoleID.Name, _RoleID.Code);
        }
        public void CreateRuleByCodeAndNameIfNotExists(string _Name, string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cRoleEntity __RoleEntity = GetRoleByCode(_Code);
            if (__RoleEntity == null)
            {
                __RoleEntity = __DataService.Database.CreateNew<cRoleEntity>();
                __RoleEntity.Name = _Name;
                __RoleEntity.Code = _Code;
                __RoleEntity.Save();
            }
        }
        public bool CheckIfNotExistsRoleMenu(long _RoleID, long _MenuID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();


            cRoleMenuEntity __RoleMenuEntityAlias = null;



            int __IsHaveRoleMenu = __DataService.Database.Query<cRoleMenuEntity>(() => __RoleMenuEntityAlias)
              .SelectCount()
              .Where()
                 .Operand(() => __RoleMenuEntityAlias, "RoleID").Eq(_RoleID)
                 .And
                 .Operand(() => __RoleMenuEntityAlias, "MenuID").Eq(_MenuID)
                 .ToQuery()
               .ToCount();
            return __IsHaveRoleMenu > 0 ? true : false;
        }
        public void AddMenuToRole(cRoleEntity _Role, cMenuEntity _Menu)
        {
            if (!ControlRoleMenueExists(_Role, _Menu))
            { 
                _Role.Menus.AddValue(_Menu);
            }
            if (!CheckIfNotExistsRoleMenu(_Role.ID, _Menu.ID))
            {
                IDataService __DataService = DataServiceManager.GetDataService();
                cRoleMenuEntity __RoleMenuEntity = __DataService.Database.CreateNew<cRoleMenuEntity>();
                __RoleMenuEntity.SortValue = _Menu.SortValue;
                __RoleMenuEntity.Save(_Role, _Menu);
            }
        }

        public bool ControlRoleMenueExists(cRoleEntity _Role, cMenuEntity _Menu)
        {
            return _Role.Menus.ToList().Exists(__Item => __Item.Code == _Menu.Code);
        }


    }
}
