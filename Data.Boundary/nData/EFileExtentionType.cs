using Base.Boundary.nValueTypes.nConstType;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Boundary.nData
{
    public enum EFileExtentionTypeEnums
    {
        NONE = 0,
        JPEG = 1,
        JPG = 2,
        PNG = 3,
        PDF = 4,
        DOC = 5,
        DOCX = 6
    }

    public class EFileExtentionType : cBaseConstType<EFileExtentionType>
    {
        public static EFileExtentionType NONE = new EFileExtentionType(GetVariableName(() => NONE), (int)EFileExtentionTypeEnums.NONE, "NONE");
        public static EFileExtentionType JPEG = new EFileExtentionType(GetVariableName(() => JPEG), (int)EFileExtentionTypeEnums.JPEG, "JPEG");
        public static EFileExtentionType JPG = new EFileExtentionType(GetVariableName(() => JPG), (int)EFileExtentionTypeEnums.JPG, "JPG");
        public static EFileExtentionType PNG = new EFileExtentionType(GetVariableName(() => PNG), (int)EFileExtentionTypeEnums.PNG, "PNG");
        public static EFileExtentionType PDF = new EFileExtentionType(GetVariableName(() => PDF), (int)EFileExtentionTypeEnums.PDF, "PDF");
        public static EFileExtentionType DOC = new EFileExtentionType(GetVariableName(() => DOC), (int)EFileExtentionTypeEnums.DOC, "DOC");
        public static EFileExtentionType DOCX = new EFileExtentionType(GetVariableName(() => DOCX), (int)EFileExtentionTypeEnums.DOCX, "DOCX");


        public static List<EFileExtentionType> TypeList { get; set; }

        public EFileExtentionType(string _Code, int _ID, string _Name)
            : base(_Name, _Code, _ID)
        {
            TypeList = TypeList ?? new List<EFileExtentionType>();
            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static EFileExtentionType GetByID(int _ID, EFileExtentionType _DefaultData)
        {
            return GetByID(TypeList, _ID, _DefaultData);
        }
        public static EFileExtentionType GetByCode(string _Code, EFileExtentionType _DefaultData)
        {
            return GetByCode(TypeList, _Code.ToUpper(), _DefaultData);
        }
        public static EFileExtentionType GetByName(string _Name, EFileExtentionType _DefaultData)
        {
            return GetByName(TypeList, _Name.ToUpper(), _DefaultData);
        }
    }
}
