﻿using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nSaveConfigParamListCommand
{
    public interface ISaveConfigParamListReceiver : ICommandReceiver
    {
        void ReceiveSaveConfigParamListData(cListenerEvent _ListenerEvent, IController _Controller, cSaveConfigParamListCommandData _ReceivedData);
    }
}
