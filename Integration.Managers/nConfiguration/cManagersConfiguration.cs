using Base.Boundary.nCore.nBootType;
using Base.Data.nConfiguration;
using Base.FileData.nConfiguration;
using Base.FileData.nFileDataService;
using Integration.Boundary.nData;
using Integration.Managers.nConfiguration;
using Integration.MicroServiceGraph.nConfiguration;
using Integration.MicroServiceGraph.nDataFileEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Managers.nConfiguration
{
    public class cManagersConfiguration : cMicroServiceConfiguration
	{
		public cManagersConfiguration(EBootType _BootType, EDomainType _DomainType)
            :base(_BootType, _DomainType)
        {
        }

        public override void Init()
        {
            base.Init();
		}
	}
}
