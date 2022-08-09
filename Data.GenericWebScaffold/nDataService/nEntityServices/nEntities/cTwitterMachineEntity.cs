using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cTwitterMachineEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Code { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue: "")]
        public virtual string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ETwitterMachineStateEnums.Stopped)]
        public virtual int State { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ETwitterMachineConceptTypeEnums.NFT)]
        public virtual int Type { get; set; }


        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool FeedFriendTable { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool IncreaseFollower { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool TweetTager { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool TweetReplier { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool DoQuoteRetweet { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool DoRandomRetweet { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool DoTweet { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool FollowOurUser { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool MasterRetweet { get; set; }


        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool CustomMessager { get; set; }



        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 30000)]
        public virtual int TickSleepTime { get; set; }

        public virtual cMappedEntity<cTwitterMachineToSearchStringMapEntity, cTwitterSearchStringEntity> SearchStrings { get; set; }

        public virtual cMappedEntity<cTwitterMachineToQuoteStringMapEntity, cTwitterQuoteStringEntity> QuoteStrings { get; set; }

        public virtual cMappedEntity<cTwitterMachineToTwitterUserMapEntity, cTwitterUserEntity> TwitterUsers{ get; set; }

        public virtual cMappedEntity<cTwitterMachineToSampleTweetMapEntity, cTwitterSampleTweetEntity> SampleTweets { get; set; }

        
    }
}
