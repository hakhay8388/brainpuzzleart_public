using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Core.BatchJobService.nDefaultValueTypes
{
    public enum EnumVideoConfServiceTypes
    {
        NewUser = 0,
        NewCourse = 1,
    }

    public class EVideoConfServiceTypes : cBaseConstType<EVideoConfServiceTypes>
    {

        public static List<EVideoConfServiceTypes> TypeList { get; set; }

        public static EVideoConfServiceTypes NewUser = new EVideoConfServiceTypes("NewUser", EnumVideoConfServiceTypes.NewUser);

        public EVideoConfServiceTypes(string _Name, EnumVideoConfServiceTypes _VideoConfServiceTypes)
            : base(_Name, _Name, (int)_VideoConfServiceTypes)
        {
            TypeList = TypeList ?? new List<EVideoConfServiceTypes>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EVideoConfServiceTypes GetByID(EnumVideoConfServiceTypes _ID, EVideoConfServiceTypes _DefaultCommandID)
        {
            return GetByID(TypeList, (int)_ID, _DefaultCommandID);
        }
        public static EVideoConfServiceTypes GetByName(string _Name, EVideoConfServiceTypes _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
