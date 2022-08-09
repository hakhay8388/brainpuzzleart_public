using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cTwitterSearchStringEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string SearchString { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: false)]
        public virtual bool UseForReply { get; set; }

        public virtual cMappedEntity<cTwitterMachineToSearchStringMapEntity, cTwitterMachineEntity> TwitterMachine { get; set; }
    }
}
