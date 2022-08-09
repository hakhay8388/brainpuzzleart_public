using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nActionGraph.nAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ActionPropField : System.Attribute
    {
        public Object DefaultValue { get; set; }

        public ActionPropField(Object _DefaultValue = null)
        {
            DefaultValue = _DefaultValue;
        }

    }
}
