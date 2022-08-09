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
    public abstract class cBaseImageUploadController : cBaseController
    {
        
        cFileDataManager FileDataManager { get; set; }
        public cBaseImageUploadController(cApp _App, cWebGraph _WebGraph, IDataServiceManager _DataServiceManager, IHubContext<SignalRHub> _SignalHub, cFileDataManager _FileDataManager)
            : base(_App, _WebGraph, _DataServiceManager, _SignalHub)
        {
            FileDataManager = _FileDataManager;
        }

        public abstract bool CheckPermission();

        public virtual string GetFilePath()
        {
            return App.Configuration.UserFilePath;
        }

        public abstract EFileType GetFileType();

        public abstract bool MustDeleteOld();

        public virtual List<EFileExtentionType> GetSupportedFileTypes()
        {
            return EFileExtentionType.TypeList;

        }

        public void SetCompleteStep(cActorType_SellerDetailEntity _ActorType_SellerDetailEntity)
        {
            List<cFileEntity> __File = _ActorType_SellerDetailEntity.Files.ToList();

            if (__File.Where(__Item => __Item.FileType == EFileType.Student.ID).ToList().Count > 0
                && __File.Where(__Item => __Item.FileType == EFileType.Proof.ID).ToList().Count > 0
                && __File.Where(__Item => __Item.FileType == EFileType.IdImage.ID).ToList().Count > 0)
            {

                if (
                    !string.IsNullOrEmpty(_ActorType_SellerDetailEntity.IbanNo) && _ActorType_SellerDetailEntity.IbanNo.IndexOf('_') == -1 &&
                    !string.IsNullOrEmpty(_ActorType_SellerDetailEntity.AboutMe) /*&& !string.IsNullOrEmpty(_ConfirmationPreliminary.MeAsATeacher)*/
                && !string.IsNullOrEmpty(_ActorType_SellerDetailEntity.MyLessonsAndTeachingStyle) /*&& !string.IsNullOrEmpty(_ConfirmationPreliminary.MyTeachingMaterial)*/

                /*&& !string.IsNullOrEmpty(_ConfirmationPreliminary.VideoLink)*/ && !string.IsNullOrEmpty(_ActorType_SellerDetailEntity.IntroductoryText)
                && !string.IsNullOrEmpty(_ActorType_SellerDetailEntity.CurriculumVitaeText)
                )
                {
                    _ActorType_SellerDetailEntity.ProfileInfoStepComplete = 3;
                }
                else if (
                    !string.IsNullOrEmpty(_ActorType_SellerDetailEntity.IbanNo) && _ActorType_SellerDetailEntity.IbanNo.IndexOf('_') == -1 &&
                    !string.IsNullOrEmpty(_ActorType_SellerDetailEntity.AboutMe) /*&& !string.IsNullOrEmpty(_ConfirmationPreliminary.MeAsATeacher)*/
                && !string.IsNullOrEmpty(_ActorType_SellerDetailEntity.MyLessonsAndTeachingStyle) /*&& !string.IsNullOrEmpty(_ConfirmationPreliminary.MyTeachingMaterial)*/)
                {
                    _ActorType_SellerDetailEntity.ProfileInfoStepComplete = 2;
                }
                else
                {
                    _ActorType_SellerDetailEntity.ProfileInfoStepComplete = 1;
                }
            }

            else
            {
                _ActorType_SellerDetailEntity.ProfileInfoStepComplete = 0;
            }
        }

        private string GetFileName(EFileExtentionType _FileExtentionType)
        {
            string __FileName = App.Handlers.FileHandler.GenerateFileName("." + _FileExtentionType.Code);
            string __Path = Path.Combine(GetFilePath(), __FileName);
            if (App.Handlers.FileHandler.Exists(__Path))
            {
                return GetFileName(_FileExtentionType);
            }
            return __FileName;
        }


        public virtual void SaveFileName(string _OrgFileName, string _FileName, EFileExtentionType _FileExtentionType)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            EFileType __FileType = GetFileType();

            __DataService.Perform(() =>
            {
                cActorType_SellerDetailEntity __ActorType_SellerDetailEntity = this.ClientSession.User.Actor.GetValue().SellerDetail;


                if (MustDeleteOld())
                {
                    List<cFileEntity> __OldFiles = __ActorType_SellerDetailEntity.Files.ToList().Where(__Item => __Item.FileType == __FileType.ID).ToList();
                    if (__OldFiles.Count > 0)
                    {
                        for (int i = __OldFiles.Count - 1; i > -1; i--)
                        {
                            App.Handlers.FileHandler.DeleteFileIfExists(Path.Combine(GetFilePath(), __OldFiles[i].FileName));
                            __ActorType_SellerDetailEntity.Files.Delete(__OldFiles[i]);
                            __OldFiles[i].Delete();
                        }

                    }
                }
                cFileEntity __File = FileDataManager.AddFile(_OrgFileName, _FileName, _FileExtentionType, __FileType);

                __ActorType_SellerDetailEntity.Files.AddValue(__File);

                SetCompleteStep(__ActorType_SellerDetailEntity);

                __ActorType_SellerDetailEntity.Save();


            });
        }


        [HttpPost]
        public IActionResult Upload()
        {
            try
            {
                if (CheckPermission())
                {
                    var __Request = HttpContext.Request;

                    if (__Request.Form.Files.Count > 0)
                    {
                        string __OrginalFilename = __Request.Form.Files[0].FileName;
                        string __Extension = __OrginalFilename.Substring(__OrginalFilename.LastIndexOf(".") + 1);
                        EFileExtentionType __FileExtentionType = GetSupportedFileTypes().Where(__Item => __Item.Code.ToUpper() == __Extension.ToUpper()).ToList().FirstOrDefault();
                        cGenericWebScaffoldDataService __DataService = (cGenericWebScaffoldDataService)DataServiceManager.GetDataService();

                        if (__FileExtentionType == null && __Request.Form.Files[0].ContentType.StartsWith("image"))
                        {
                            string[] __ContentTypeParts = __Request.Form.Files[0].ContentType.Split('/');
                            if (__ContentTypeParts.Length > 1)
                            {
                                __FileExtentionType = GetSupportedFileTypes().Where(__Item => __Item.Code.ToUpper() == __ContentTypeParts[1].ToUpper()).ToList().FirstOrDefault();
                            }
                        }

                        if (__FileExtentionType != null)
                        {
                            using (var __MemoryStream = new MemoryStream())
                            {
                                __Request.Form.Files[0].CopyTo(__MemoryStream);

                                // Upload the file if less than 10 MB
                                if (__MemoryStream.Length < __DataService.UploadFileMaxSize)
                                {
                                    string __FileName = GetFileName(__FileExtentionType);
                                    string __Path = Path.Combine(GetFilePath(), __FileName);

                                    using (Stream __Stream = new FileStream(__Path, FileMode.Create))
                                    {
                                        __MemoryStream.WriteTo(__Stream);
                                    }

                                    SaveFileName(__OrginalFilename, __FileName, __FileExtentionType);

                                    //return Ok();
                                    return StatusCode(StatusCodes.Status200OK);
                                }
                                else
                                {
                                    return BadRequest("FileSizeTooLarge");
                                }
                            }
                        }
                        else
                        {
                            return BadRequest("FileFormatNotRecognised");
                        }
                    }
                    else
                    {
                        return BadRequest("NoFileSelected");
                    }
                }
                else
                {
                    return BadRequest("NoPermission");
                }

            }
            catch (Exception _Ex)
            {
                App.Loggers.CoreLogger.LogError(_Ex);
                return BadRequest("FileUploadError");
            }
        }
    }
}
