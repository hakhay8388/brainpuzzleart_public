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
    public class cPageDataManager : cBaseDataManager
    {
        public cPageDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

        public cPageEntity GetPageByUrl(string _Url)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cPageEntity __Page = __DataService.Database.Query<cPageEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Url).Eq(_Url)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __Page;
        }

        public cPageEntity AddPage(string _Name, string _Code, string _Url, string _ComponentName)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cPageEntity __PageEntity = __DataService.Database.CreateNew<cPageEntity>();
            __PageEntity.Name = _Name;
            __PageEntity.Code = _Code;
            __PageEntity.Url = _Url;
            __PageEntity.ComponentName = _ComponentName;
            __PageEntity.Save();
            return __PageEntity;
        }
        public cPageEntity UpdatePage(cPageEntity __PageEntity, string _Name, string _Code, string _Url, string _ComponentName)
        {

            __PageEntity.Name = _Name;
            __PageEntity.Code = _Code;
            __PageEntity.Url = _Url;
            __PageEntity.ComponentName = _ComponentName;
            __PageEntity.Save();
            return __PageEntity;
        }

        public void CreatePageIfNotExists(PageIDs _PageID)
        {
            CreatePageIfNotExists(_PageID.Name, _PageID.Code, _PageID.Url, _PageID.Component);
        }

        public void CreatePageIfNotExists(string _Name, string _Code, string _Url, string _ComponentName)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cPageEntity __PageEntity = GetPageByUrl(_Url);
            if (__PageEntity == null)
            {
                AddPage(_Name, _Code, _Url, _ComponentName);
            }
#if DEBUG
            else
            {

                UpdatePage(__PageEntity, _Name, _Code, _Url, _ComponentName);

            }
#endif
        }

        public void AddPageToRole(cRoleEntity _Role, cPageEntity _Page)
        {
            if (_Page != null && !ControlRoleMenueExists(_Role, _Page))
            {
                _Role.Pages.AddValue(_Page);
            }
        }

        public bool ControlRoleMenueExists(cRoleEntity _Role, cPageEntity _Page)
        {
            return _Role.Pages.ToList().Exists(__Item => __Item.Code == _Page.Code);
        }

        public bool IsThereSameNamedUrlForUserNick(string _UserNick)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            var __UsernickCount = __DataService.Database.Query<cPageEntity>()
                .SelectCount()
                .Where()
                .Operand(__Item => __Item.Url).Eq(_UserNick).ToQuery().ToCount();

            return __UsernickCount > 0;   
        }
    }
}
