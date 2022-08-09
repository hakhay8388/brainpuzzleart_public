using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using Data.Boundary.nData;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nEnumDataSources
{
    public class cUserStateDataSource : ILookupDataSource
    {
        public object ToLookUpObject(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaDataCommandData _ReceivedData)
        {
            JArray __Result = new JArray();
            EUserState.TypeList.ForEach(__Item =>
            {
                JObject __JObject = new JObject();
                __JObject[__Item.ID] = __Item.Name;
            });
            return __Result;
        }

    }
}
