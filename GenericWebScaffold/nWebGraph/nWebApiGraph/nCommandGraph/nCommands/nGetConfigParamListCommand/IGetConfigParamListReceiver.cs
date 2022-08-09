using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetConfigParamListCommand
{
    public interface IGetConfigParamListReceiver : ICommandReceiver
    {
        void ReceiveGetConfigParamListData(cListenerEvent _ListenerEvent, IController _Controller, cGetConfigParamListCommandData _ReceivedData);
    }
}
