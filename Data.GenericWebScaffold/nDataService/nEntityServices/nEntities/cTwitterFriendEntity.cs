using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cTwitterFriendEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue: "")]
        public virtual string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string UserName { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue: "")]
        public virtual string Description { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int FollowerCount { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int FollowingCount { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ETwitterUserStateEnums.Available)]
        public virtual int State { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ETwitterFirendFindTypeEnums.FoundInSearch)]
        public virtual int FindType { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ETwitterMachineConceptTypeEnums.NFT)]
        public virtual int ConceptType { get; set; }
        


        public virtual cMappedEntity<cTwitterFriendFeedByTwitterUserMapEntity, cTwitterUserEntity> FeededByUser { get; set; }

        public virtual cMappedEntity<cTwitterUserFriendToFriendMapEntity, cTwitterUserFriendEntity> UserFriends { get; set; }
        
    }
}
