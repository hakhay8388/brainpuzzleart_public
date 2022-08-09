using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetPageListCommand
{
    public interface IGetPageListReceiver : ICommandReceiver
    {
        void ReceiveGetPageListData(cListenerEvent _ListenerEvent, IController _Controller, cGetPageListCommandData _ReceivedData);
    }
}
