using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources
{
    public interface IDataSourceFieldType<TEntity> where TEntity : cBaseEntity
    {
        cDataSourceFieldTypeProps<TEntity> Props { get; set; }
        object ToMetaObject(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_GetMetaDataCommandData _ReceivedData);
        string GetColumnRoot();
        string ColumnName { get; }
        Type ColumnType { get; }
        string GetFullName();

        //void Update(object _Value);

        IDataSourceFieldType<TEntity> IDField { get; }

        void Update(long _ID, object _Value);
    }
}
