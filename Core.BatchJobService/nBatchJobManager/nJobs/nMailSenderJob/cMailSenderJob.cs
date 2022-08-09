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

namespace Core.BatchJobService.nBatchJobManager.nJobs.nMailSenderJob
{
    public class cMailSenderJob : cBaseJob<cMailSenderJobProps>
    {
        cBatchJobExecutionDataManager BatchJobExecutionDataManager { get; set; }
        public cMailSenderJob(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IManagers _Managers, IMicroService _MicroService, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService
            , cBatchJobDataManager _BatchJobDataManager
            , cBatchJobExecutionDataManager _BatchJobExecutionDataManager
            )
         : base(BatchJobIDs.MailSenderJob, _CoreServiceContext, _Managers, _MicroService, _DataServiceManager, _FileDataService, _BatchJobDataManager)
        {
            BatchJobExecutionDataManager = _BatchJobExecutionDataManager;
        }

        public override cBatchJobResult Run(cMailSenderJobProps _Props)
        {
            cBatchJobResult __Result = null;
            if (_Props.ReLoadConfig)
            {
                App.Handlers.EmailHandler.ReloadConfig();
                __Result = new cBatchJobResult("Mail Konfigurasyonu Yeniden Yüklendi.");
                return __Result;
            }
            if (_Props.Content == null || _Props.MessageTo == null || _Props.Subject == null)
            {
                __Result = new cBatchJobResult("Parametreler Boş");
                return __Result;
            }
            if (_Props.MessageTo.Contains("@seller.com") || _Props.MessageTo.Contains("@customer.com") || _Props.MessageTo.Contains("@admin.com") || _Props.MessageTo.Contains("@developer.com"))
            {
                __Result = new cBatchJobResult("Mail Başarıyla Gönderildi.");
                return __Result;
            }
            bool __MessageSentStatus = App.Handlers.EmailHandler.SendMail(_Props.MessageTo, _Props.Subject, _Props.Content, _Props.AttachmentFileList);
            __Result = new cBatchJobResult(__MessageSentStatus ? "Mail Başarıyla Gönderildi." : $"Mail Gönderilemedi.");
            return __Result;
        }
    }
}
