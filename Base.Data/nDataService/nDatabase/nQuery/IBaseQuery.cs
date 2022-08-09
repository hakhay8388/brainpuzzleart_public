using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators;
using Base.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery
{
    public interface IBaseQuery
    {
        EQueryType QueryType { get; set; }

        string DefaultAlias { get; }
        cSql ToSql();

        IDatabase Database { get; }
        cEntityTable EntityTable { get; }

        List<IQueryElement> DataSource { get; }
        List<IQueryElement> Columns { get; }
        List<IFilterElement> Filters { get; }
        List<cParameter> Parameters { get; set; }
    }
}
