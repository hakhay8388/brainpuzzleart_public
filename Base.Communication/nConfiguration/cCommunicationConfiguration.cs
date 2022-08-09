using Base.Boundary.nCore.nBootType;
using Base.Communication.nDataFileEntity;
using Base.Data.nConfiguration;
using Base.FileData.nConfiguration;
using Base.FileData.nFileDataService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Communication.nConfiguration
{
    public class cCommunicationConfiguration : cDataConfiguration
	{
		public cCommunicationSettingEntity CommunicationSettingEntity { get; set; }

		public cCommunicationConfiguration(EBootType _BootType)
            :base(_BootType)
        {
        }

        public override void Init()
        {
            base.Init();
			LoadCommunicationConfiguration();
		}

		private void LoadCommunicationConfiguration()
		{
			CommunicationSettingEntity = FileDataService.FindByID<cCommunicationSettingEntity>(1);
		}
	}
}
