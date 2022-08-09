using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceIDs
{
    public class MicroServiceIDs : cBaseConstType<MicroServiceIDs>
    {

        public static List<MicroServiceIDs> TypeList { get; set; }

        public static MicroServiceIDs Notification = new MicroServiceIDs(GetVariableName(() => Notification), 1, "", true);
		
		
		public static MicroServiceIDs TestMessage = new MicroServiceIDs(GetVariableName(() => TestMessage), 99999, "", true);
		public bool Enabled { get; set; }
        public string Info { get; set; }


        public MicroServiceIDs(string _Name, int _ID, string _Info, bool _Enabled)
            : base(_Name, _Name, _ID)
        {
            TypeList = TypeList ?? new List<MicroServiceIDs>();
            Enabled = _Enabled;
            Info = _Info;
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static MicroServiceIDs GetByID(int _ID, MicroServiceIDs _DefaultCommandID)
        {
            return GetByID(TypeList, _ID, _DefaultCommandID);
        }
        public static MicroServiceIDs GetByName(string _Name, MicroServiceIDs _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
