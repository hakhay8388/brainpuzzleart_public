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

namespace Core.BatchJobService.nDataService.nDataManagers.nLoaders
{
    public class cBatchJobDataLoader : cBaseDataLoader
    {
        public cBatchJobDataManager BatchJobDataManager { get; set; }


        public cBatchJobDataLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
         )
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
            BatchJobDataManager = _BatchJobDataManager;
        }

        public void Init(IDataService _DataService)
        {

            ////////////// Global //////////////////

            for (int i = 0; i < BatchJobIDs.TypeList.Count; i++)
            {
                BatchJobDataManager.CreateBatchJobIfNotExists(BatchJobIDs.TypeList[i]);
            }

            List<cBatchJobEntity> __BatchJobList = BatchJobDataManager.GetBatchJobList();
            for (int i = 0; i < __BatchJobList.Count;i++)
            {
                if (BatchJobIDs.GetByCode(__BatchJobList[i].Code, null) == null)
                {
                    __BatchJobList[i].State = EBatchJobState.Stopped.ID;
                    __BatchJobList[i].Save();
                }
            }
        }
    }
}
