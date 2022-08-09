using Base.Boundary.nValueTypes.nConstType;
using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataServiceManager;
using Base.FileData;
using Base.FileData.nFileDataService;
using Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders.nLoaderIDs;
using Data.GenericWebScaffold.nDefaultValueTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Data.GenericWebScaffold.nDataService.nDataManagers.nLoaders
{
    public class cBaseDataLoader : cCoreService<cGenericWebScaffoldDataServiceContext>
    { 
		public LoaderIDs LoaderID { get; set; }
		public IDataServiceManager DataServiceManager { get; set; }
        public IFileDateService FileDataService { get; set; }
		public cChecksumDataManager ChecksumDataManager { get; set; }

		public cBaseDataLoader(LoaderIDs _LoaderID, cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService, cChecksumDataManager _ChecksumDataManager)
          : base(_CoreServiceContext)
        {
			LoaderID = _LoaderID;
			ChecksumDataManager = _ChecksumDataManager;
			DataServiceManager = _DataServiceManager;
            FileDataService = _FileDataService;
        }

		public string GetTotalString<TType>(List<TType> _List)
		{
			JArray __JArray = JArray.FromObject(_List);
			return __JArray.ToString();
		}
	}
}
