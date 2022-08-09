using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cTwitterQuoteStringEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string QuoteString { get; set; }



        public virtual cMappedEntity<cTwitterMachineToQuoteStringMapEntity, cTwitterMachineEntity> TwitterMachine { get; set; }
    }
}
