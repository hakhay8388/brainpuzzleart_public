using Base.Boundary.nData;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;

namespace Integration.MicroServiceGraph.nDataFileEntity
{
    public class cMicroServiceSettingEntity : cFileEntity<cMicroServiceSettingEntity>
    {
		[Default("")]
		public virtual string PublisherServerIP { get; set; }

		[Default("")]
		public virtual string PublisherServerPort { get; set; }

		[Default("")]
		public virtual string MicroServiceTriggerServerIP { get; set; }

		[Default("")]
		public virtual string MicroServiceTriggerServerPort { get; set; }

		[Default("")]
		public virtual string MicroServiceListenServerIP { get; set; }
		[Default("")]
		public virtual string MicroServiceListenServerPort { get; set; }

	}
}
