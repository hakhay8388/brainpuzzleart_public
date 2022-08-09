using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cTwitterTagedUserAndTweetEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue: "")]
        public virtual string MasterUserName { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string TweetID { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1000, _DefaultValue: "")]
        public virtual string UserName { get; set; }
    }
}
