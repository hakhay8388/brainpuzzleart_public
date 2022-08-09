

using Base.Boundary.nCore.nBootType;
using Base.Boundary.nData;
using Base.Data.nConfiguration;
using Base.Data.nDataFileEntity;

namespace Data.GenericWebScaffold.nConfiguration
{
    public class cGenericWebScaffoldDataConfiguration : cDataConfiguration
    {
        cDBConnectionSettingEntity SqlServerConnectionSettingEntity { get; set; }

        public cGenericWebScaffoldDataConfiguration(EBootType _BootType)
            : base(_BootType)
        {
        }

        public override void Init()
        {
            base.Init();
            Reload();
        }

        public void Reload()
        {
            SqlServerConnectionSettingEntity = FileDataService.FindByID<cDBConnectionSettingEntity>(1);
            if (!SqlServerConnectionSettingEntity.IsExists)
            {
                SqlServerConnectionSettingEntity.Save();
            }
            MaxConnectCount = SqlServerConnectionSettingEntity.MaxConnectCount;
            DBUserName = SqlServerConnectionSettingEntity.UserName;
            DBPassword = SqlServerConnectionSettingEntity.Password;
            DBServer = SqlServerConnectionSettingEntity.Server;
            GlobalDBName = SqlServerConnectionSettingEntity.GlobalDBName;
            DBVendor = EDBVendor.GetByName(SqlServerConnectionSettingEntity.DBVendor, EDBVendor.MSSQL);
        }

        public void SaveDatabaseSettings()
        {
            SqlServerConnectionSettingEntity.DBVendor = DBVendor.Name;
            SqlServerConnectionSettingEntity.UserName = DBUserName;
            SqlServerConnectionSettingEntity.Password = DBPassword;
            SqlServerConnectionSettingEntity.Server = DBServer;
            SqlServerConnectionSettingEntity.GlobalDBName = GlobalDBName;
            SqlServerConnectionSettingEntity.Save();
        }

        /* public string DBName
         {
             get
             {
                 string[] __Parameters = SqlServerConnectionSettingEntity.ConnectionString.Split(';');
                 foreach (string __Value in __Parameters)
                 {
                     string[] __Parameter = __Value.Trim().Split('=');
                     if (__Parameter[0].Trim().ToLower() == "database")
                     {
                         return __Parameter[1].Trim();
                     }
                 }
                 return "";
             }
         }*/
    }
}
