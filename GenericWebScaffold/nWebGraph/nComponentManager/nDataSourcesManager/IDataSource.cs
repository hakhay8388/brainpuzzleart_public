using Base.Core.nApplication;
using Base.Data.nDataService.nDatabase.nEntity;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetSettingsCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager
{
    public interface IDataSource : IDataSource_ReadReceiver
        , IDataSource_GetSettingsReceiver
        , IDataSource_CreateReceiver
        , IDataSource_UpdateReceiver
        , IDataSource_DeleteReceiver
        , IDataSource_GetMetaDataReceiver
    {
        cApp App { get; set; }
        DataSourceIDs DataSourceID { get; set; }
        void Init();
        void SynchronizeColumnNames();
    

    }
}
