using Base.Data.nDataService.nDatabase;
using Base.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nCatalog
{
    public class cBaseCatalogOperations : cBaseDatabaseComponent
    {
        public cBaseCatalogOperations(IDatabase _Database)
            :base(_Database)
        {
        }

        protected cSql CreateSql(string _Sql)
        {
            return Database.ConnectionPoolingManager.DefaultConnection.CreateSql(_Sql);
        }
    }
}
