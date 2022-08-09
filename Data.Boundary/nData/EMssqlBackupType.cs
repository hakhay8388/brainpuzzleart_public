using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Boundary.nData
{
    public enum EMssqlBackupTypeEnums
    {
        Other = 1,
        Full = 2,
        Diff = 3,
    }

    public class EMssqlBackupType : cBaseConstType<EMssqlBackupType>
    {
        public static EMssqlBackupType Other = new EMssqlBackupType(GetVariableName(() => Other), (int)EMssqlBackupTypeEnums.Other, "Other");
        public static EMssqlBackupType Full = new EMssqlBackupType(GetVariableName(() => Full), (int)EMssqlBackupTypeEnums.Full, "Full");

       

        public static EMssqlBackupType Diff = new EMssqlBackupType(GetVariableName(() => Diff), (int)EMssqlBackupTypeEnums.Diff, "Diff");
        public static List<EMssqlBackupType> TypeList { get; set; }

        public EMssqlBackupType(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EMssqlBackupType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EMssqlBackupType GetByID(int _ID, EMssqlBackupType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EMssqlBackupType GetByCode(string _Code, EMssqlBackupType _DefaultData)
        {
            return GetByCode(TypeList, _Code, _DefaultData);
        }
        public static EMssqlBackupType GetByName(string _Name, EMssqlBackupType _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
