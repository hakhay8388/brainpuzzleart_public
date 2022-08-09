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
	public class ProfileImageUploadController : cBaseImageUploadController
	{
		public ProfileImageUploadController(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, IHubContext<SignalRHub> _SignalHub, cFileDataManager _FileDataManager)
			: base(_App, _WebGraph, _DataServiceManager, _SignalHub, _FileDataManager)
		{
		}

        public override bool CheckPermission()
        {
			return this.ClientSession.IsLogined;

		}

        public override string GetFilePath()
        {
			return App.Configuration.ProfileImagePath;
		}

        public override EFileType GetFileType()
        {
            throw new NotImplementedException();
        }

        public override List<EFileExtentionType> GetSupportedFileTypes()
        {
			List<EFileExtentionType> __FileExtentionType = new List<EFileExtentionType>();
			__FileExtentionType.Add(EFileExtentionType.JPEG);
			__FileExtentionType.Add(EFileExtentionType.JPG);
			__FileExtentionType.Add(EFileExtentionType.PNG);
			return __FileExtentionType;
		}

        public override bool MustDeleteOld()
        {
            throw new NotImplementedException();
        }

        public override void SaveFileName(string _OrgFileName, string _FileString, EFileExtentionType _FileExtentionType)
		{
			IDataService __DataService = DataServiceManager.GetDataService();

			cActorEntity __Actor = this.ClientSession.User.Actor.GetValue();
			if (__Actor.Roles.GetValue().Code == RoleIDs.Seller.Code)
			{
				__DataService.Perform(() =>
				{
					
					if (this.ClientSession.User.UserDetail.ProfileImage != __Actor.SellerDetail.ProfileImage && __Actor.SellerDetail.ProfileImage != "DefaultProfile.png")
					{
						App.Handlers.FileHandler.DeleteFile(Path.Combine(GetFilePath(), __Actor.SellerDetail.ProfileImage));
					}

					__Actor.SellerDetail.ProfileImage = _FileString;
					__Actor.SellerDetail.Save();
				});
			}
			else
            {
				__DataService.Perform(() =>
				{
					if (this.ClientSession.User.UserDetail.ProfileImage != "DefaultProfile.png")
					{
						App.Handlers.FileHandler.DeleteFile(Path.Combine(GetFilePath(), this.ClientSession.User.UserDetail.ProfileImage));
					}

					this.ClientSession.User.UserDetail.ProfileImage = _FileString;
					this.ClientSession.User.UserDetail.Save(this.ClientSession.User);
				});
			}
		} 
		 
	}
}
