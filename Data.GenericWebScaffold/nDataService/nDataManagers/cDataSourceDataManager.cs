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
    public class cDataSourceDataManager : cBaseDataManager
    {
        public cDataSourceDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

        public List<cDataSourceColumnEntity> GetDataSourceColumnsByRoleAndDataSourceID(List<cRoleEntity> _RoleList, DataSourceIDs _DataSourceID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cDataSourceColumnEntity __Alias = null;

            var __Temp = __DataService.Database.Query<cDataSourceColumnEntity>(() => __Alias)
                .Distinct()
                .SelectAliasAllColumns<cDataSourceColumnEntity>(() => __Alias)
                .Inner<cRoleDataSourceColumnMapEntity>().Join().On()
                .Operand<cDataSourceColumnEntity>().Eq(() => __Alias.ID)
                .And
                .PrOpen;

            for (int i = 0; i < _RoleList.Count; i++)
            {
                if (i != 0) __Temp = __Temp.Or;
                __Temp.Operand<cRoleEntity>().Eq(_RoleList[i].ID);
            }


            List<cDataSourceColumnEntity> __Result = __Temp.PrClose
                .ToQuery()
                .Where()
                .Operand(__Item => __Item.DataSourceCode).Eq(_DataSourceID.Code)
                .ToQuery()
                .ToList();


            return __Result;
        }

        public List<cDataSourceColumnEntity> GetDataSourceColumns(DataSourceIDs _DataSourceID)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cDataSourceColumnEntity __Alias = null;

            List<cDataSourceColumnEntity> __Result = __DataService.Database.Query<cDataSourceColumnEntity>(() => __Alias)
                .Distinct()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.DataSourceCode).Eq(_DataSourceID.Code)
                .ToQuery()
                .ToList();

            return __Result;
        }

        public cDataSourceColumnEntity GetDataSourceColumnsByRoleAndDataSourceID(cRoleEntity _Role, DataSourceIDs _DataSourceID, cDataSourceColumnEntity _Column)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cDataSourceColumnEntity __Alias = null;

            List<cDataSourceColumnEntity> __Result = __DataService.Database.Query<cDataSourceColumnEntity>(() => __Alias)
                .Distinct()
                .SelectAll()
                .Inner<cRoleDataSourceColumnMapEntity>().Join().On()
                .Operand<cDataSourceColumnEntity>().Eq(() => __Alias.ID)
                .And
                .Operand<cRoleEntity>().Eq(_Role.ID)
                .ToQuery()
                .Where()
                .Operand(__Item => __Item.DataSourceCode).Eq(_DataSourceID.Code)
                .And
                .Operand(__Item => __Item.ColumnName).Eq(_Column.ColumnName)
                .ToQuery()
                .ToList();


            return __Result.FirstOrDefault();
        }

        public bool IsDataSourceColumnExistsInRole(cRoleEntity _Role, DataSourceIDs _DataSourceID, cDataSourceColumnEntity _Column)
        {
            return GetDataSourceColumnsByRoleAndDataSourceID(_Role, _DataSourceID, _Column) != null;
        }

        public cDataSourceColumnEntity GetColumnByColumnNameInDataSource(DataSourceIDs _DataSourceID, string _ColumnName)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            return __DataService.Database.Query<cDataSourceColumnEntity>().SelectAll().Where()
                .Operand(__Item => __Item.ColumnName).Eq(_ColumnName)
                .And
                .Operand(__Item => __Item.DataSourceID).Eq(_DataSourceID.ID)
                .ToQuery()
                .ToList().FirstOrDefault();
        }

        public void AddColumnToDataSource(DataSourceIDs _DataSourceID, string _ColumnName)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            if (GetColumnByColumnNameInDataSource(_DataSourceID, _ColumnName) == null)
            {
                cDataSourceColumnEntity __DataSourceColumnEntity = __DataService.Database.CreateNew<cDataSourceColumnEntity>();
                __DataSourceColumnEntity.ColumnName = _ColumnName;
                __DataSourceColumnEntity.DataSourceCode = _DataSourceID.Code;
                __DataSourceColumnEntity.DataSourceID = _DataSourceID.ID;
                __DataSourceColumnEntity.Save();
            }
        }

        public void DeleteColumnFromDataSourceAndRoles(List<cDataSourceColumnEntity> _Columns)
        {
            foreach (cDataSourceColumnEntity __Column in _Columns)
            {
                DeleteColumnFromDataSourceAndRoles(__Column);
            }
        }

        public void DeleteColumnFromDataSourceAndRoles(cDataSourceColumnEntity _Column)
        {
            IDataService __DataService = DataServiceManager.GetDataService();
            List<cRoleEntity> __Roles = _Column.Roles.ToList();
            foreach (cRoleEntity __Role in __Roles)
            {
                _Column.Roles.Delete(__Role);
            }
            _Column.Delete();
        }

        public bool IsDataSourceExistsInRole(cRoleEntity _Role, DataSourceIDs _DataSourceID)
        {
            return GetDataSourceInRoleByDataSourceID(_Role, _DataSourceID) != null;
        }

        public List<cDataSourcePermissionEntity> GetDataSourceInRoleByDataSourceID(List<cRoleEntity> _RoleList, DataSourceIDs _DataSourceIDs)
        {
            List<cDataSourcePermissionEntity> __Result = new List<cDataSourcePermissionEntity>();
            foreach (cRoleEntity __Item in _RoleList)
            {
                cDataSourcePermissionEntity __Permission = GetDataSourceInRoleByDataSourceID(__Item, _DataSourceIDs);
                if (__Permission != null)
                {
                    __Result.Add(__Permission);
                }
            }
            return __Result;
        }

        public cDataSourcePermissionEntity GetDataSourceInRoleByDataSourceID(cRoleEntity _Role, DataSourceIDs _DataSourceIDs)
        {
            return _Role.DataSource.ToList().Find(__Item => __Item.DataSourceID == _DataSourceIDs.ID);
        }

        public void AddDataSourceColumnToRole(cRoleEntity _Role, DataSourceIDs _DataSourceID, cDataSourceColumnEntity _Column)
        {
            if (!IsDataSourceColumnExistsInRole(_Role, _DataSourceID, _Column))
            {
                IDataService __DataService = DataServiceManager.GetDataService();
                _Role.DataSourceColumns.AddValue(_Column);
            }
        }



        public void AddDataSourceColumnToRole(cRoleEntity _Role, DataSourceIDs _DataSourceID, List<cDataSourceColumnEntity> _Columns)
        {
            foreach (var _Column in _Columns)
            {
                AddDataSourceColumnToRole(_Role, _DataSourceID, _Column);
            }
        }

        public void AddAllDatasourceColumnToRole(cRoleEntity _Role, DataSourceIDs _DataSourceID)
        {
            List<cDataSourceColumnEntity> __Columns = GetDataSourceColumns(_DataSourceID);
            AddDataSourceColumnToRole(_Role, _DataSourceID, __Columns);
        }

        public void AddDataSourceToRole(cRoleEntity _Role, DataSourceIDs _DataSourceID, bool _CanCreate, bool _CanRead, bool _CanUpdate, bool _CanDelete)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            if (!IsDataSourceExistsInRole(_Role, _DataSourceID))
            {

                cDataSourcePermissionEntity __DataSourcePermissionEntity = __DataService.Database.CreateNew<cDataSourcePermissionEntity>();
                __DataSourcePermissionEntity.CanCreate = _CanCreate;
                __DataSourcePermissionEntity.CanRead = _CanRead;
                __DataSourcePermissionEntity.CanUpdate = _CanUpdate;
                __DataSourcePermissionEntity.CanDelete = _CanDelete;
                __DataSourcePermissionEntity.DataSourceID = _DataSourceID.ID;
                __DataSourcePermissionEntity.DataSourceCode = _DataSourceID.Code;
                __DataSourcePermissionEntity.Save();

                _Role.DataSource.AddValue(__DataSourcePermissionEntity);
            }
            else
            {
                cDataSourcePermissionEntity __DataSourcePermissionEntity = GetDataSourceInRoleByDataSourceID(_Role, _DataSourceID);

                __DataSourcePermissionEntity.CanCreate = _CanCreate;
                __DataSourcePermissionEntity.CanRead = _CanRead;
                __DataSourcePermissionEntity.CanUpdate = _CanUpdate;
                __DataSourcePermissionEntity.CanDelete = _CanDelete;
                __DataSourcePermissionEntity.DataSourceID = _DataSourceID.ID;
                __DataSourcePermissionEntity.DataSourceCode = _DataSourceID.Code;
                __DataSourcePermissionEntity.Save();
            }
        }
    }
}
