using Base.Data.nDataServiceManager;
using Base.FileData;
using Core.BatchJobService.nBatchJobManager.nJobs.nTestJob;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.BatchJobService.nDefaultValueTypes;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using Integration.MicroServiceGraph.nMicroService.nMicroServiceGraph.nMicroServiceActionGraph.nMicroServiceActions.nMicroServiceNotificationAction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nBatchJobManager.nJobs.nTestJob
{
    public class cTestServiceJob : cBaseJob<cTestServiceJobProps>
    {
        cUserDataManager UserDataManager { get; set; }
        public cTestServiceJob(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IManagers _Managers, IMicroService _MicroService, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService , cBatchJobDataManager _BatchJobDataManager, cUserDataManager _UserDataManager)
         : base(BatchJobIDs.TestService, _CoreServiceContext, _Managers, _MicroService, _DataServiceManager, _FileDataService, _BatchJobDataManager)
        {
            UserDataManager = _UserDataManager;
        }

        public override cBatchJobResult Run(cTestServiceJobProps _Props)
        {
			cMicroServiceNotificationProps __MicroServiceNotificationProps = new cMicroServiceNotificationProps(new List<long>() { 11,12,13,14,15 }, 1, ENotificationChannel.GlobalChannel, ENotificationType.TestNotification, new { Test = "Test" });
			MicroService.MicroServiceActionGraph.NotificationAction.BroadcastAction(__MicroServiceNotificationProps);
				
			
			cUserEntity __UserEntity = UserDataManager.GetUserByEmail("customer@customer.com");
            cBatchJobResult __Result = new cBatchJobResult("Başarılı : " + _Props.TestValue + " , " + __UserEntity.Name + " " + __UserEntity.UserDetail.ProfileImage);
            return __Result;
        }
    }
}
