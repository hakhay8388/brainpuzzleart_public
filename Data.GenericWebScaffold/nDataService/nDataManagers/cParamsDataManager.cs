using Base.Core.nCore;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.FileData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Base.Data.nDataService.nDatabase.nSql;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Base.Data.nDataServiceManager;
using Data.GenericWebScaffold.nDefaultValueTypes;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cParamsDataManager : cBaseDataManager
    {
        public cParamsDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

        public cGlobalParamEntity GetParamByCode(string _Code)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cGlobalParamEntity __MenuEntity = __DataService.Database.Query<cGlobalParamEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.Code).Eq(_Code)
                .ToQuery()
                .ToList()
                .FirstOrDefault();
            return __MenuEntity;
        }
        public List<dynamic> GetAllParams()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            List<dynamic> __GlobalParams = __DataService.Database.Query<cGlobalParamEntity>()
                .SelectAll()
                .Where()
                .ToQuery()
                .ToDynamicObjectList();
            return __GlobalParams;
        }
        public List<dynamic> GetAllParamsBackup()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            List<dynamic> __GlobalParams = __DataService.Database.Query<cGlobalParamEntity>()
                .SelectColumn(__Item => __Item.Code)
                .SelectColumn(__Item => __Item.Value)
                .Where()
                .ToQuery()
                .ToDynamicObjectList();
            return __GlobalParams;
        }

        public cGlobalParamEntity AddGlobalParam(string _Code, string _Name, object _Value, string _TypeFullName, int _Order)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cGlobalParamEntity __GlobalParamEntity = __DataService.Database.CreateNew<cGlobalParamEntity>();
            __GlobalParamEntity.Name = _Name;
            __GlobalParamEntity.Code = _Code;
            __GlobalParamEntity.SortOrder = _Order;
            __GlobalParamEntity.Value = _Value.ToString();
            __GlobalParamEntity.TypeFullName = _TypeFullName;
            __GlobalParamEntity.Save();

            return __GlobalParamEntity;
        }
        public cGlobalParamEntity UpdateGlobalParam(long _ID, string _Value)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cGlobalParamEntity __GlobalParamEntity = __DataService.Database.GetEntityByID<cGlobalParamEntity>(_ID);

            __GlobalParamEntity.Value = _Value;
            __GlobalParamEntity.Save();

            return __GlobalParamEntity;
        }

        public cGlobalParamEntity CreateMenuIfNotExists(string _Code, string _Name, object _Value, string _TypeFullName, int _Order)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cGlobalParamEntity __GlobalParamEntity = GetParamByCode(_Code);
            if (__GlobalParamEntity == null)
            {
                __GlobalParamEntity = AddGlobalParam(_Code, _Name, _Value, _TypeFullName, _Order);
            }
            return __GlobalParamEntity;
        }

        public cGlobalParamEntity CreateMenuIfNotExists(DefaultGlobalParamsIDs _DefaultGlobalParamsID)
        {
            return CreateMenuIfNotExists(_DefaultGlobalParamsID.Code, _DefaultGlobalParamsID.Name, _DefaultGlobalParamsID.Value, _DefaultGlobalParamsID.Value.GetType().FullName, _DefaultGlobalParamsID.Order);
        }
    }
}
