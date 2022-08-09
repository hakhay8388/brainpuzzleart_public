using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDefaultValueTypes
{
    public class RoleIDs : cBaseConstType<RoleIDs>
    {

        public static List<RoleIDs> TypeList { get; set; }

        public static RoleIDs Admin = new RoleIDs(GetVariableName(() => Admin), "Admin", 1);
        public static RoleIDs Seller = new RoleIDs(GetVariableName(() => Seller), "Seller", 2);
        public static RoleIDs Customer = new RoleIDs(GetVariableName(() => Customer), "Customer", 3);
		public static RoleIDs Unlogined = new RoleIDs(GetVariableName(() => Unlogined), "Unlogined", 4);
        public static RoleIDs Developer = new RoleIDs(GetVariableName(() => Developer), "Developer", 5);


        public RoleIDs(string _Code, string _Name, int _ID)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<RoleIDs>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static RoleIDs GetByID(int _ID, RoleIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static RoleIDs GetByName(string _Name, RoleIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static RoleIDs GetByCode(string _Code, RoleIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}
