using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nBatchJobStartCommand
{
    public interface IBatchJobStartReceiver : ICommandReceiver
    {
        void ReceiveBatchJobStartData(cListenerEvent _ListenerEvent, IController _Controller, cBatchJobStartCommandData _ReceivedData);
    }
}
