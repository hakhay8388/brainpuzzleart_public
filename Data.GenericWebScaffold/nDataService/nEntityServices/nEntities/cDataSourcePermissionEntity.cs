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
    public class cDataSourcePermissionEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Bit)]
        public virtual bool CanRead { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit)]
        public virtual bool CanCreate { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit)]
        public virtual bool CanUpdate { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit)]
        public virtual bool CanDelete { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int)]
        public virtual int DataSourceID { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Varchar, _DefaultValue: "")]
        public virtual string DataSourceCode { get; set; }

        public virtual cMappedEntity<cRoleDataSourcePermissionMapEntity, cRoleEntity> Roles { get; set; }

        public DataSourceIDs DataSource
        {
            get
            {
                return DataSourceIDs.GetByID(DataSourceID, null);
            }
        }

    }
}
