using Base.Data.nDataFileEntity;
using Base.Data.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Data.nDataServiceManager
{
    public interface IGlobalDefaultDataLoader : IDefaultDataLoader
    {
        cDBConnectionSettingEntity GetConnectionSettingByHostName(string _HostName);
    }
}
