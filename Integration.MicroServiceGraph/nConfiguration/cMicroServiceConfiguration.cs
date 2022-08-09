using Base.Boundary.nCore.nBootType;
using Base.Data.nConfiguration;
using Base.FileData.nConfiguration;
using Base.FileData.nFileDataService;
using Data.GenericWebScaffold.nConfiguration;
using Integration.Boundary.nData;
using Integration.MicroServiceGraph.nDataFileEntity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.MicroServiceGraph.nConfiguration
{
    public class cMicroServiceConfiguration : cGenericWebScaffoldDataConfiguration
	{

		/// <summary>
		/// Pub Sub mimarisi için gerekli ayarlar
		/// </summary>
		public string PublisherServerIP { get; set; }
		public string PublisherServerPort { get; set; }
		public string MicroServiceTriggerServerIP { get; set; }
		public string MicroServiceTriggerServerPort { get; set; }
		/// /////////////////////////////////////////////
		/// /////////////////////////////////////////////
		/// /////////////////////////////////////////////

		/// <summary>
		/// Server Client mimarisi için gerekli ayarlar
		/// </summary>
		public string MicroServiceListenServerIP { get; set; }
		public string MicroServiceListenServerPort { get; set; }
		/////////////////////////////////////////////
		/// /////////////////////////////////////////////
		/// /////////////////////////////////////////////


		public EDomainType DomainType { get; set; }



		public cMicroServiceConfiguration(EBootType _BootType, EDomainType _DomainType)
            :base(_BootType)
        {
			DomainType = _DomainType;
		}

        public override void Init()
        {
            base.Init();
			LoadCommunicationConfiguration();
		}

		private void LoadCommunicationConfiguration()
		{
			cMicroServiceSettingEntity __MicroServiceSettingEntity = FileDataService.FindByID<cMicroServiceSettingEntity>(1);

			string __PublisherServerIP = Environment.GetEnvironmentVariable("PublisherServerIP");
			if (!__PublisherServerIP.IsNullOrEmpty()) __MicroServiceSettingEntity.PublisherServerIP = __PublisherServerIP;

			string __PublisherServerPort = Environment.GetEnvironmentVariable("PublisherServerPort");
			if (!__PublisherServerPort.IsNullOrEmpty()) __MicroServiceSettingEntity.PublisherServerPort = __PublisherServerPort; 

			string __MicroServiceTriggerServerIP = Environment.GetEnvironmentVariable("MicroServiceTriggerServerIP");
			if (!__MicroServiceTriggerServerIP.IsNullOrEmpty()) __MicroServiceSettingEntity.MicroServiceTriggerServerIP = __MicroServiceTriggerServerIP;

			string __MicroServiceTriggerServerPort = Environment.GetEnvironmentVariable("MicroServiceTriggerServerPort");
			if (!__MicroServiceTriggerServerPort.IsNullOrEmpty()) __MicroServiceSettingEntity.MicroServiceTriggerServerPort = __MicroServiceTriggerServerPort;

			string __MicroServiceListenServerIP = Environment.GetEnvironmentVariable("MicroServiceListenServerIP");
			if (!__MicroServiceListenServerIP.IsNullOrEmpty()) __MicroServiceSettingEntity.MicroServiceListenServerIP = __MicroServiceListenServerIP;

			string __MicroServiceListenServerPort = Environment.GetEnvironmentVariable("MicroServiceListenServerPort");
			if (!__MicroServiceListenServerPort.IsNullOrEmpty()) __MicroServiceSettingEntity.MicroServiceListenServerPort = __MicroServiceListenServerPort;


			__MicroServiceSettingEntity.Save();

			PublisherServerIP = __MicroServiceSettingEntity.PublisherServerIP;
			PublisherServerPort = __MicroServiceSettingEntity.PublisherServerPort;
			MicroServiceTriggerServerIP = __MicroServiceSettingEntity.MicroServiceTriggerServerIP;
			MicroServiceTriggerServerPort = __MicroServiceSettingEntity.MicroServiceTriggerServerPort;

			MicroServiceListenServerIP = __MicroServiceSettingEntity.MicroServiceListenServerIP;
			MicroServiceListenServerPort = __MicroServiceSettingEntity.MicroServiceListenServerPort;
		}
	}
}
