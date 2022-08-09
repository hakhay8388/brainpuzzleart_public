using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nRegisterCommand
{
    public interface IRegisterReceiver : ICommandReceiver
    {
        void ReceiveRegisterData(cListenerEvent _ListenerEvent, IController _Controller, cRegisterCommandData _ReceivedData);
    }
}
