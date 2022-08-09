using Base.Data.nDataService.nDatabase.nEntity;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources
{
    public class cBaseMetaObject
    {
        public string Title { get; set; }
        public string FieldName { get; set; }
        public string Type { get; set; }
        public bool Visible { get; set; }
        public string Editable { get; set; }
        public bool Removable { get; set; }
        public bool TranslateValue { get; set; }
        public bool Readonly { get; set; }
        public object LookUpDataSource { get; set; } 
    }
}
