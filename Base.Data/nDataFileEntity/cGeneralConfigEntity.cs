using Base.Boundary.nData;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;

namespace Base.Data.nDataFileEntity
{
    public class cGeneralConfigEntity : cFileEntity<cGeneralConfigEntity>
    {
		[Default("")]
		public virtual string WebSitePath { get; set; }
    }
}
