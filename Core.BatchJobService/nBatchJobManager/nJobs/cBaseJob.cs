using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Core.BatchJobService.nDataService.nDataManagers;
using Core.BatchJobService.nDefaultValueTypes;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Integration.Managers.nManagers;
using Integration.MicroServiceGraph.nMicroService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Core.BatchJobService.nBatchJobManager.nJobs
{
    public abstract class cBaseJob<TJobProps> : cCoreService<cGenericWebScaffoldDataServiceContext>, IBatchJob
        where TJobProps : cBaseJobProps
    {
        public  BatchJobIDs BatchJobID { get; set; }
        protected IDataServiceManager DataServiceManager { get; set; }
        public IFileDateService FileDataService { get; set; }
        public cBatchJobDataManager BatchJobDataManager { get; set; }
        public cBatchJobManager BatchJobManager { get; set; }

		public IMicroService MicroService { get; set; }

		public IManagers Managers { get; set; }
		

		public cBaseJob(BatchJobIDs _BatchJobID, cGenericWebScaffoldDataServiceContext _CoreServiceContext, IManagers _Managers, IMicroService _MicroService,  IDataServiceManager _DataServiceManager, IFileDateService _FileDataService, cBatchJobDataManager _BatchJobDataManager)
          : base(_CoreServiceContext)
        {
            DataServiceManager = _DataServiceManager;
            FileDataService = _FileDataService;
            BatchJobID = _BatchJobID;
            BatchJobDataManager = _BatchJobDataManager;
			MicroService = _MicroService;
			Managers = _Managers;
		}

        public virtual void AddQueue(TJobProps _Props)
        {
            IDataService __DateService = DataServiceManager.GetDataService();

            cBatchJobEntity __BatchJobEntity = BatchJobDataManager.GetBatchJobByCode(BatchJobID.Code);


            cBatchJobExecutionEntity __BatchJobExecutionEntity = __DateService.Database.CreateNew<cBatchJobExecutionEntity>();

            __BatchJobExecutionEntity.State = EBatchJobExecutionState.NotRunning.ID;
            __BatchJobExecutionEntity.ParameterObjects = _Props.SerializeObject();

            __BatchJobExecutionEntity.Save(__BatchJobEntity);
        }
				
		public void Execute(cBatchJobEntity _BatchJobEntity, cBatchJobExecutionEntity _Entity)
        {
            TJobProps __JobProps = cBaseJobProps.GetPropFromString<TJobProps>(_Entity.ParameterObjects);
            IDataService __DateService = DataServiceManager.GetDataService();

            Stopwatch __StopWatch = new Stopwatch();
            __StopWatch.Start();

            try
            {
                __DateService.Perform(() =>
                {
                    _Entity.ExecutionTime = DateTime.Now;
                    _Entity.State = EBatchJobExecutionState.Running.ID;
                    _Entity.Save();
                });

                cBatchJobResult __Result = Run(__JobProps);

                __StopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan __TimeSpan = __StopWatch.Elapsed;

                __DateService.Perform(() =>
                {
                    _Entity.ExecutionTime = DateTime.Now;
                    _Entity.ElapsedTimeMilisecond = Convert.ToInt32(__TimeSpan.TotalMilliseconds);
                    _Entity.State = EBatchJobExecutionState.Success.ID;
                    _Entity.Result = __Result.Result;
                    _Entity.Save();
                });
            }
            catch(Exception _Ex)
            {
				App.Loggers.BatchJobLogger.LogError(_Ex);
				__StopWatch.Stop();
                // Get the elapsed time as a TimeSpan value.
                TimeSpan __TimeSpan = __StopWatch.Elapsed;

                cBatchJobExecutionEntity __RetryBatchJobExecutionEntity = null;
                __DateService.Perform(() =>
                {
                    string __Ex = _Ex.Message + _Ex.StackTrace;
                    _Entity.ExecutionTime = DateTime.Now;
                    _Entity.State = EBatchJobExecutionState.Error.ID;
                    _Entity.ElapsedTimeMilisecond = Convert.ToInt32(__TimeSpan.TotalMilliseconds);
                    _Entity.Exception = __Ex;
                    _Entity.Save();

                    if (_BatchJobEntity.MaxRetryCount > _Entity.CurrentTryCount)
                    {
                        __RetryBatchJobExecutionEntity = __DateService.Database.CreateNew<cBatchJobExecutionEntity>();
                        __RetryBatchJobExecutionEntity.CurrentTryCount = _Entity.CurrentTryCount + 1;
                        __RetryBatchJobExecutionEntity.ParameterObjects = _Entity.ParameterObjects;
                        __RetryBatchJobExecutionEntity.State = EBatchJobExecutionState.NotRunning.ID;
                        __RetryBatchJobExecutionEntity.Save(_BatchJobEntity);                        
                    }
                });

                if (__RetryBatchJobExecutionEntity != null)
                {
                    Execute(_BatchJobEntity, __RetryBatchJobExecutionEntity);
                }
            }
        }

        public abstract cBatchJobResult Run(TJobProps _Props);

    }
}
