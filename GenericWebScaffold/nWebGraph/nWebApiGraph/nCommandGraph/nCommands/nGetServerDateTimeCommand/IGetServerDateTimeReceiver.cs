using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetServerDateTimeCommand
{
    public interface IGetServerDateTimeReceiver : ICommandReceiver
    {
        void ReceiveGetServerDateTimeData(cListenerEvent _ListenerEvent, IController _Controller, cGetServerDateTimeCommandData _ReceivedData);
    }
}
