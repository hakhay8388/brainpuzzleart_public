using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum ETwitterMachineStateEnums
    {
        Started = 1,
        Stopped = 2,
    }

    public class ETwitterMachineState : cBaseConstType<ETwitterMachineState>
    {
        public static ETwitterMachineState Started = new ETwitterMachineState(GetVariableName(() => Started), (int)ETwitterMachineStateEnums.Started, "Started");
        public static ETwitterMachineState Stopped = new ETwitterMachineState(GetVariableName(() => Stopped), (int)ETwitterMachineStateEnums.Stopped, "Stopped");

        public static List<ETwitterMachineState> TypeList { get; set; }

        public ETwitterMachineState(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ETwitterMachineState>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ETwitterMachineState GetByID(int _ID, ETwitterMachineState _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static ETwitterMachineState GetByName(string _Name, ETwitterMachineState _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
