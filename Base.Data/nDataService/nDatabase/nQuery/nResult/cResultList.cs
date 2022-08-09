using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Data.nDataService.nDatabase.nQuery.nResult
{
    public class cResultList<T>
    {
        public T ResultList { get; set; }
        public int TotalRecordCount { get; set; }
        public cResultList(T _ResultList, int _TotalRecordCount)
        {
            ResultList = _ResultList;
            TotalRecordCount = _TotalRecordCount;
        }
    }
}
