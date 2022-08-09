using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nBatchJobManager.nJobs.nMailSenderJob
{
    public class cMailSenderJobProps : cBaseJobProps
    {
        public virtual string Content { get; set; }
        public virtual string MessageTo { get; set; }
        public virtual string Subject { get; set; } 
        public virtual bool ReLoadConfig { get; set; }
        public virtual List<string> AttachmentFileList { get; set; }
    }
}
