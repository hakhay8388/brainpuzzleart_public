using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cFileEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue: "")]
        public virtual string OrginalFileName { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string FileName { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Extention { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)EFileExtentionTypeEnums.JPEG)]
        public virtual int ExtentionType { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)EFileTypeEnums.Global)]
        public virtual int FileType { get; set; }

        public virtual cMappedEntity<cSellerToFileMapEntity, cActorType_SellerDetailEntity> SellerDetails { get; set; }

    }
}
