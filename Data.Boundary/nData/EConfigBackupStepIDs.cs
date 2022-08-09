using Base.Boundary.nValueTypes.nConstType; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public class EConfigBackupStepIDs :  cBaseConstType<EConfigBackupStepIDs>
    {

        public static List<EConfigBackupStepIDs> TypeList { get; set; }

        public static EConfigBackupStepIDs GlobalParams = new EConfigBackupStepIDs(GetVariableName(() => GlobalParams), GetVariableName(() => GlobalParams), 2);
        public static EConfigBackupStepIDs Template = new EConfigBackupStepIDs(GetVariableName(() => Template), GetVariableName(() => Template), 3);
        public static EConfigBackupStepIDs PaymentKey = new EConfigBackupStepIDs(GetVariableName(() => PaymentKey), GetVariableName(() => PaymentKey), 4);


        public EConfigBackupStepIDs(string _Code, string _Name, int _ID)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EConfigBackupStepIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EConfigBackupStepIDs GetByID(int _ID, EConfigBackupStepIDs _DefaultCommandID)
        {
            return GetByID(TypeList, _ID, _DefaultCommandID);
        }
        public static EConfigBackupStepIDs GetByName(string _Name, EConfigBackupStepIDs _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
