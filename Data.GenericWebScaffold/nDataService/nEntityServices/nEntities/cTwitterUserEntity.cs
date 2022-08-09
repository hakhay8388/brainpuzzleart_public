using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cTwitterUserEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Email { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue: "")]
        public virtual string UserName { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue: "")]
        public virtual string Password { get; set; }

        public virtual cMappedEntity<cTwitterMachineToTwitterUserMapEntity, cTwitterMachineEntity> TwitterMachine { get; set; }

        public virtual cMappedEntity<cTwitterFriendFeedByTwitterUserMapEntity, cTwitterFriendEntity> FeededFriend { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ETwitterUserStateEnums.Available)]
        public virtual int State { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ETwitterUserTypeEnums.User)]
        public virtual int Type { get; set; }        

        [DBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime LastDeadTime { get; set; }

        public virtual cEntityList<cTwitterUserFriendEntity> UserFriends { get; set; }

        public virtual cEntityList<cTwitterUserTweetEntity> Tweets { get; set; }

        public virtual cEntityList<cTwitterUserRetweetEntity> Retweets { get; set; }

        public virtual cEntityList<cTwitterUserReplyTweetEntity> Replies { get; set; }

        public virtual cEntityList<cTwitterUserFollowedOurTwitterUserEntity> FollowedOurTwitterUser { get; set; }
        

    }
}
