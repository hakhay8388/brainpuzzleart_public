using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;

namespace Data.GenericWebScaffold.nDefaultValueTypes
{
    public class MenuIDs : cBaseConstType<MenuIDs>
    {
        public static List<MenuIDs> TypeList { get; set; }

        //////////// Global Pages ////////////

        public static MenuIDs MainPage = new MenuIDs(null, MenuTypes.LeftMenu, GetVariableName(() => MainPage), "MainPage", "fas fa-home", 0, true);


        public MenuTypes MenuType { get; set; }
        public string Icon { get; set; }

        public MenuIDs RootMenu { get; set; }

		public bool IsMainMenu { 
			get
			{
				List<MenuIDs>  __SubMenu = TypeList.Where(__Item => __Item.RootMenu != null && __Item.RootMenu.Code == this.Code).ToList();
				return __SubMenu.Count > 0;
			}
		}

		public bool UnloginedPage { get; set; }


        public MenuIDs(MenuIDs _RootMenu, MenuTypes _MenuType, string _Code, string _Name, string _Icon, int _SortValue, bool _UnloginedPage)
            : base(_Name, _Code, _SortValue)
        {
            TypeList = TypeList ?? new List<MenuIDs>();
            TypeList.Add(this);
            RootMenu = _RootMenu;
            MenuType = _MenuType;
            Icon = _Icon;
            UnloginedPage = _UnloginedPage;
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static MenuIDs GetByID(int _ID, MenuIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static MenuIDs GetByName(string _Name, MenuIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static MenuIDs GetByCode(string _Code, MenuIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }

/*        public static List<MenuIDs> GetSubMenus(MenuIDs _RootMenu)
        {
	        return TypeList.Where(__Item => __Item.RootMenu = !null && __Item.RootMenu.ID == _RootMenu.ID).ToList()();
	     
        }*/

        }
    }
