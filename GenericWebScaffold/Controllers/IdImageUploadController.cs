using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.nWebGraph;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.Controllers
{
    [Route("api/[controller]")]
    public class IdImageUploadController : cBaseImageUploadController
    {
        public IdImageUploadController(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, IHubContext<SignalRHub> _SignalHub, cFileDataManager _FileDataManager)
            : base(_App, _WebGraph, _DataServiceManager, _SignalHub, _FileDataManager)
        {
        }

        public override bool CheckPermission()
        {
            return this.ClientSession.IsLogined && this.ClientSession.User.Actor.GetValue().Roles.GetValue().Code == RoleIDs.Seller.Code;

        }
        public override EFileType GetFileType()
        {
            return EFileType.IdImage;
        }

        public override bool MustDeleteOld()
        {
            return true;
        }

        /*
        public override string GetImagePath()
        {
			return App.Configuration.IdImagePath;

		}

		public override void SaveFileName(string _FullPath, string _FileString, string _Extension)
		{
			IDataService __DataService = DataServiceManager.GetDataService();

			__DataService.Perform(() =>
			{
				cActorType_SellerDetailEntity __SellerDetail = this.ClientSession.User.Actor.GetValue().SellerDetail;
				string __OldFileName = __SellerDetail.IdPhotoImageName;
				if (!string.IsNullOrEmpty(__OldFileName))
				{
					App.Handlers.FileHandler.DeleteFile(Path.Combine(GetImagePath(), __OldFileName));
				}

				__SellerDetail.IdPhotoImageName = _FileString;
				SetCompleteStep(__SellerDetail);

				__SellerDetail.Save();
			});
		}
		*/
    }
}
