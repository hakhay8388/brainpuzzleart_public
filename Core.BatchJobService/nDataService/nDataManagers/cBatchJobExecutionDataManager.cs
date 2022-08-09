using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService;

namespace Core.BatchJobService.nDataService.nDataManagers
{
    public class cBatchJobExecutionDataManager : cBaseDataManager
    {
        public cBatchJobExecutionDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

		public List<cBatchJobExecutionEntity> GetUnexecuted(cBatchJobEntity _BatchJobEntity)
		{
			IDataService __DataService = DataServiceManager.GetDataService();

			List<cBatchJobExecutionEntity> __BatchJobExecutionEntityList = __DataService.Database.Query<cBatchJobExecutionEntity>()
				.SelectAll()
				.Where()
				.Operand<cBatchJobEntity>().Eq(_BatchJobEntity.ID)
				.And
				.Operand(__Item => __Item.State).Eq(EBatchJobExecutionState.NotRunning.ID)
				.ToQuery()
				.ToList();
			return __BatchJobExecutionEntityList;
		}

		public cBatchJobExecutionEntity GetLastExecution(cBatchJobEntity _BatchJobEntity)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cQuery<cBatchJobExecutionEntity> __Query = __DataService.Database.Query<cBatchJobExecutionEntity>()
                  .SelectAll()
                  .Where()
                  .Operand<cBatchJobEntity>().Eq(_BatchJobEntity.ID)
                  .ToQuery()
                  .OrderBy().Desc(__Item => __Item.ExecutionTime)
                  .ToQuery();



            cBatchJobExecutionEntity __BatchJobExecutionEntity = __Query.ToList().FirstOrDefault();
            return __BatchJobExecutionEntity;
        }

        public cBatchJobExecutionEntity AddBatchJob(cBatchJobEntity _OwnerBatchJobEntity, string _ParameterObjects, EBatchJobExecutionState _State, string _Exception, string _Result, DateTime _ExecutionTime, int _ElapsedTimeMilisecond)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cBatchJobExecutionEntity __BatchJobEntity = __DataService.Database.CreateNew<cBatchJobExecutionEntity>();
            __BatchJobEntity.ParameterObjects = _ParameterObjects;
            __BatchJobEntity.State = _State.ID;
            __BatchJobEntity.Exception = _Exception;
            __BatchJobEntity.Result = _Result;
            __BatchJobEntity.ExecutionTime = _ExecutionTime;
            __BatchJobEntity.ElapsedTimeMilisecond = _ElapsedTimeMilisecond;
            __BatchJobEntity.Save(_OwnerBatchJobEntity);

            return __BatchJobEntity;
        }

        public int DeleteExecutionBeforeDate(DateTime _Date)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            int __Count = __DataService.Database.Delete<cBatchJobExecutionEntity>()
                .Operand(__Item => __Item.ExecutionTime).Lt(_Date).ToQuery()
                .ExecuteForDeleteAndUpdate();
            return __Count;
            //cSql __Sql = __DataService.Database.DefaultConnection.CreateSql("DELETE FROM BatchJobExecutions Where ExecutionTime < :ExecutionTime");
            //__Sql.SetParameter("ExecutionTime", _Date);
            //__DataService.Database.DefaultConnection.Execute(__Sql);
        }

    }
}
