using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveConfigBackupCommand
{
    public interface ISaveConfigBackupReceiver : ICommandReceiver
    {
        void ReceiveSaveConfigBackupData(cListenerEvent _ListenerEvent, IController _Controller, cSaveConfigBackupCommandData _ReceivedData);
    }
}
