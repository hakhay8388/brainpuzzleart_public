﻿using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSetLanguageCommand
{
    public interface ISetLanguageReceiver : ICommandReceiver
    {
        void ReceiveSetLanguageData(cListenerEvent _ListenerEvent, IController _Controller, cSetLanguageCommandData _ReceivedData);
    }
}
