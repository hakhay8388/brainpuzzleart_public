using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nMessageResultCommand
{
    public interface IMessageResultReceiver : ICommandReceiver
    {
        void ReceiveMessageResultData(cListenerEvent _ListenerEvent, IController _Controller, cMessageResultCommandData _ReceivedData);
    }
}
