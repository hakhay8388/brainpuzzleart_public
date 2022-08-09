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
    public class cRoleEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _UniqueKey: true)]
        public virtual string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255, _UniqueKey: true)]
        public virtual string Code { get; set; }

        public virtual cMappedEntity<cActorRoleMapEntity, cActorEntity> Actor { get; set; }

        public virtual cMappedEntity<cRolePageMapEntity, cPageEntity> Pages { get; set; }

        public virtual cMappedEntity<cRoleMenuMapEntity, cMenuEntity> Menus { get; set; }


		public virtual cMappedEntity<cRoleDataSourcePermissionMapEntity, cDataSourcePermissionEntity> DataSource { get; set; }

        public virtual cMappedEntity<cRoleDataSourceColumnMapEntity, cDataSourceColumnEntity> DataSourceColumns { get; set; }
        public virtual cRoleMenuEntity RoleMenuEntity { get; set; }
    }
}
