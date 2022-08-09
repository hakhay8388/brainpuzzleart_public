using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Base.FileData.nFileDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cBaseDataManager : cCoreService<cGenericWebScaffoldDataServiceContext>
    {
        public IDataServiceManager DataServiceManager { get; set; }
        public IFileDateService FileDataService { get; set; }
        public cBaseDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext)
        {
            DataServiceManager = _DataServiceManager;
            FileDataService = _FileDataService;
        }
    }
}
