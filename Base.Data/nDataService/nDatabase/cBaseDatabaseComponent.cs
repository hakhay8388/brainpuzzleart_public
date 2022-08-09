using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase
{
    public class cBaseDatabaseComponent
    {
        public IDatabase Database { get; set; }
        public cBaseDatabaseComponent(IDatabase _Database)
        {
            Database = _Database;
        }
    }
}
