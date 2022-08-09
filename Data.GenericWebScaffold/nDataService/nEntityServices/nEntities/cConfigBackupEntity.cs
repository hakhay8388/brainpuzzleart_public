using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cConfigBackupEntity : cBaseGenericWebScaffoldEntity
    {

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string Code { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int SortValue { get; set; }  
    }
}
