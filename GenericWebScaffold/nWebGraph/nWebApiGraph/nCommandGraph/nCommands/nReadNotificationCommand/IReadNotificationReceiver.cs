using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nReadNotificationCommand
{
    public interface IReadNotificationReceiver : ICommandReceiver
    {
        void ReceiveReadNotificationData(cListenerEvent _ListenerEvent, IController _Controller, cReadNotificationCommandData _ReceivedData);
    }
}
