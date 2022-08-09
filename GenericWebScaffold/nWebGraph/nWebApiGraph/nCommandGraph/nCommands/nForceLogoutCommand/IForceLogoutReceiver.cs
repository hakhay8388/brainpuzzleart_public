using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nForceLogoutCommand
{
    public interface IForceLogoutReceiver : ICommandReceiver
    {
        void ReceiveForceLogoutData(cListenerEvent _ListenerEvent, IController _Controller, cForceLogoutCommandData _ReceivedData);
    }
}
