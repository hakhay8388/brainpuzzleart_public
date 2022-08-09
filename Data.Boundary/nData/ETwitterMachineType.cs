using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum ETwitterMachineConceptTypeEnums
    {
        NFT = 1,
    }

    public class ETwitterMachineConceptType : cBaseConstType<ETwitterMachineConceptType>
    {
        public static ETwitterMachineConceptType NFT = new ETwitterMachineConceptType(GetVariableName(() => NFT), (int)ETwitterMachineConceptTypeEnums.NFT, "NFT");

        public static List<ETwitterMachineConceptType> TypeList { get; set; }

        public ETwitterMachineConceptType(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<ETwitterMachineConceptType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static ETwitterMachineConceptType GetByID(int _ID, ETwitterMachineConceptType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static ETwitterMachineConceptType GetByName(string _Name, ETwitterMachineConceptType _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
