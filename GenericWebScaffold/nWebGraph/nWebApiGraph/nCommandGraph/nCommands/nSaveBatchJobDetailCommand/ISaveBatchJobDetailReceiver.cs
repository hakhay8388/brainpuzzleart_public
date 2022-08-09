using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveBatchJobDetailCommand
{
    public interface ISaveBatchJobDetailReceiver : ICommandReceiver
    {
        void ReceiveSaveBatchJobDetailData(cListenerEvent _ListenerEvent, IController _Controller, cSaveBatchJobDetailCommandData _ReceivedData);
    }
}
