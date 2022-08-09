using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cUserSessionTempEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length : 255, _UniqueKey: true)]
        public virtual string SessionHash { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _DefaultValue: "")]
        public virtual string IpAddress { get; set; }
    }
}
