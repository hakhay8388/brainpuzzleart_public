using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum ETwitterUserStateEnums
    {
        Available = 1,
        Dead = 2
    }

    public class ETwitterUserState : cBaseConstType<ETwitterUserState>
    {
        public static ETwitterUserState Available = new ETwitterUserState(GetVariableName(() => Available), (int)ETwitterUserStateEnums.Available, "Available");
        public static ETwitterUserState Dead = new ETwitterUserState(GetVariableName(() => Dead), (int)ETwitterUserStateEnums.Dead, "Dead");

        public static List<ETwitterUserState> TypeList { get; set; }

        public ETwitterUserState(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ETwitterUserState>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ETwitterUserState GetByID(int _ID, ETwitterUserState _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static ETwitterUserState GetByName(string _Name, ETwitterUserState _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
