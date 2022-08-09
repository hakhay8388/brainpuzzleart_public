using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders.nLoaderIDs
{
    public class LoaderIDs : cBaseConstType<LoaderIDs>
    {
        public static List<LoaderIDs> TypeList { get; set; }


		public static LoaderIDs LanguageDataLoader = new LoaderIDs(GetVariableName(() => LanguageDataLoader), 1);
		public static LoaderIDs GlobalParamsDataLoader = new LoaderIDs(GetVariableName(() => GlobalParamsDataLoader), 2);
		public static LoaderIDs DefaultUsersDataLoader = new LoaderIDs(GetVariableName(() => DefaultUsersDataLoader), 3);
		public static LoaderIDs RoleMenuLoader = new LoaderIDs(GetVariableName(() => RoleMenuLoader), 4);
		public static LoaderIDs PageDataLoader = new LoaderIDs(GetVariableName(() => PageDataLoader), 5);
		public static LoaderIDs RolePageLoader = new LoaderIDs(GetVariableName(() => RolePageLoader), 6);
		public static LoaderIDs MenuDataLoader = new LoaderIDs(GetVariableName(() => MenuDataLoader), 7);
		public static LoaderIDs RoleDataLoader = new LoaderIDs(GetVariableName(() => RoleDataLoader), 8);
		public static LoaderIDs LessonDataLoader = new LoaderIDs(GetVariableName(() => LessonDataLoader), 9);
		public static LoaderIDs RoleDataSourcePermissionLoader = new LoaderIDs(GetVariableName(() => RoleDataSourcePermissionLoader), 10);
		public static LoaderIDs RoleDataSourceColumnLoader = new LoaderIDs(GetVariableName(() => RoleDataSourceColumnLoader), 11);
		public static LoaderIDs UniversityDataLoader = new LoaderIDs(GetVariableName(() => UniversityDataLoader), 12);
		public static LoaderIDs TemplateDataLoader = new LoaderIDs(GetVariableName(() => TemplateDataLoader), 13);
		public static LoaderIDs CountryDataLoader = new LoaderIDs(GetVariableName(() => CountryDataLoader), 14);
		public static LoaderIDs ClassLevelDataLoader = new LoaderIDs(GetVariableName(() => ClassLevelDataLoader), 15);
		public static LoaderIDs CreditPackageDataLoader = new LoaderIDs(GetVariableName(() => CreditPackageDataLoader), 16);
		public static LoaderIDs OrientationFlowDataLoader = new LoaderIDs(GetVariableName(() => OrientationFlowDataLoader), 17);
		
		/// <summary>
		/// /////////////////////////
		/// Excel Loaders
		/// /////////////////////////
		/// </summary>
		public static LoaderIDs LessonLanguageLoader = new LoaderIDs(GetVariableName(() => LessonLanguageLoader), 1000);
		public static LoaderIDs UniversitiesLanguageLoader = new LoaderIDs(GetVariableName(() => UniversitiesLanguageLoader), 1001);
		public static LoaderIDs ConfigBackupDataLoader = new LoaderIDs(GetVariableName(() => ConfigBackupDataLoader), 1002);
		public static LoaderIDs FeeTypeDataLoader = new LoaderIDs(GetVariableName(() => FeeTypeDataLoader), 1003);
		public static LoaderIDs ParasutSafeTypeDataLoader = new LoaderIDs(GetVariableName(() => ParasutSafeTypeDataLoader), 1004);
		

		/// /////////////////////////
		/// /// /////////////////////////

		public LoaderIDs(string _Code, int _ID)
            : base(_Code, _Code, _ID)
        {
            TypeList = TypeList ?? new List<LoaderIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static LoaderIDs GetByID(int _ID, LoaderIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static LoaderIDs GetByName(string _Name, LoaderIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static LoaderIDs GetByCode(string _Code, LoaderIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
