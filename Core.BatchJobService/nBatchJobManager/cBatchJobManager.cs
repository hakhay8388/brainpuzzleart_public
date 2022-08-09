using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Core.BatchJobService.nBatchJobManager.nJobs;
using Core.BatchJobService.nBatchJobManager.nJobs.ncSessionTempClearJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nMailSenderJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nMssqlBackupJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nOldBatchJobExcutionsDeleteJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nOldNotificationDeleteJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nSitemapGenerateJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nTestJob;
using Data.GenericWebScaffold.nDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.BatchJobService.nBatchJobManager
{
    [Register(typeof(cBatchJobManager), false, true, true, true, LifeTime.ContainerControlledLifetimeManager)]
    public class cBatchJobManager : cCoreService<cGenericWebScaffoldDataServiceContext>
    {
        public List<IBatchJob> JobList { get; set; }
        public cTestServiceJob TestServiceJob { get; set; }
        public cOldBatchJobExecutionsDeleteJob OldBatchJobExcutionsDeleteJob { get; set; }
        public cOldNotificationDeleteJob OldNotificationDeleteJob { get; set; }
        public cMailSenderJob MailSenderJob { get; set; }
        public cSitemapGenerateJob SitemapGenerateJob { get; set; }
        public cMssqlBackupJob MssqlBackupJob { get; set; }

        public cSessionTempClearJob SessionTempClearJob { get; set; }

        public cBatchJobManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext
                , cTestServiceJob _TestServiceJob
                , cOldBatchJobExecutionsDeleteJob _OldBatchJobExcutionsDeleteJob
                , cOldNotificationDeleteJob _OldNotificationDeleteJob
                , cMailSenderJob _MailSenderJob
                , cSitemapGenerateJob _SitemapGenerateJob
                , cSessionTempClearJob _SessionTempClearJob
                , cMssqlBackupJob _MssqlBackupJob

            )
              : base(_CoreServiceContext)
        {
            JobList = new List<IBatchJob>();
        }

        public override void Init()
        {


            Type __ThisType = this.GetType();
            List<Type> __Templates = App.Handlers.AssemblyHandler.GetTypesFromBaseInterface<IBatchJob>();
            __Templates.ForEach(__Type =>
            {
                IBatchJob __Step = (IBatchJob)App.Factories.ObjectFactory.ResolveInstance(__Type);

                PropertyInfo __PropertyInfo = __ThisType.GetAllProperties().Where(__Item => __Item.Name.StartsWith(__Step.BatchJobID.Name)).FirstOrDefault();
                if (__PropertyInfo == null)
                {
                    throw new Exception($"{__Step.BatchJobID.Name} BatchJob ismi BatchJobIDs ile eşleşmiyor.");
                }
                __PropertyInfo.GetSetMethod().Invoke(this, new object[] { __Step });

                __Step.BatchJobManager = this;
                JobList.Add(__Step);
            });

        }

        public IBatchJob GetBatchJobByCode(string _Code)
        {
            return JobList.Find(__Item => __Item.BatchJobID.Code == _Code);
        }
    }
}
