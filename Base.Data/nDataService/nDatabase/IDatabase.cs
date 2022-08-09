﻿using Base.Core.nApplication;
using Base.Data.nDataService.nDatabase.nCatalog;
using Base.Data.nDataService.nDatabase.nConnection;
using Base.Data.nDataService.nDatabase.nDBInfo;
using Base.Data.nDataService.nDatabase.nDifference;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nIDController;
using Base.Data.nDataService.nDatabase.nMetadata;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataUtils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase
{
    public interface IDatabase
    {
        IDataService DataService { get; set; }
        cDatabaseContext ServiceContext { get; set; }
        cApp App { get; set; }
        cConnectionPoolingManager CustomConnectionPoolingManager { get; set; }
        cConnectionPoolingManager ConnectionPoolingManager { get; set; }
        cBaseCatalogs Catalogs { get; set; }
        cIDController IDController { get; set; }
        cDBInfo DBInfo { get; set; }
        cBaseConnection DefaultConnection { get; }
        cDifferenceManager DifferenceManager { get; }
        cEntityManager EntityManager { get; }
        cMetadataManager MetadataManager { get; }
        Type GetEntityType();
        IList GetEntityListByColumnValue(Type _Type, string _ColumnName, object _Value);
        List<TEntity> GetEntityByColumnValue<TEntity>(string _ColumnName, object _Value) where TEntity : cBaseEntity;
        object GetEntityByID(Type _EntityType, long _ID);
        TEntity GetEntityByID<TEntity>(long _ID) where TEntity : cBaseEntity;
        TEntity GetEntityByID_Or_CreateNew<TEntity>(long _ID) where TEntity : cBaseEntity;
        TEntity CreateNew<TEntity>() where TEntity : cBaseEntity;
        List<TOutEntity> GetEntityByColumnValue<TOutEntity, TForeignKeyEntityColumn>(TForeignKeyEntityColumn _Column, object _Value) where TOutEntity : cBaseEntity where TForeignKeyEntityColumn : cBaseEntity;
        ISelectionDemonstrator<TEntity> Query<TEntity>() where TEntity : cBaseEntity;
        ISelectionDemonstrator<TEntity> Query<TEntity>(cBaseHardCodedValues _HardCodedValues, Expression<Func<TEntity>> _SubQueryExternalAlias) where TEntity : cBaseEntity;

        IBaseFilterForOperands<TEntity, TEntity> Delete<TEntity>() where TEntity : cBaseEntity;
        ISelectionDemonstrator<TEntity> Query<TEntity>(IQuery _Query) where TEntity : cBaseEntity;
        //ISelectionDemonstrator<TEntity> Query<TEntity>(IQuery _Query, Expression<Func<TEntity>> _SubQueryExternalAlias) where TEntity : cBaseEntity;
        ISelectionDemonstrator<TEntity> Query<TEntity>(Expression<Func<TEntity>> _Alias) where TEntity : cBaseEntity;
        ISelectionDemonstrator<TEntity> Query<TEntity>(Expression<Func<TEntity>> _Alias, IQuery _Query) where TEntity : cBaseEntity;
        //ISelectionDemonstrator<TEntity> Query<TEntity>(Expression<Func<TEntity>> _Alias, IQuery _Query, Expression<Func<TEntity>> _SubQueryExternalAlias) where TEntity : cBaseEntity;
        bool ControlDBConnection();
        void LoadVersion();
    }
}
