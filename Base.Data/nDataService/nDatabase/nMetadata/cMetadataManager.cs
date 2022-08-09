using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Data.nDataService.nDatabase.nMetadata.nTable;
using Base.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Data.nDataService.nDatabase;
using Base.Data.nDataService.nDatabase.nSql;

namespace Base.Data.nDataService.nDatabase.nMetadata
{
    public class cMetadataManager : cBaseDatabaseComponent
    {
        public cTableManager TableManager { get; set; }

        public cMetadataManager(IDatabase _Database)
            : base(_Database)
        {
            TableManager = new cTableManager(this);
            if (Database.DefaultConnection.IsOpen)
            {
                Reload();
            }
			else
			{
				TableManager.MetadataManager.Database.App.Loggers.SqlLogger.LogError(new Exception("Connection Pool Hatasi"));
			}
        }

        public void Reload()
        {
            TableManager.Create();
        }

        public void DropForeignKeys()
        {
            TableManager.DropForeignKeys();
        }

        public void DropIndexes()
        {
            TableManager.DropIndexes();
        }

        public void DropTables()
        {
            TableManager.DropTables();
        }
    }
}
