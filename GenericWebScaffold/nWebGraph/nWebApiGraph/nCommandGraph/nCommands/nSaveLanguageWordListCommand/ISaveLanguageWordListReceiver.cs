using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveLanguageWordListCommand
{
    public interface ISaveLanguageWordListReceiver : ICommandReceiver
    {
        void ReceiveSaveLanguageWordListData(cListenerEvent _ListenerEvent, IController _Controller, cSaveLanguageWordListCommandData _ReceivedData);
    }
}
