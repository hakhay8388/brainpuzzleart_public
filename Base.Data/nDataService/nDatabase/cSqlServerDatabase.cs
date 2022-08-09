using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Data.nDataService.nDatabase;
using Base.Data.nDataService.nDatabase.nCatalog;
using Base.Data.nDataService.nDatabase.nConnection;
using Base.Data.nDataService.nDatabase.nEntity;

namespace Base.Data.nDataService.nDatabase
{
    public class cSqlServerDatabase<TBaseEntity> : cBaseDatabase<TBaseEntity> where TBaseEntity : cBaseEntity
    {
        public cSqlServerDatabase(cDatabaseContext _DatabaseContext, bool _IsGlobalConnection)
            : base(_DatabaseContext, _IsGlobalConnection)
        {
        }

        protected override Type GetConnectionType()
        {
            return typeof(cSqlServerConnection);
        }

        protected override cBaseCatalogs GetCatalogs()
        {
            return new cSqlServerCatalogs(this);
        }
    }
}
