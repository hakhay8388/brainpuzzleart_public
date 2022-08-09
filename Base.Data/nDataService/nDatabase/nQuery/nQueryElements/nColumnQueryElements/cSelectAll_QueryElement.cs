using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nColumnQueryElements
{
    public class cSelectAll_QueryElement<TEntity> : cBaseQueryElement, ISelectableColumns where TEntity : cBaseEntity
    {
        public cSelectAll_QueryElement(IBaseQuery _Query)
            : base(_Query)
        {
        }

        public List<string> GetColumnNameList()
        {
            cEntityTable __Table = Query.Database.EntityManager.GetEntityTableByEnitityType<TEntity>();
            List<string> __Result = new List<string>();
            for (int i = 0; i < __Table.EntityFieldList.Count;i++)
            {
                __Result.Add(__Table.EntityFieldList[i].ColumnName);
            }
            return __Result;
        }

        public override string ToElementString(params object[] _Params)
        {
            return "*";
        }
    }
}
