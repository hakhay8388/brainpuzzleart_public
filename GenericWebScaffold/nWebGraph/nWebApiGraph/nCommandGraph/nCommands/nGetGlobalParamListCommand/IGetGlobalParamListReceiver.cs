﻿using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nGetGlobalParamListCommand
{
    public interface IGetGlobalParamListReceiver : ICommandReceiver
    {
        void ReceiveGetGlobalParamListData(cListenerEvent _ListenerEvent, IController _Controller, cGetGlobalParamListCommandData _ReceivedData);
    }
}
