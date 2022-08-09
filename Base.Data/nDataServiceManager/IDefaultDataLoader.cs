using Base.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Data.nDataServiceManager
{
    public interface IDefaultDataLoader
    {
        void Load(IDataService _DataService);
    }
}
