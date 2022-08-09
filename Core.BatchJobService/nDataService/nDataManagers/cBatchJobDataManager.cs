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
using Core.BatchJobService.nDefaultValueTypes;

namespace Core.BatchJobService.nDataService.nDataManagers
{
    public class cBatchJobDataManager : cBaseDataManager
    {
        public cBatchJobDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

        public cBatchJobEntity GetBatchJobByCode(string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cBatchJobEntity __BatchJobEntity = __DataService.Database.Query<cBatchJobEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Code).Eq(_Code)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __BatchJobEntity;
        }
        public dynamic GetBatchJobByID(long _ID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cBatchJobEntity __BatchJobEntity = __DataService.Database.Query<cBatchJobEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.ID).Eq(_ID)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __BatchJobEntity;
        }

        public List<cBatchJobEntity> GetBatchJobList()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            List<cBatchJobEntity> __BatchJobEntityList = __DataService.Database.Query<cBatchJobEntity>()
                .SelectAll()
                .Where()
                .ToQuery()
                .ToList();
            return __BatchJobEntityList;
        }

        public cBatchJobEntity AddBatchJob(string _Code, string _Name, int _TimePeriodMilisecond, EBatchJobState _State, bool _AutoExecution, bool _ExecuteFirstWithoutWait, bool _StopAfterFirstExecution, int _MaxRetryCount)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cBatchJobEntity __BatchJobEntity = __DataService.Database.CreateNew<cBatchJobEntity>();
            __BatchJobEntity.Code = _Code;
            __BatchJobEntity.Name = _Name;
            __BatchJobEntity.State = _State.ID;
            __BatchJobEntity.AutoAddExecution = _AutoExecution;
            __BatchJobEntity.ExecuteFirstWithoutWait = _ExecuteFirstWithoutWait;
            __BatchJobEntity.StopAfterFirstExecution = _StopAfterFirstExecution;
            __BatchJobEntity.TimePeriodMilisecond = _TimePeriodMilisecond;
            __BatchJobEntity.MaxRetryCount = _MaxRetryCount;
            __BatchJobEntity.Save();

            return __BatchJobEntity;
        }
        public cBatchJobEntity UpdateBatchJob(cBatchJobEntity _BatchJobEntity, string _Code, string _Name, int _TimePeriodMilisecond, EBatchJobState _State, bool _AutoExecution, bool _ExecuteFirstWithoutWait, bool _StopAfterFirstExecution, int _MaxRetryCount)
        {

            _BatchJobEntity.Code = _Code;
            _BatchJobEntity.Name = _Name;
            _BatchJobEntity.State = _State.ID;
            _BatchJobEntity.AutoAddExecution = _AutoExecution;
            _BatchJobEntity.ExecuteFirstWithoutWait = _ExecuteFirstWithoutWait;
            _BatchJobEntity.StopAfterFirstExecution = _StopAfterFirstExecution;
            _BatchJobEntity.TimePeriodMilisecond = _TimePeriodMilisecond;
            _BatchJobEntity.MaxRetryCount = _MaxRetryCount;
            _BatchJobEntity.Save();

            return _BatchJobEntity;
        }
        public cBatchJobEntity UpdateBatchJob(long _ID, int _TimePeriodMilisecond,bool _AutoExecution, bool _ExecuteFirstWithoutWait, bool _StopAfterFirstExecution, int _MaxRetryCount)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cBatchJobEntity _BatchJobEntity = __DataService.Database.GetEntityByID<cBatchJobEntity>(_ID);

             
            _BatchJobEntity.AutoAddExecution = _AutoExecution;
            _BatchJobEntity.ExecuteFirstWithoutWait = _ExecuteFirstWithoutWait;
            _BatchJobEntity.StopAfterFirstExecution = _StopAfterFirstExecution;
            _BatchJobEntity.TimePeriodMilisecond = _TimePeriodMilisecond;
            _BatchJobEntity.MaxRetryCount = _MaxRetryCount;
            _BatchJobEntity.Save();

            return _BatchJobEntity;
        }

        public cBatchJobEntity CreateBatchJobIfNotExists(string _Code, string _Name, int _TimePeriodMilisecond, EBatchJobState _State, bool _AutoExecution, bool _ExecuteFirstWithoutWait, bool _StopAfterFirstExecution, int _MaxRetryCount)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cBatchJobEntity __BatchJobEntity = GetBatchJobByCode(_Code);
            if (__BatchJobEntity == null)
            {
                __BatchJobEntity = AddBatchJob(_Code, _Name, _TimePeriodMilisecond, _State, _AutoExecution, _ExecuteFirstWithoutWait, _StopAfterFirstExecution, _MaxRetryCount);
            }
            return __BatchJobEntity;
        }

        public cBatchJobEntity CreateBatchJobIfNotExists(BatchJobIDs _BatchJobID)
        {
            return CreateBatchJobIfNotExists(_BatchJobID.Code, _BatchJobID.Name, _BatchJobID.TimePeriodMilisecond, _BatchJobID.State, _BatchJobID.AutoExecution, _BatchJobID.ExecuteFirstWithoutWait, _BatchJobID.StopAfterFirstExecution, _BatchJobID.MaxRetryCount);
        }
    }
}
