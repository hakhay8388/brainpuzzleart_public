using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Data.Boundary.nData
{
    public enum EMessageTypeEnums
    {
        Text = 0,
        File = 1,
        Image = 2
    }

    public class EMessageType : cBaseConstType<EMessageType>
    {
        public static EMessageType Text = new EMessageType(GetVariableName(() => Text), (int)EMessageTypeEnums.Text, "Text");
        public static EMessageType File = new EMessageType(GetVariableName(() => File), (int)EMessageTypeEnums.File, "File");
        public static EMessageType Image = new EMessageType(GetVariableName(() => Image), (int)EMessageTypeEnums.Image, "Image"); 

        public static List<EMessageType> TypeList { get; set; }

        public EMessageType(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EMessageType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EMessageType GetByID(int _ID, EMessageType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EMessageType GetByCode(string _Code, EMessageType _DefaultData)
        {
            return GetByCode(TypeList, _Code, _DefaultData);
        }
        public static EMessageType GetByName(string _Name, EMessageType _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
