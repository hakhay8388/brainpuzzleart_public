using Base.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Data.nDataServiceManager
{
    public interface IDataServiceManager
    {
        string GetDataHost();
        IDataService GetDataService();
		List<IDataService> GetAllDataService();

		IGlobalDataService GetGlobalDataService();
    }
}
