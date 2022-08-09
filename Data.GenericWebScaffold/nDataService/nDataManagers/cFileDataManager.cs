using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Data.Boundary.nData;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cFileDataManager : cBaseDataManager
    {
        public cFileDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }
        public cFileEntity AddFile(string _OrgFileName, string _FileName, EFileExtentionType _FileExtentionType, EFileType __FileType)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cFileEntity __File = __DataService.Database.CreateNew<cFileEntity>();

            __File.OrginalFileName = _OrgFileName;
            __File.FileName = _FileName;
            __File.Extention = _FileExtentionType.Code;
            __File.ExtentionType = _FileExtentionType.ID;
            __File.FileType = __FileType.ID;

            __File.Save();
            return __File;

        }

        public cFileEntity GetFileByID(long _ID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cFileEntity __FileEntity = __DataService.Database.GetEntityByID<cFileEntity>(_ID);
            return __FileEntity;
        }

        public void DeleteFileByID(long _ID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cFileEntity __FileEntity = __DataService.Database.GetEntityByID<cFileEntity>(_ID);

            cActorType_SellerDetailEntity __SellerDetail = __FileEntity.SellerDetails.GetValue();

            __SellerDetail.Files.Delete(__FileEntity);
            __FileEntity.Delete();
        }

        public void DeleteSellerFileByID(cActorType_SellerDetailEntity _SellerDetail, long _ID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            cFileEntity __FileEntity = __DataService.Database.GetEntityByID<cFileEntity>(_ID);

            cActorType_SellerDetailEntity __SellerDetail = __FileEntity.SellerDetails.GetValue();
            if (_SellerDetail.ID == __SellerDetail.ID)
            {
                __SellerDetail.Files.Delete(__FileEntity);
                __FileEntity.Delete();
            }
        }

    }
}
