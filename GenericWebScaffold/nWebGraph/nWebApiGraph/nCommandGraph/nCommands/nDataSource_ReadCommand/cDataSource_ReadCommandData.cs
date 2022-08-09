using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand
{
    public class cDataSource_ReadCommandData
    {
        public String ClientComponentName;
        public int PageSize;
        public int Page;
        public string OrderByField;
        public string OrderDirection;
        public cFilters Filters;
        public string Search;
        public object Options;
    }
}
