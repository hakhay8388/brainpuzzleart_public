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

namespace Base.Data.nDataService.nDatabase.nMetadata.nTable.nForeignKey
{
    public class cForeignKey
    {
        public cTable Table { get; set; }
        public cForeignKeyCoreEnitity ForeignKeyEnitity { get; set; }

        public cForeignKey(cTable _Table, cForeignKeyCoreEnitity _ForeignKeyEnitity)
        {
            Table = _Table;
            ForeignKeyEnitity = _ForeignKeyEnitity;
        }

        public void Drop()
        {
            cSql __Sql = Table.TableManager.MetadataManager.Database.Catalogs.TableOperationSQLCatalog.SQLDropConstraint(Table.TableEnitity.TableName, ForeignKeyEnitity.ForeignKeyName);
            Table.TableManager.MetadataManager.Database.DefaultConnection.Execute(__Sql);
            Table.ParentedForeignKeyList.Remove(this);
            Table.TableManager.GetTableByName(ForeignKeyEnitity.ReferencedTableName).ReferencedForeignKeyList.Remove(this);
        }
    }
}
