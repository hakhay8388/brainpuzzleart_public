using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nChangeServiceStateCommand
{
    public interface IChangeServiceStateReceiver : ICommandReceiver
    {
        void ReceiveChangeServiceStateData(cListenerEvent _ListenerEvent, IController _Controller, cChangeServiceStateCommandData _ReceivedData);
    }
}
