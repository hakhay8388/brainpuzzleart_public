using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EUserGenderEnums
    {
        NotSelected = 0,
        Male = 1,
        Female = 2,
    }

    public class EUserGender : cBaseConstType<EUserGender>
    {
        public static EUserGender NotSelected = new EUserGender(GetVariableName(() => NotSelected), (int)EUserGenderEnums.NotSelected, "NotSelected");
        public static EUserGender Male = new EUserGender(GetVariableName(() => Male), (int)EUserGenderEnums.Male, "Male");
        public static EUserGender Female = new EUserGender(GetVariableName(() => Female), (int)EUserGenderEnums.Female, "Female");
        
        public static List<EUserGender> TypeList { get; set; }

        public EUserGender(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EUserGender>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EUserGender GetByID(int _ID, EUserGender _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EUserGender GetByName(string _Name, EUserGender _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
