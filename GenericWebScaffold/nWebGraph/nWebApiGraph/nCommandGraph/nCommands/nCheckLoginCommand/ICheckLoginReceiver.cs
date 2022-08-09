using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nCheckLoginCommand
{
    public interface ICheckLoginReceiver : ICommandReceiver
    {
        void ReceiveCheckLoginData(cListenerEvent _ListenerEvent, IController _Controller, cCheckLoginCommandData _ReceivedData);
    }
}
