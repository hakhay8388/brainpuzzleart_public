using Base.Core.nApplication;
using Base.Core.nApplication.nStarter;
using Base.Core.nCore;
using Base.Data.nDataFileEntity;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Base.FileData.nFileDataService;
using Core.GenericWebScaffold.nWebGraph;
using Data.GenericWebScaffold.nDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericWebScaffold
{
    public class cStarter : cCoreObject, IStarter
    {
        IDataServiceManager DataServiceManager { get; set; }
        IFileDateService FileDataService { get; set; }
        cVersionEntity VersionEntity { get; set; }
        cSharedVolumeConfigEntity SharedVolumeConfigEntity { get; set; }

        public cStarter(cApp _App, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
            : base(_App)
        {
            DataServiceManager = _DataServiceManager;
            FileDataService = _FileDataService;
        }

        public void Start(cApp _App)
        {
            VersionEntity = FileDataService.FindByID<cVersionEntity>(1);
            if (!VersionEntity.IsExists)
            {
                VersionEntity.Save();
            }
            SharedVolumeConfigEntity = FileDataService.FindByID<cSharedVolumeConfigEntity>(1);
            if (!SharedVolumeConfigEntity.IsExists)
            {
                SharedVolumeConfigEntity.Save();
            }

            
            Type __Type = VersionEntity.GetType();
        }



    }
}
