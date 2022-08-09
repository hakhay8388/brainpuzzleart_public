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

namespace Core.BatchJobService.nBatchJobManager.nJobs.nOldNotificationDeleteJob
{
    public class cOldNotificationDeleteJob : cBaseJob<cOldNotificationDeleteJobProps>
    {
        cNotificationDataManager NotificationDataManager { get; set; }
        public cOldNotificationDeleteJob(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IManagers _Managers, IMicroService _MicroService, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cNotificationDataManager _NotificationDataManager)
         : base(BatchJobIDs.OldNotificationDeleteJob, _CoreServiceContext, _Managers, _MicroService, _DataServiceManager, _FileDataService, _BatchJobDataManager)
        {
            NotificationDataManager = _NotificationDataManager;
        }

        public override cBatchJobResult Run(cOldNotificationDeleteJobProps _Props)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            int __DeleteCount = 0;
            __DataService.Perform(() =>
            {
                __DeleteCount = NotificationDataManager.DeleteOldNotificationDate(DateTime.Now);
            });
            cBatchJobResult __Result = new cBatchJobResult("Eski kullanıcı bildirimleri silindi. Silinen Kayıt Sayısı : " + __DeleteCount);

            return __Result;
        }
    }
}
