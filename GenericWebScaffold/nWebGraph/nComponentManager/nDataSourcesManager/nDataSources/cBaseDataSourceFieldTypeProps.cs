using Base.Data.nDataService.nDatabase.nEntity;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources
{
    public class cBaseDataSourceFieldTypeProps
    {

        public string ColumnAs { get; set; }
        public bool InnerJoin { get; set; }
        public string Title { get; set; }
        public bool Visible { get; set; }
        public bool ElasticSearch { get; set; }
        public ColumnEditableIDs Editable { get; set; }
        public bool Removable { get; set; }
        public bool Readonly { get; set; }
        public bool TranslateValue { get; set; }
        public ILookupDataSource LookUpDataSource { get; set; } 
        public int OrderFromLeft { get; set; }

        public cBaseDataSourceFieldTypeProps()
        {
            Title = "Atanmadı";
            Visible = true;
            ElasticSearch = false;
            Editable = ColumnEditableIDs.Never;
            Removable = false;
            Readonly = false;
            LookUpDataSource = null;
            InnerJoin = false;
            TranslateValue = false;
            OrderFromLeft = 1;
            ColumnAs = ""; 
        }

    }
}
