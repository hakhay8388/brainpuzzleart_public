using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nGlobalDataServices.nEntityServices.nEntities
{
    public class cProfileEntity : cBaseGlobalEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string HostName { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual cDBSettingEntity DBSetting { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "Now")]
        public virtual DateTime EndDate { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public string Email { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public string Surname { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public string Telephone { get; set; }


    }
}
