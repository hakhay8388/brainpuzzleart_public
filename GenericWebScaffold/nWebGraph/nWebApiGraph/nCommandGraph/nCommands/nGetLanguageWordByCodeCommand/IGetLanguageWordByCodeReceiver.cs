using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetLanguageWordByCodeCommand
{
    public interface IGetLanguageWordByCodeReceiver : ICommandReceiver
    {
        void ReceiveGetLanguageWordByCodeData(cListenerEvent _ListenerEvent, IController _Controller, cGetLanguageWordByCodeCommandData _ReceivedData);
    }
}
