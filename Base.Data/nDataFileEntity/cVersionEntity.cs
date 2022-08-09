using Base.Boundary.nData;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;

namespace Base.Data.nDataFileEntity
{
    public class cVersionEntity : cFileEntity<cVersionEntity>
    {
        [Default(1)]
        public virtual int MainVersion { get; set; }

        [Default(0)]
        public virtual int LastDBUpdateDate_TimeStamp { get; set; }
        
        [Default(1)]
        public virtual int DBVersion { get; set; }

        [Default(1)]
        public virtual int VersionExtension { get; set; }
    }
}
