using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nActions.nHotSpotMessageAction;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetSettingsCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Core.GenericWebScaffold.nWebGraph.nComponentManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using Core.GenericWebScaffold.Controllers;
using Integration.MicroServiceGraph;
using Integration.MicroServiceGraph.nMicroService;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nListenerGraph.nDataSourceListener
{
    public class cDataSourceListener : cBaseListener
        , IDataSource_ReadReceiver
        , IDataSource_GetSettingsReceiver
        , IDataSource_CreateReceiver
        , IDataSource_UpdateReceiver
        , IDataSource_DeleteReceiver
        , IDataSource_GetMetaDataReceiver
    {
        public cSessionDataManager SessionDataManager { get; set; }
        public cUserDataManager UserDataManager { get; set; }
        public cDataSourceListener(cApp _App, IMicroService _MicroService, cWebGraph _WebGraph, IDataServiceManager  _DataServiceManager)
           : base(_App, _MicroService, _WebGraph, _DataServiceManager)
        {
            WebGraph = _WebGraph;
        }

        public void ReceiveDataSource_ReadData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_ReadCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_ReadData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_GetSettingsData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetSettingsCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_GetSettingsData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_CreateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_CreateCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_CreateData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_UpdateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_UpdateCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_UpdateData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_DeleteData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_DeleteCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_DeleteData(_ListenerEvent, _Controller, _ReceivedData);
        }

        public void ReceiveDataSource_GetMetaDataData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaDataCommandData _ReceivedData)
        {
            WebGraph.ComponentManager.DataSourceManager.GetDataSourceByDataSourceClientComponentName(_ReceivedData.ClientComponentName).ReceiveDataSource_GetMetaDataData(_ListenerEvent, _Controller, _ReceivedData);
        }

    }
}
