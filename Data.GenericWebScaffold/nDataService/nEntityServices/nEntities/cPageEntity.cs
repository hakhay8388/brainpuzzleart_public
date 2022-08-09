using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cPageEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length : 255,  _UniqueKey: true)]
        public virtual string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _UniqueKey: true)]
        public virtual string Code { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _UniqueKey: true)]
        public virtual string Url { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _UniqueKey: true)]
        public virtual string ComponentName { get; set; }

        public virtual cMappedEntity<cRolePageMapEntity, cRoleEntity> Roles { get; set; }

        public virtual cMappedEntity<cMenuPageMapEntity, cPageEntity> Menu { get; set; }

    }
}
