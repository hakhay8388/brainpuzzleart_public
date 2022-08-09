using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDefaultValueTypes
{
    public class PageIDs : cBaseConstType<PageIDs>
    {

        public static List<PageIDs> TypeList { get; set; }

        /////////// Global Pages ////////////

        public static PageIDs MainPage = new PageIDs(GetVariableName(() => MainPage), "MainPage", "mainpage", "TMainPage", 0, true);
        public static PageIDs LoginPage = new PageIDs(GetVariableName(() => LoginPage), "LoginPage", "login", "TLogin", 0, true);

    
        public string Url { get; set; }
        public string Component { get; set; }
        public string[] SubParamName { get; set; }
        public bool UnloginedPage { get; set; }
        public string OriginalCode { get; set; }

        public PageIDs(string _Code, string _Name, string _Url, string _Component, int _ID, bool _UnloginedPage, string[] _SubParamName = null)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<PageIDs>();
            this.Url = _Url.LowerConvertToEnglishCharacter();
            this.OriginalCode = _Code;
            this.Code = this.Code.LowerConvertToEnglishCharacter();
            TypeList.Add(this);
            
            UnloginedPage = _UnloginedPage;
            Component = _Component;
            SubParamName = _SubParamName == null ? new string[] { } : _SubParamName;

        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static PageIDs GetByID(int _ID, PageIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static PageIDs GetByName(string _Name, PageIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static PageIDs GetByCode(string _Code, PageIDs _DefaultID)
        {

            return GetByCode(TypeList, _Code.LowerConvertToEnglishCharacter(), _DefaultID);
        }
        public static PageIDs GetByUrl(string _Url)
        {
            PageIDs __Item = TypeList.Find(_Item => _Item.Url == _Url);
            return __Item;
        }
    }
}
