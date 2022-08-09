using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetSettingsCommand
{
    public interface IDataSource_GetSettingsReceiver : ICommandReceiver
    {
        void ReceiveDataSource_GetSettingsData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetSettingsCommandData _ReceivedData);
    }
}
