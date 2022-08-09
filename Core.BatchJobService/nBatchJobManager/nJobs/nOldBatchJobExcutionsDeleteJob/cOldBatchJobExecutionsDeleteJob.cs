using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Core.BatchJobService.nBatchJobManager.nJobs.nTestJob;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.BatchJobService.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nBatchJobManager.nJobs.nOldBatchJobExcutionsDeleteJob
{
    public class cOldBatchJobExecutionsDeleteJob : cBaseJob<cOldBatchJobExecutionsDeleteJobProps>
    {
        cBatchJobExecutionDataManager BatchJobExecutionDataManager { get; set; }
        public cOldBatchJobExecutionsDeleteJob(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IManagers _Managers, IMicroService _MicroService, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cBatchJobExecutionDataManager _BatchJobExecutionDataManager)
         : base(BatchJobIDs.OldBatchJobExcutionsDelete, _CoreServiceContext, _Managers, _MicroService, _DataServiceManager, _FileDataService, _BatchJobDataManager)
        {
            BatchJobExecutionDataManager = _BatchJobExecutionDataManager;
        }

        public override cBatchJobResult Run(cOldBatchJobExecutionsDeleteJobProps _Props)
        {
            IDataService __DateService = DataServiceManager.GetDataService();

            int __DeleteCount = 0;
            __DateService.Perform(() =>
            {
                __DeleteCount = BatchJobExecutionDataManager.DeleteExecutionBeforeDate(DateTime.Now.AddDays(-_Props.KeepLastDayCount));                
            });
            cBatchJobResult __Result = new cBatchJobResult("Son " + _Props.KeepLastDayCount + " gün öncesindeki BatchJob verileri silindi. Silinen Kayıt Sayısı : " + __DeleteCount);

            return __Result;
        }
    }
}
