using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum ETwitterUserTypeEnums
    {
        MasterUser = 1,
        User = 2,
        QuoteRetweeterAndTagerUser = 3,
        Follow4FollowUser = 4,
        FriendTableFeeder = 5
    }

    public class ETwitterUserType : cBaseConstType<ETwitterUserType>
    {
        public static ETwitterUserType MasterUser = new ETwitterUserType(GetVariableName(() => MasterUser), (int)ETwitterUserTypeEnums.MasterUser, "MasterUser");
        public static ETwitterUserType User = new ETwitterUserType(GetVariableName(() => User), (int)ETwitterUserTypeEnums.User, "User");
        public static ETwitterUserType QuoteRetweeterAndTagerUser = new ETwitterUserType(GetVariableName(() => QuoteRetweeterAndTagerUser), (int)ETwitterUserTypeEnums.QuoteRetweeterAndTagerUser, "QuoteRetweeterAndTagerUser");
        public static ETwitterUserType Follow4FollowUser = new ETwitterUserType(GetVariableName(() => Follow4FollowUser), (int)ETwitterUserTypeEnums.Follow4FollowUser, "Follow4FollowUser");
        public static ETwitterUserType FriendTableFeeder = new ETwitterUserType(GetVariableName(() => FriendTableFeeder), (int)ETwitterUserTypeEnums.FriendTableFeeder, "FriendTableFeeder");


        public static List<ETwitterUserType> TypeList { get; set; }

        public ETwitterUserType(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ETwitterUserType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ETwitterUserType GetByID(int _ID, ETwitterUserType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static ETwitterUserType GetByName(string _Name, ETwitterUserType _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
