using Base.Boundary.nData;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;

namespace Base.Data.nDataFileEntity
{
    public class cSharedVolumeConfigEntity : cFileEntity<cSharedVolumeConfigEntity>
    {
		[Default("")]
		public virtual string Password { get; set; }
		[Default("")]
		public virtual string Path { get; set; }
		[Default("")]
		public virtual string UserName { get; set; }
    }
}
