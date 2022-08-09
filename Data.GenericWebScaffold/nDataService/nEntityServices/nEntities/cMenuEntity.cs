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
    public class cMenuEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _UniqueKey: true)]
        public virtual string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _UniqueKey: true)]
        public virtual string Code { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Icon { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int SortValue { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string MenuTypeCode { get; set; }

        public virtual cMenuEntity RootMenu { get; set; }

        public virtual cMappedEntity<cRoleMenuMapEntity, cRoleEntity> Roles { get; set; }

        public virtual cMappedEntity<cMenuPageMapEntity, cPageEntity> Page { get; set; }
        public virtual cRoleMenuEntity RoleMenuEntity { get; set; }

    }
}
