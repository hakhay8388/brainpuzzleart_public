using Base.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Data.nDataServiceManager
{
    public interface IComponentLoader
    {
        void Load(IDataService _DataService);
    }
}
