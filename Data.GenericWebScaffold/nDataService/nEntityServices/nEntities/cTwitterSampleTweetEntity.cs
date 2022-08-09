using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cTwitterSampleTweetEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 180, _DefaultValue: "")]
        public virtual string Tweet { get; set; }

        public virtual cMappedEntity<cTwitterMachineToSampleTweetMapEntity, cTwitterMachineEntity> TwitterMachines { get; set; }
    }
}
