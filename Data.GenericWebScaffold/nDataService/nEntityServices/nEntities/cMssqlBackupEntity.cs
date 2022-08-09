using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cMssqlBackupEntity : cBaseGenericWebScaffoldEntity
    {

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)EMssqlBackupTypeEnums.Other)]
        public virtual int BackupType { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 1024, _DefaultValue: "")]
        public virtual string FilePath { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: 0)]
        public virtual long FileSize { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: 0)]
        public virtual long ZipFileSize { get; set; }
    }
}
