using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nAttributes;
using Data.Boundary.nData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.GenericWebScaffold.nDataService.nEntityServices.nEntities
{
    public class cBatchJobExecutionEntity : cBaseGenericWebScaffoldEntity
    {
 
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: -1, _DefaultValue: "")] 
        public virtual string ParameterObjects { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)EBatchJobExecutionStateEnums.NotRunning)]
        public virtual int State { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: -1, _DefaultValue: "")]
        public virtual string Exception { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: -1, _DefaultValue: "")]
        public virtual string Result { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 1)]
        public virtual int CurrentTryCount { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Datetime, _DefaultValue: "now")]
        public virtual DateTime ExecutionTime { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 0)]
        public virtual int ElapsedTimeMilisecond { get; set; }

    }
}
