using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Base.FileData.nFileDataService;
using Data.GenericWebScaffold.nDataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.BatchJobService.nDataService.nDataManagers.nLoaders
{
    public class cBaseDataLoader : cCoreService<cGenericWebScaffoldDataServiceContext>
    {
        public IDataServiceManager DataServiceManager { get; set; }
        public IFileDateService FileDataService { get; set; }
        public cBaseDataLoader(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext)
        {
            DataServiceManager = _DataServiceManager;
            FileDataService = _FileDataService;
        }
    }
}
