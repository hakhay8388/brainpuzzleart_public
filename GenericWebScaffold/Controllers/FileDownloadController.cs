using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.nWebGraph;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Data.Boundary.nData;
using Data.GenericWebScaffold.nDataService;
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
    public class FileDownloadController : cBaseController
    {
        public cParamsDataManager ParamsDataManager { get; set; }
        public cFileDataManager FileDataManager { get; set; }
        public FileDownloadController(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, IHubContext<SignalRHub> _SignalHub
            , cParamsDataManager _ParamsDataManager
            , cFileDataManager _FileDataManager
            )
            : base(_App, _WebGraph, _DataServiceManager, _SignalHub)
        {
            ParamsDataManager = _ParamsDataManager;
            FileDataManager = _FileDataManager;
        }
        [HttpGet("[action]/{_IncomingHash}/{_FileID}")]
        public FileResult DownloadFile(string _IncomingHash, string _FileID)
        {
            cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

            if (this.ClientSession.IsLogined)
            {
                string __Hash = App.Handlers.StringHandler.UsernameAndIdMd5(this.ClientSession.User.Email, _FileID).ToString();
                if (_IncomingHash == __Hash)
                {
                    cFileEntity __FileEntity = FileDataManager.GetFileByID(_FileID.ToInt());

                    byte[] __FileBytes = System.IO.File.ReadAllBytes(__FileEntity.OrginalFileName);
                    return File(__FileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(__FileEntity.OrginalFileName));
                }
                else
                {
                    Response.StatusCode = 404;
                    return null;

                }
            }
            else
            {
                Response.StatusCode = StatusCodes.Status401Unauthorized;
                return null;
            }

        }
    }
}
