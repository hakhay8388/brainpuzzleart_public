using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetMenuListCommand
{
    public interface IGetMenuListReceiver : ICommandReceiver
    {
        void ReceiveGetMenuListData(cListenerEvent _ListenerEvent, IController _Controller, cGetMenuListCommandData _ReceivedData);
    }
}
