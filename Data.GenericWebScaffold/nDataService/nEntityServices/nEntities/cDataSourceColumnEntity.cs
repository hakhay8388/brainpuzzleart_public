using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cDataSourceColumnEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Varchar, _DefaultValue:"")]
        public virtual string ColumnName { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Varchar, _DefaultValue: "")]
        public virtual string DataSourceCode { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int)]
        public virtual int DataSourceID { get; set; }

        public virtual cMappedEntity<cRoleDataSourceColumnMapEntity, cRoleEntity> Roles { get; set; }


    }
}
