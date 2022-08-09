using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EUserVisibleGroupEnums
    {
        AllUser = 0,
        RealUser = 1,
        TestUser = 2,
    }
    public class EUserVisibleGroups : cBaseConstType<EUserVisibleGroups>
    {

        public static List<EUserVisibleGroups> TypeList { get; set; }

        public static EUserVisibleGroups AllUser = new EUserVisibleGroups(GetVariableName(() => AllUser), (int)EUserVisibleGroupEnums.AllUser, "AllUser");
        public static EUserVisibleGroups RealUser = new EUserVisibleGroups(GetVariableName(() => RealUser), (int)EUserVisibleGroupEnums.RealUser, "RealUser");
        public static EUserVisibleGroups TestUser = new EUserVisibleGroups(GetVariableName(() => TestUser), (int)EUserVisibleGroupEnums.TestUser, "TestUser");


        public string ColumnName { get; set; }


        public EUserVisibleGroups(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EUserVisibleGroups>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EUserVisibleGroups GetByID(int _ID, EUserVisibleGroups _DefaultCommandID)
        {
            return GetByID(TypeList, _ID, _DefaultCommandID);
        }
        public static EUserVisibleGroups GetByName(string _Name, EUserVisibleGroups _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }

        public static EUserVisibleGroups GetByCode(string _Code, EUserVisibleGroups _DefaultCommandID)
        {
            return GetByCode(TypeList, _Code, _DefaultCommandID);
        }
    }
}
