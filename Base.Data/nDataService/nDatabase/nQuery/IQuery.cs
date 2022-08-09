using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery
{
    public interface IQuery : IBaseQuery
    {

        List<IQueryElement> GroupBys { get; }
        string DefaultExternalAlias { get; set; }
        public List<TProjection> ToList<TProjection>(Action<TProjection> _Action = null) where TProjection : cBaseEntity;
        DataTable ToDataTable();
        int ExecuteForDeleteAndUpdate();
        List<dynamic> ToDynamicObjectList(Action<dynamic> _Action = null);
    }
}
