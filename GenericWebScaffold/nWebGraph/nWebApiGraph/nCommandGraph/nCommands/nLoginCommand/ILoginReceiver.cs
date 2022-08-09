using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLoginCommand
{
    public interface ILoginReceiver : ICommandReceiver
    {
        void ReceiveLoginData(cListenerEvent _ListenerEvent, IController _Controller, cLoginCommandData _ReceivedData);
    }
}
