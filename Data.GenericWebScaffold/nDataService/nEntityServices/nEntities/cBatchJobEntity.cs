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
    public class cBatchJobEntity : cBaseGenericWebScaffoldEntity
    {
        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length: 255)]
        public virtual string Code { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Nvarchar, _Length : 255)]
        public virtual string Name { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bigint, _DefaultValue: 10000)]
        public virtual int TimePeriodMilisecond { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: (int)EBatchJobStateEnums.Stopped)]
        public virtual int State { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool ExecuteFirstWithoutWait { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: false)]
        public virtual bool AutoAddExecution { get; set; }
        

        [DBField(_Nullable: false, _DataType: EDataType.Bit, _DefaultValue: true)]
        public virtual bool StopAfterFirstExecution { get; set; }

        [DBField(_Nullable: false, _DataType: EDataType.Int, _DefaultValue: 1)]
        public virtual int MaxRetryCount { get; set; }


        public virtual cEntityList<cBatchJobExecutionEntity> JobExecutions { get; set; }

    }
}
