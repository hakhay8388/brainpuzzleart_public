using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cNotificationActorDetailEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: false)]
        public virtual bool Readed { get; set; }

        public virtual cMappedEntity<cNotificationDetailToActorMapEntity, cActorEntity> Actor { get; set; }

    }
}
