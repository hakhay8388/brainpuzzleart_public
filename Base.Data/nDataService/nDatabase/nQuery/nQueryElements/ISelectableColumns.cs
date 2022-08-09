using Base.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Data.nDataService.nDatabase.nQuery.nQueryElements
{
    public interface ISelectableColumns
    {
        List<string> GetColumnNameList();
    }
}
