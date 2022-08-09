using Base.Boundary.nValueTypes.nConstType;
using Core.GenericWebScaffold.nUtils.nValueTypes;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommandIDs
{
    public class CommandIDs : cBaseConstType<CommandIDs>
    {

        public static List<CommandIDs> TypeList { get; set; }

        public static CommandIDs GetCommandList = new CommandIDs(GetVariableName(() => GetCommandList), 1, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs GetActionList = new CommandIDs(GetVariableName(() => GetActionList), 2, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs SetLanguage = new CommandIDs(GetVariableName(() => SetLanguage), 3, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });

        public static CommandIDs MessageResult = new CommandIDs(GetVariableName(() => MessageResult), 4, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs GetEnumVariableList = new CommandIDs(GetVariableName(() => GetEnumVariableList), 5, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer }, true);
        public static CommandIDs GetServerDateTime = new CommandIDs(GetVariableName(() => GetServerDateTime), 6, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs ChangeServiceState = new CommandIDs(GetVariableName(() => ChangeServiceState), 7, "", true, new List<RoleIDs>() { RoleIDs.Admin });


        public static CommandIDs Login = new CommandIDs(GetVariableName(() => Login), 10, "", true, new List<RoleIDs>() { RoleIDs.Unlogined }, _DoFlowCheck: true);
        public static CommandIDs Logout = new CommandIDs(GetVariableName(() => Logout), 11, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Developer });
        public static CommandIDs CheckLogin = new CommandIDs(GetVariableName(() => CheckLogin), 12, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer }, _DoFlowCheck: true);
        public static CommandIDs Register = new CommandIDs(GetVariableName(() => Register), 13, "", true, new List<RoleIDs>() { RoleIDs.Unlogined });
        public static CommandIDs SellerRegisterCheck = new CommandIDs(GetVariableName(() => SellerRegisterCheck), 4028, "", true, new List<RoleIDs>() { RoleIDs.Unlogined });
        public static CommandIDs SellerRegister = new CommandIDs(GetVariableName(() => SellerRegister), 4029, "", true, new List<RoleIDs>() { RoleIDs.Unlogined });
        public static CommandIDs GetUser = new CommandIDs(GetVariableName(() => GetUser), 14, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Developer });
        
        public static CommandIDs GetGlobalParamList = new CommandIDs(GetVariableName(() => GetGlobalParamList), 108, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });

        public static CommandIDs DeleteFile = new CommandIDs(GetVariableName(() => DeleteFile), 110, "", true, new List<RoleIDs>() { RoleIDs.Seller });
        public static CommandIDs GetMenuList = new CommandIDs(GetVariableName(() => GetMenuList), 1000, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs GetPageList = new CommandIDs(GetVariableName(() => GetPageList), 1001, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });

        public static CommandIDs DataSource_Read = new CommandIDs(GetVariableName(() => DataSource_Read), 2000, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_Create = new CommandIDs(GetVariableName(() => DataSource_Create), 2001, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_Update = new CommandIDs(GetVariableName(() => DataSource_Update), 2002, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_Delete = new CommandIDs(GetVariableName(() => DataSource_Delete), 2003, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_GetMetaData = new CommandIDs(GetVariableName(() => DataSource_GetMetaData), 2004, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs DataSource_GetSettings = new CommandIDs(GetVariableName(() => DataSource_GetSettings), 2005, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });



        public static CommandIDs GetLoginCheck = new CommandIDs(GetVariableName(() => GetLoginCheck), 10010, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Unlogined, RoleIDs.Developer });
        public static CommandIDs GetConfigParamList = new CommandIDs(GetVariableName(() => GetConfigParamList), 10041, "", false, new List<RoleIDs>() { RoleIDs.Admin });
        public static CommandIDs SaveConfigParamList = new CommandIDs(GetVariableName(() => SaveConfigParamList), 10042, "", false, new List<RoleIDs>() { RoleIDs.Admin }, _DoFlowCheck: true);
        public static CommandIDs GetBatchJobDetail = new CommandIDs(GetVariableName(() => GetBatchJobDetail), 10043, "", true, new List<RoleIDs>() { RoleIDs.Admin });
        public static CommandIDs SaveBatchJobDetail = new CommandIDs(GetVariableName(() => SaveBatchJobDetail), 10044, "", true, new List<RoleIDs>() { RoleIDs.Admin }, _DoFlowCheck: true);
        public static CommandIDs GetLanguageWordByCode = new CommandIDs(GetVariableName(() => GetLanguageWordByCode), 10048, "", true, new List<RoleIDs>() { RoleIDs.Admin });
        public static CommandIDs SaveLanguageWordList = new CommandIDs(GetVariableName(() => SaveLanguageWordList), 10049, "", false, new List<RoleIDs>() { RoleIDs.Admin });
        public static CommandIDs SaveConfigBackup = new CommandIDs(GetVariableName(() => SaveConfigBackup), 10053, "", true, new List<RoleIDs>() { RoleIDs.Admin });
        public static CommandIDs ReloadConfigBackup = new CommandIDs(GetVariableName(() => ReloadConfigBackup), 10054, "", true, new List<RoleIDs>() { RoleIDs.Admin });
        public static CommandIDs BatchJobStart = new CommandIDs(GetVariableName(() => BatchJobStart), 10056, "", true, new List<RoleIDs>() { RoleIDs.Developer });
        public static CommandIDs ForceLogout = new CommandIDs(GetVariableName(() => ForceLogout), 10057, "", true, new List<RoleIDs>() { RoleIDs.Admin, RoleIDs.Customer, RoleIDs.Seller, RoleIDs.Developer });
        




        public bool Enabled { get; set; }
        public string Info { get; set; }
        public bool CacheIt { get; set; }
        public bool DoFlowCheck { get; set; }
        public List<RoleIDs> MainRoles { get; set; }

        public CommandIDs(string _Name, int _ID, string _Info, bool _Enabled, List<RoleIDs> _MainRoles, bool _CacheIt = false, bool _DoFlowCheck = false)
            : base(_Name, _Name, _ID)
        {
            TypeList = TypeList ?? new List<CommandIDs>();
            Info = _Info;
            Enabled = _Enabled;
            CacheIt = _CacheIt;
            DoFlowCheck = _DoFlowCheck;
            MainRoles = _MainRoles;

            TypeList.Add(this);
        }
        public static DataTable Table()
        {
            return Table(TypeList);
        }
        public static CommandIDs GetByID(int _ID, CommandIDs _DefaultCommandID)
        {
            return GetByID(TypeList, _ID, _DefaultCommandID);
        }
        public static CommandIDs GetByName(string _Name, CommandIDs _DefaultCommandID)
        {
            return GetByName(TypeList, _Name, _DefaultCommandID);
        }
    }
}
