using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nLogoutCommand
{
    public interface ILogoutReceiver : ICommandReceiver
    {
        void ReceiveLogoutData(cListenerEvent _ListenerEvent, IController _Controller, cLogoutCommandData _ReceivedData);
    }
}
