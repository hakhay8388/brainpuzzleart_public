using Base.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nAttributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities
{
    public class cTableCoreEnitity : cBaseCoreEntity<cTableCoreEnitity>
    {
        [CoreDBField("")]
        public string TableName { get; set; }
    }
}
