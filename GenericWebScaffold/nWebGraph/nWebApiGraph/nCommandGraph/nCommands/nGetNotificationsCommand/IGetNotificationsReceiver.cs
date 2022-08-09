using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetNotificationsCommand
{
    public interface IGetNotificationsReceiver : ICommandReceiver
    {
        void ReceiveGetNotificationsData(cListenerEvent _ListenerEvent, IController _Controller, cGetNotificationsCommandData _ReceivedData);
    }
}
