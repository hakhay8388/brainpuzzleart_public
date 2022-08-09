using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Boundary.nMemoryData
{
    public class EMemoryDataType : cBaseConstType<EMemoryDataType>
    {
        public static EMemoryDataType Redis = new EMemoryDataType(GetVariableName(() => Redis), 1);

        static List<EMemoryDataType> TypeList { get; set; }
        public EMemoryDataType(string _Name, int _Value)
            : base(_Name, _Name, _Value)
        {
            TypeList = TypeList ?? new List<EMemoryDataType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EMemoryDataType GetByID(int _ID, EMemoryDataType _DBVendor)
        {
            return GetByID(TypeList, _ID, _DBVendor);
        }
        public static EMemoryDataType GetByName(string _Name, EMemoryDataType _DBVendor)
        {
            return GetByName(TypeList, _Name, _DBVendor);
        }
    }
}
