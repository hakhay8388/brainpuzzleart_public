using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cTwitterUserFriendEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: false)]
        public virtual bool FollowingMe { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime FollowingMeDate { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: false)]
        public virtual bool IsUnFollowed { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime UnFollowedDate { get; set; }

        public virtual cMappedEntity<cTwitterUserFriendToFriendMapEntity, cTwitterFriendEntity> Friend { get; set; }

    }
}
