using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cActorType_SellerDetailEntity : cBaseGenericWebScaffoldEntity
    {
        

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int ProfileInfoStepComplete { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string IntroductoryText { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Text,  _DefaultValue: "")]
        public virtual string CurriculumVitaeText { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1024, _DefaultValue: "")]
        public virtual string VideoLink { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1024, _DefaultValue: "")]
        public virtual string AboutMe { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1024, _DefaultValue: "")]
        public virtual string MeAsATeacher { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1024, _DefaultValue: "")]
        public virtual string MyLessonsAndTeachingStyle { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1024, _DefaultValue: "")]
        public virtual string MyTeachingMaterial { get; set; } 


        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 50, _DefaultValue: "")]
        public virtual string IbanNo { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "DefaultProfile.png")]
        public virtual string ProfileImage { get; set; }

        public virtual cMappedEntity<cSellerToFileMapEntity, cFileEntity> Files { get; set; }

      
        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool IsSearchShowEnabled { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool IsFullNameSearchEnabled { get; set; }

    }
}
