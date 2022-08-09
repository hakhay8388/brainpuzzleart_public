using Base.Boundary.nValueTypes.nConstType;
using Core.BatchJobService.nBatchJobManager.nJobs;
using Core.BatchJobService.nBatchJobManager.nJobs.nMssqlBackupJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nOldBatchJobExcutionsDeleteJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nOldNotificationDeleteJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nSessionTempClearJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nSitemapGenerateJob;
using Core.BatchJobService.nBatchJobManager.nJobs.nTestJob;
using System.Collections.Generic;
using System.Data;

namespace Core.BatchJobService.nDefaultValueTypes
{
    public class DefaultBatchJobExecutionIDs : cBaseConstType<DefaultBatchJobExecutionIDs>
    {
        public static List<DefaultBatchJobExecutionIDs> TypeList { get; set; }

		public static DefaultBatchJobExecutionIDs Test_DefaultExecution = new DefaultBatchJobExecutionIDs(BatchJobIDs.TestService, 5000, new cTestServiceJobProps() { TestValue = "aa" });
		public static DefaultBatchJobExecutionIDs OldBatchJobExcutionDelete_DefaultExecution = new DefaultBatchJobExecutionIDs(BatchJobIDs.OldBatchJobExcutionsDelete, 1, new cOldBatchJobExecutionsDeleteJobProps() { KeepLastDayCount = 30 });
        public static DefaultBatchJobExecutionIDs OldNotificationDeleteJob_DefaultExecution = new DefaultBatchJobExecutionIDs(BatchJobIDs.OldNotificationDeleteJob, 2, new cOldNotificationDeleteJobProps() { });
        public static DefaultBatchJobExecutionIDs SessionTempClearJob_DefaultExecution = new DefaultBatchJobExecutionIDs(BatchJobIDs.SessionTempClearJob, 2002, new cSessionTempClearJobProps() { });
        public static DefaultBatchJobExecutionIDs MssqlBackupJob_DefaultExecution = new DefaultBatchJobExecutionIDs(BatchJobIDs.MssqlBackupJob, 2003, new cMssqlBackupJobProps() { });
        public static DefaultBatchJobExecutionIDs SitemapGenerateJob_DefaultExecution = new DefaultBatchJobExecutionIDs(BatchJobIDs.SitemapGenerateJob, 2006, new cSitemapGenerateJobProps() { });
        



		public BatchJobIDs BatchJobID { get; set; }

        public cBaseJobProps Props { get; set; }

        public DefaultBatchJobExecutionIDs(BatchJobIDs _BatchJobID, int _ID, cBaseJobProps _Props)
            : base("", "", _ID)
        {
            BatchJobID = _BatchJobID;
            Props = _Props;

            TypeList = TypeList ?? new List<DefaultBatchJobExecutionIDs>();
            TypeList.Add(this);
        }
        public virtual string SerializeObject()
        {
            return Props.SerializeObject();
        }

        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static DefaultBatchJobExecutionIDs GetByID(int _ID, DefaultBatchJobExecutionIDs _DefaultID)
        {
            return GetByID(TypeList, _ID, _DefaultID);
        }
        public static DefaultBatchJobExecutionIDs GetByName(string _Name, DefaultBatchJobExecutionIDs _DefaultID)
        {
            return GetByName(TypeList, _Name, _DefaultID);
        }

        public static DefaultBatchJobExecutionIDs GetByCode(string _Code, DefaultBatchJobExecutionIDs _DefaultID)
        {
            return GetByCode(TypeList, _Code, _DefaultID);
        }
    }
}