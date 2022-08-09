using Base.Data.nDataService.nDatabase.nCatalog.nDatabaseOperationCatalog.nEntityForCore.nEntities;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataService.nDatabase.nMetadata.nTable;
using Base.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nMetadata.nTable.nDefaultContraint
{
    public class cDefaultContraint
    {
        public cTable Table { get; set; }
        public cDefaultConstraintCoreEnitity DefaultConstraintCoreEnitity { get; set; }

        public cDefaultContraint(cTable _Table, cDefaultConstraintCoreEnitity _DefaultConstraintCoreEnitity)
        {
            Table = _Table;
            DefaultConstraintCoreEnitity = _DefaultConstraintCoreEnitity;
        }

        public void Drop()
        {
            cSql __Sql = Table.TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLDropConstraint(Table.TableEnitity.TableName, DefaultConstraintCoreEnitity.ConstraintName);
            Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
            Table.DefaultContraintList.Remove(this);
        }
    }
}
