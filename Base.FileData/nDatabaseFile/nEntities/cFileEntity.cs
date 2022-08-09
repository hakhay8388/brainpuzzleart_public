using Base.Boundary.nCore.nObjectLifeTime;
using Base.Core.nAttributes;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nFileDataService;


namespace Base.FileData.nDatabaseFile.nEntities
{
    [Register(null, false, false, false, false, LifeTime.PerResolveLifetimeManager)]
    public class cFileEntity<TFileEntity> : IFileEntity where TFileEntity : IFileEntity
    {
        cFileDataService FileDataService { get; set; }
        [Default(0)]
        public virtual long ID { get; set; }
        public bool IsExists { get; set; }

        public void Save()
        {
            FileDataService.Save<TFileEntity>((TFileEntity)(object)this);
        }

        public void Delete()
        {
            FileDataService.Delete<TFileEntity>((TFileEntity)(object)this);
        }
    }
}
