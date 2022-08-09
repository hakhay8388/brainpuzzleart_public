using Base.Boundary.nData;
using Base.FileData.nDatabaseFile.nAttributes;
using Base.FileData.nDatabaseFile.nEntities;

namespace Base.Data.nDataFileEntity
{
    public class cDBConnectionSettingEntity : cFileEntity<cDBConnectionSettingEntity>
    {
        [Default("sa")]
        public virtual string UserName { get; set; }

        [Default("123456")]
        public virtual string Password { get; set; }

        [Default("127.0.0.1")]
        public virtual string Server { get; set; }

        [Default(100)]
        public virtual int MaxConnectCount { get; set; }

        [Default(EDBVendor.MSSQL_Consts)]
        public virtual string DBVendor { get; set; }

        [Default("BPAGlobalWebDB")]
        public virtual string GlobalDBName { get; set; }
    }
}
