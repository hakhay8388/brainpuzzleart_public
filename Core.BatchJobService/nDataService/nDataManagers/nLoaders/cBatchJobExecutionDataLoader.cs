using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.Boundary.nData;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService;
using Core.BatchJobService.nDefaultValueTypes;
using Core.BatchJobService.nBatchJobManager.nJobs;

namespace Core.BatchJobService.nDataService.nDataManagers.nLoaders
{
    public class cBatchJobExecutionDataLoader : cBaseDataLoader
    {
        public cBatchJobExecutionDataManager BatchJobExecutionDataManager { get; set; }

        public cBatchJobDataManager BatchJobDataManager { get; set; }


        public cBatchJobExecutionDataLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cBatchJobExecutionDataManager _BatchJobExecutionDataManager
         )
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
            BatchJobDataManager = _BatchJobDataManager;
            BatchJobExecutionDataManager = _BatchJobExecutionDataManager;
        }

        public void Init(IDataService _DataService)
        {

            ////////////// Global //////////////////


            for (int i = 0; i < DefaultBatchJobExecutionIDs.TypeList.Count; i++)
            {
                DefaultBatchJobExecutionIDs __Excution = DefaultBatchJobExecutionIDs.TypeList[i];
                cBatchJobEntity __BatchJobEntity = BatchJobDataManager.GetBatchJobByCode(__Excution.BatchJobID.Code);
                if (__BatchJobEntity != null && __BatchJobEntity.JobExecutions.Count < 1)
                {
                    
                    BatchJobExecutionDataManager.AddBatchJob(__BatchJobEntity, __Excution.Props.SerializeObject(), EBatchJobExecutionState.NotRunning, "", "", DateTime.Now, 0);
                }
            }
        }
    }
}
