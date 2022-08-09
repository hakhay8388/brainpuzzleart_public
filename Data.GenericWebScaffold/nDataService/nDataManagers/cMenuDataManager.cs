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
    public class cMenuDataManager : cBaseDataManager
    {
        public cMenuDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

        public cMenuEntity GetMenuByCode(string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cMenuEntity __MenuEntity = __DataService.Database.Query<cMenuEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Code).Eq(_Code)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __MenuEntity;
        }

        public cMenuEntity AddMenu(string _MenuTypeCode, string _Name, string _Code, string _Icon, int _SortValue, cPageEntity _PageEntity)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cMenuEntity __MenuEntity = __DataService.Database.CreateNew<cMenuEntity>();
            __MenuEntity.Name = _Name;
            __MenuEntity.Code = _Code;
            __MenuEntity.Icon = _Icon;
            __MenuEntity.MenuTypeCode = _MenuTypeCode;
            __MenuEntity.SortValue = _SortValue;
            __MenuEntity.Save();
			if (_PageEntity != null)
			{
				__MenuEntity.Page.AddValue(_PageEntity);
			}
            return __MenuEntity;
        }
        public cMenuEntity UpdateMenu(cMenuEntity __MenuEntity, string _MenuTypeCode, string _Name, string _Code, string _Icon, int _SortValue)
        {
			__MenuEntity.Name = _Name;
            __MenuEntity.Code = _Code;
            __MenuEntity.Icon = _Icon;
            __MenuEntity.MenuTypeCode = _MenuTypeCode;
            __MenuEntity.SortValue = _SortValue;
            __MenuEntity.Save();
            return __MenuEntity;
        }

        public cMenuEntity CreateMenuIfNotExists(string _MenuTypeCode, string _Name, string _Code, string _Icon,
            int _SortValue, cPageEntity _PageEntity)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cMenuEntity __MenuEntity = GetMenuByCode(_Code);
            if (__MenuEntity == null)
            {
                __MenuEntity = AddMenu(_MenuTypeCode, _Name, _Code, _Icon, _SortValue, _PageEntity);
            }
#if DEBUG
            else
            {
                __MenuEntity = UpdateMenu(__MenuEntity, _MenuTypeCode, _Name, _Code, _Icon, _SortValue);
            }
#endif
            return __MenuEntity;
        }

        public cMenuEntity CreateMenuIfNotExists(MenuIDs _MenuID, cPageEntity _PageEntity = null)
        {
            return CreateMenuIfNotExists(_MenuID.MenuType.Code, _MenuID.Name, _MenuID.Code, _MenuID.Icon, _MenuID.ID, _PageEntity);
        }

        public cMenuEntity CreateSubMenuIfNotExists(MenuIDs _RootMenuID, string _MenuTypeCode, string _Name, string _Code, string _Icon, int _SortValue, cPageEntity _PageEntity)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cMenuEntity __MenuEntity = GetMenuByCode(_Code);
            if (__MenuEntity == null)
            {
                __MenuEntity = AddMenu(_MenuTypeCode, _Name, _Code, _Icon, _SortValue,  _PageEntity);
            }

            cMenuEntity __RootMenuEntity = GetMenuByCode(_RootMenuID.Code);
            __MenuEntity.RootMenu = __RootMenuEntity;
			__MenuEntity.MenuTypeCode = _MenuTypeCode;
			__MenuEntity.Save();
            return __MenuEntity;
        }

        public cMenuEntity CreateSubMenuIfNotExists(MenuIDs _RootMenuID, MenuIDs _MenuID, cPageEntity _PageEntity)
        {
            return CreateSubMenuIfNotExists(_RootMenuID, _MenuID.MenuType.Code, _MenuID.Name, _MenuID.Code, _MenuID.Icon, _MenuID.ID, _PageEntity);
        }

       /* public List<object> GetSubMenus(List<MenuIDs> _MenuIDsList)
        {
            List<object> __List = new List<object>();
            foreach (MenuIDs _Menu in _MenuIDsList)
            {
                cMenuEntity __MenuEntity = GetMenuByCode(_Menu.Code);
                cPageEntity __Page = __MenuEntity.Page.GetValue();
                __List.Add(new
                {
                    url = __Page.Url,
                    icon = __MenuEntity.Icon,
                    name = __MenuEntity.Name,
                    Active = false
                });
                
            }
            return __List;
        }*/
    }
}
