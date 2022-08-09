﻿using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nApply.nCrossApply
{
    public class cCross<TEntity> : cBaseApplyType<TEntity>
        where TEntity : cBaseEntity
    {
        public cCross(cQuery<TEntity> _Query)
            : base(_Query)
        {
            Query = _Query;
        }

        public override cSql GetSql(string _DataSource)
        {
            return Query.Database.Catalogs.RowOperationSQLCatalog.SQLCrossApply(_DataSource);
        }
    }
}
