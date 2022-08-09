using Base.Data.nDataService.nDatabase;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Data.nDataService
{
    public interface IGlobalDataService : IDataService
    {
        void LockPofile(Action _ServiceMethod);
        bool IsProfileLocked();
    }
}
