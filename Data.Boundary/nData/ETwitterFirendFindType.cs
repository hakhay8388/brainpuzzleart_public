using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum ETwitterFirendFindTypeEnums
    {
        FoundInSearch = 1,
        FoundInOtherFriendsFollowers = 2,
    }

    public class ETwitterFirendFindType : cBaseConstType<ETwitterFirendFindType>
    {
        public static ETwitterFirendFindType FoundInSearch = new ETwitterFirendFindType(GetVariableName(() => FoundInSearch), (int)ETwitterFirendFindTypeEnums.FoundInSearch, "FoundInSearch");
        public static ETwitterFirendFindType FoundInOtherFriendsFollowers = new ETwitterFirendFindType(GetVariableName(() => FoundInOtherFriendsFollowers), (int)ETwitterFirendFindTypeEnums.FoundInOtherFriendsFollowers, "FoundInOtherFriendsFollowers");



        public static List<ETwitterFirendFindType> TypeList { get; set; }

        public ETwitterFirendFindType(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ETwitterFirendFindType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ETwitterFirendFindType GetByID(int _ID, ETwitterFirendFindType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static ETwitterFirendFindType GetByName(string _Name, ETwitterFirendFindType _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
