using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EUserConfirmationStateEnums
    {
        ConfirmationConfirmed = 1,
        ConfirmationMailAlreadyExists = 2,
        ConfirmationTokenExpire = 3,
        ConfirmationTokenNotFound = 4,
        ConfirmationNotConfirmed = 5,
    }

    public class EUserConfirmationState : cBaseConstType<EUserConfirmationState>
    {
        public static EUserConfirmationState ConfirmationConfirmed = new EUserConfirmationState(GetVariableName(() => ConfirmationConfirmed), (int)EUserConfirmationStateEnums.ConfirmationConfirmed, "ConfirmationConfirmed");
        public static EUserConfirmationState ConfirmationMailAlreadyExists = new EUserConfirmationState(GetVariableName(() => ConfirmationMailAlreadyExists), (int)EUserConfirmationStateEnums.ConfirmationMailAlreadyExists, "ConfirmationMailAlreadyExists");
        public static EUserConfirmationState ConfirmationTokenExpire = new EUserConfirmationState(GetVariableName(() => ConfirmationTokenExpire), (int)EUserConfirmationStateEnums.ConfirmationTokenExpire, "ConfirmationTokenExpire");
        public static EUserConfirmationState ConfirmationTokenNotFound = new EUserConfirmationState(GetVariableName(() => ConfirmationTokenNotFound), (int)EUserConfirmationStateEnums.ConfirmationTokenNotFound, "ConfirmationTokenNotFound");
        public static EUserConfirmationState ConfirmationNotConfirmed = new EUserConfirmationState(GetVariableName(() => ConfirmationNotConfirmed), (int)EUserConfirmationStateEnums.ConfirmationNotConfirmed, "ConfirmationNotConfirmed");

        public static List<EUserConfirmationState> TypeList { get; set; }

        public EUserConfirmationState(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EUserConfirmationState>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EUserConfirmationState GetByID(int _ID, EUserConfirmationState _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EUserConfirmationState GetByName(string _Name, EUserConfirmationState _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
