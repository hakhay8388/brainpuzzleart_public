using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cNotificationEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1024, _DefaultValue: "")]
        public virtual string ParameterObjects { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ENotificationTypeEnums.None)]
        public virtual int Type { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)ENotificationChannelEnums.GlobalChannel)]
        public virtual int ChannelID { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: false)]
        public virtual bool NotificationBroadcasted { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "Now")]
        public virtual DateTime ValidUntilDate { get; set; }

        

        //public virtual cActorEntity OwnerActor { get; set; }
        public virtual cEntityList<cNotificationActorDetailEntity> ActorDetails { get; set; }

    }
}
