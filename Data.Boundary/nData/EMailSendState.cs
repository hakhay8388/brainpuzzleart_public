using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EMailSendStateEnums
    {
        NotSended = 0,
        FailedSended = -1,
        FailedOutSended=-2,
        Sended = 1,
    }

    public class EMailSendState : cBaseConstType<EMailSendState>
    {
        public static EMailSendState NotSended = new EMailSendState(GetVariableName(() => NotSended), (int)EMailSendStateEnums.NotSended, "NotSended");
        public static EMailSendState FailedSended = new EMailSendState(GetVariableName(() => FailedSended), (int)EMailSendStateEnums.FailedSended, "FailedSended");
        public static EMailSendState FailedOutSended = new EMailSendState(GetVariableName(() => FailedOutSended), (int)EMailSendStateEnums.FailedOutSended, "FailedOutSended");
        public static EMailSendState Sended = new EMailSendState(GetVariableName(() => Sended), (int)EMailSendStateEnums.Sended, "Sended");


        public static List<EMailSendState> TypeList { get; set; }

        public EMailSendState(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EMailSendState>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EMailSendState GetByID(int _ID, EMailSendState _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EMailSendState GetByName(string _Name, EMailSendState _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
