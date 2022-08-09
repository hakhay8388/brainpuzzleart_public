using Base.Boundary.nData;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;

namespace Base.Communication.nDataFileEntity
{
    public class cCommunicationSettingEntity : cFileEntity<cCommunicationSettingEntity>
    {
		[Default("127.0.0.1")]
		public virtual string Test { get; set; }

	}
}
