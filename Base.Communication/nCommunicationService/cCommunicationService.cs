using Base.Boundary.nCore.nBootType;
using Base.Boundary.nCore.nObjectLifeTime;
using Base.Boundary.nData;
using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Data.nConfiguration;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Communication.nCommunicationService
{
    [Register(typeof(ICommunicationService), false, false, false, false, LifeTime.PerResolveLifetimeManager)]
    public class cCommunicationService : cCoreService<cCommunicationServiceContext>, ICommunicationService
	{
        public cCommunicationService(cCommunicationServiceContext _CoreServiceContext)
            : base(_CoreServiceContext)
        {
        }

		public void CreateServer()
		{

		}
    }
}
