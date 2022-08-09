using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDeleteFileCommand
{
    public interface IDeleteFileReceiver : ICommandReceiver
    {
        void ReceiveDeleteFileData(cListenerEvent _ListenerEvent, IController _Controller, cDeleteFileCommandData _ReceivedData);
    }
}
