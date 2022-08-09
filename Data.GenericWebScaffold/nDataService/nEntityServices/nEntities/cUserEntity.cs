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
    public class cUserEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Surname { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string RealSurname { get; set; }

        [DBField(_UniqueKey: true, _Clustered: false, _Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Email { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Password { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 2, _DefaultValue: "tr")]
        public virtual string Language { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)EUserStateEnums.Waiting)]
        public virtual int State { get; set; }
        
        [DBField(_Nullable: true, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Usernick { get; set; }


        public virtual cEntityList<cUserSessionEntity> Sessions { get; set; }
        public virtual cEntityList<cUserSessionTempEntity> SessionTemps { get; set; }

        public virtual cUserDetailEntity UserDetail { get; set; }

        public virtual cUserEntity RootUser { get; set; }


		public virtual cMappedEntity<cUserActorMapEntity, cActorEntity> Actor { get; set; }

    }
}
