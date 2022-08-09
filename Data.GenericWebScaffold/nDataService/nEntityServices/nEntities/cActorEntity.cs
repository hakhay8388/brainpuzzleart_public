using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cActorEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Name { get; set; }
        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)EUserVisibleGroupEnums.TestUser)]
        public virtual int UserVisibleGroup { get; set; }

        public virtual cMappedEntity<cActorRoleMapEntity, cRoleEntity> Roles { get; set; }

        public virtual cMappedEntity<cUserActorMapEntity, cUserEntity> User { get; set; }

        public virtual cMappedEntity<cNotificationDetailToActorMapEntity, cNotificationActorDetailEntity> Notifications { get; set; }

        public virtual cActorType_SellerDetailEntity SellerDetail { get; set; }

        //public virtual cActorType_SellerDetailNotConfirmedEntity SellerDetailNotConfirmed { get; set; }        

        public virtual cActorType_DeveloperDetailEntity DeveloperDetail { get; set; }


    }
}
