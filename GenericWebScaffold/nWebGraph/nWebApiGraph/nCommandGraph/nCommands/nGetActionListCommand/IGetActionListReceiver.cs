using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetActionListCommand
{
    public interface IGetActionListReceiver : ICommandReceiver
    {
        void ReceiveGetActionListData(cListenerEvent _ListenerEvent, IController _Controller, cGetActionListCommandData _ReceivedData);
    }
}
