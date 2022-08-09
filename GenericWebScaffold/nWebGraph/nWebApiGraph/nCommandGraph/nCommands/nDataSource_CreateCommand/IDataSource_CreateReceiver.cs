using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand
{
    public interface IDataSource_CreateReceiver : ICommandReceiver
    {
        void ReceiveDataSource_CreateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_CreateCommandData _ReceivedData);
    }
}
