using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cUserTempEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Surname { get; set; }
        
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string RealSurname { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Email { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Password { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 2, _DefaultValue: "tr")]
        public virtual string Language { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Telephone { get; set; }

        [DBField(_Nullable: true, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime DateOfBirth { get; set; }


        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: false)]
        public virtual bool IsSeller { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Token { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string GuidStr { get; set; }

        [DBField(_Nullable: true, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string OtherUniversity { get; set; }

        [DBField(_Nullable: true, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string OtherSection { get; set; }

        [DBField(_Nullable: true, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Usernick { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime LastSendMail { get; set; }
        [DBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime SendMailEndDate { get; set; }
    }
}
