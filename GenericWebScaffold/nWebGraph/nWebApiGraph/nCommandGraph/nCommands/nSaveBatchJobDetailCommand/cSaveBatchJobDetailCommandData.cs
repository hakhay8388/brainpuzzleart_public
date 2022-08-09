using Core.GenericWebScaffold.nUtils.nValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveBatchJobDetailCommand
{
    public class cSaveBatchJobDetailCommandData
    {
        public long ID;
        public int TimePeriodMilisecond;
        public bool ExecuteFirstWithoutWait;
        public bool AutoAddExecution;
        public bool StopAfterFirstExecution;
        public int MaxRetryCount;
    }
}
