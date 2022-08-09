using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders;
using Base.Data.nDataServiceManager;
using Base.Core.nAttributes;
using Base.Boundary.nCore.nObjectLifeTime;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Core.BatchJobService.nDataService.nDataManagers.nLoaders;
using Data.GenericWebScaffold.nDataService;

namespace Core.BatchJobService.nDataService.nDataManagers
{
    [Register(typeof(IBatchJobDataLoader), false, false, false, false, LifeTime.ContainerControlledLifetimeManager)]
    public class cDefaultDataLoaderManager : cBaseDataManager, IBatchJobDataLoader
    {
        public cBatchJobDataLoader BatchJobDataLoader { get; set; }
        public cBatchJobExecutionDataLoader BatchJobExecutionDataLoader { get; set; }

        public cDefaultDataLoaderManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataLoader _BatchJobDataLoader
            , cBatchJobExecutionDataLoader _BatchJobExecutionDataLoader
        )
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
            BatchJobDataLoader = _BatchJobDataLoader;
            BatchJobExecutionDataLoader = _BatchJobExecutionDataLoader;
        }

        public void Load(IDataService _DataService)
        {
            _DataService.Perform<IDataService>(LoadBatchJobData, _DataService);
            _DataService.Perform<IDataService>(LoadBatchJobExecutionData, _DataService);
        }

        public void LoadBatchJobData(IDataService _DataService)
        {
            ////////////////////////
            ///ÖNEMLİ
            ///Normalde pure data ve reletinal data olmasından dolayı önce pure dataları commitlemek sonra bağlantılı olanları eklemek gerekiyor.
            ///Fakat Aynı transaction üzerinden yönetildiğinde sorgular transaction üzerinde olanlarıda bulabiliyor.
            ///Onun için saf datalar ve bağlantılı datalar tek bir commitle gönderilebiliyor.
            BatchJobDataLoader.Init(_DataService);
        }

        public void LoadBatchJobExecutionData(IDataService _DataService)
        {
            ////////////////////////
            ///ÖNEMLİ
            ///Normalde pure data ve reletinal data olmasından dolayı önce pure dataları commitlemek sonra bağlantılı olanları eklemek gerekiyor.
            ///Fakat Aynı transaction üzerinden yönetildiğinde sorgular transaction üzerinde olanlarıda bulabiliyor.
            ///Onun için saf datalar ve bağlantılı datalar tek bir commitle gönderilebiliyor.
            BatchJobExecutionDataLoader.Init(_DataService);
        }
    }
}

