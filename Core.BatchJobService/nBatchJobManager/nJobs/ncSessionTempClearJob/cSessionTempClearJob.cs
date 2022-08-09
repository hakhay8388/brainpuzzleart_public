using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Core.BatchJobService.nBatchJobManager.nJobs.nSessionTempClearJob;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.BatchJobService.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using System;

namespace Core.BatchJobService.nBatchJobManager.nJobs.ncSessionTempClearJob
{
    public class cSessionTempClearJob : cBaseJob<cSessionTempClearJobProps>
    {
        public cSessionDataManager SessionDataManager { get; }

        public cSessionTempClearJob(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IManagers _Managers, IMicroService _MicroService, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cSessionDataManager _SessionDataManager
            )
         : base(BatchJobIDs.SessionTempClearJob, _CoreServiceContext, _Managers, _MicroService, _DataServiceManager, _FileDataService, _BatchJobDataManager)
        {
            SessionDataManager = _SessionDataManager;
            SessionDataManager = _SessionDataManager;
        }

        public override cBatchJobResult Run(cSessionTempClearJobProps _JobProps)
        {
            cBatchJobResult __BatchJobResult = new cBatchJobResult("");
            IDataService __DataService = DataServiceManager.GetDataService();
            cGenericWebScaffoldDataService __GlobalParams = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();
            int __DeleteCount = 0;
            __DataService.Perform(() =>
            {
                __DeleteCount = SessionDataManager.DeleteOldSessionTempDate(DateTime.Now.AddDays(-1));
            });
            cBatchJobResult __Result = new cBatchJobResult("1 Gün Önceki Geçici Oturumlar Silindi. Silinen Kayıt Sayısı : " + __DeleteCount);


            return __BatchJobResult;
        }
      
    }
}
