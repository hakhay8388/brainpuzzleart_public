using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetUserCommand
{
    public interface IGetUserReceiver : ICommandReceiver
    {
        void ReceiveGetUserData(cListenerEvent _ListenerEvent, IController _Controller, cGetUserCommandData _ReceivedData);
    }
}
