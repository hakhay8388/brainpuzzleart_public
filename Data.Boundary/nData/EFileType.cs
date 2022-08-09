using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EFileTypeEnums
    {
        Global = 0,
        Proof = 1,
        IdImage = 2,
        Student = 3,
        Ticket = 4,
        EInvoice=5,
    }

    public class EFileType : cBaseConstType<EFileType>
    {
        public static EFileType Proof = new EFileType(GetVariableName(() => Proof), (int)EFileTypeEnums.Proof, "Proof");
        public static EFileType IdImage = new EFileType(GetVariableName(() => IdImage), (int)EFileTypeEnums.IdImage, "IdImage");
        public static EFileType Student = new EFileType(GetVariableName(() => Student), (int)EFileTypeEnums.Student, "Student");
        public static EFileType Ticket = new EFileType(GetVariableName(() => Ticket), (int)EFileTypeEnums.Ticket, "Ticket");
        public static EFileType EInvoice = new EFileType(GetVariableName(() => EInvoice), (int)EFileTypeEnums.EInvoice, "EInvoice");
        

        public static List<EFileType> TypeList { get; set; }

        public EFileType(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EFileType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EFileType GetByID(int _ID, EFileType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EFileType GetByCode(string _Code, EFileType _DefaultData)
        {
            return GetByCode(TypeList, _Code, _DefaultData);
        }
        public static EFileType GetByName(string _Name, EFileType _DefaultData)
        {
            return GetByName(TypeList, _Name, _DefaultData);
        }
    }
}
