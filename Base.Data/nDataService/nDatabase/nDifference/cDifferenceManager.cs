using Base.Core.nCore;
using Base.Data.nDataService.nDatabase.nDifference.nDiff_Table;
using Base.Data.nDataService.nDatabase.nMetadata.nTable;
using Base.Data.nDataService.nDatabase;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Data.nDataService.nDatabase.nSql;

namespace Base.Data.nDataService.nDatabase.nDifference
{
    public class cDifferenceManager : cBaseDatabaseComponent
    {
        cDiff_TableManager Diff_TableManager { get; set; }
        public cDifferenceManager(IDatabase _Database)
            :base(_Database)
        {
            Diff_TableManager = new cDiff_TableManager(this);
            CalculateDifferences();
        }

        public void CalculateDifferences()
        {
            Diff_TableManager.Calculate();
        }

        public void Synchronize()
        {
            Diff_TableManager.Synchronize();
        }
    }
}
