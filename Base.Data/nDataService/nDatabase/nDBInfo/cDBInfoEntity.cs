using Base.Data.nDataService.nDatabase.nEntity;
using Base.Boundary.nData;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nDBInfo
{
    public class cDBInfoEntity : cBaseEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int MainVersion { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int DBVersion { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int ExtensitionVersion { get; set; }
    }
}
