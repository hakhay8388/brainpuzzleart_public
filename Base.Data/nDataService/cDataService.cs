using Base.Core.nAttributes;
using Base.Core.nCore;
using Base.Data.nConfiguration;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nDifference;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nMetadata;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable.nEntityColumn;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Base.Boundary.nData;
using Base.Data.nDataService.nDatabase;
using Base.Boundary.nCore.nBootType;

namespace Base.Data.nDataService
{
	public abstract class cDataService<TServiceContext, TBaseEntity> : cCoreService<TServiceContext>, IDataService
		where TServiceContext : cCoreServiceContext
		where TBaseEntity : cBaseEntity
	{

		public cDatabaseContext GlobalDatabaseContext { get; set; }
		public cDatabaseContext DatabaseContext { get; set; }
		public IDatabase Database { get; set; }

		public IDatabase DatabaseManager { get; set; }

		public cDataService(TServiceContext _DataServiceContext)
			: base(_DataServiceContext)
		{
		}

		public abstract void SetDBParams(string _Database);
		public virtual void LoadGlobalParams()
		{
		}

		public virtual void LoadDB(EDBVendor _DBVendor, string _Server, string _UserName, string _Password, string _Database, int _MaxConnectionCount)
		{
			GlobalDatabaseContext = new cDatabaseContext(App, _DBVendor, _Server, _UserName, _Password, "", _MaxConnectionCount);
			DatabaseContext = new cDatabaseContext(App, _DBVendor, _Server, _UserName, _Password, _Database, _MaxConnectionCount);


			AssignDatabaseManager(GlobalDatabaseContext);
			if (!DatabaseManager.Catalogs.DatabaseOperationsSQLCatalog.IsDatabaseExists(_Database))
			{
				DatabaseManager.Catalogs.DatabaseOperationsSQLCatalog.CreateDatabase(_Database);
			}
			AssignDatabase(DatabaseContext);
			SetDBParams(_Database);
		}

		public virtual void LoadDB(cDatabaseContext _DatabaseContext)
		{
			LoadDB(_DatabaseContext.Configuration.DBVendor, _DatabaseContext.Configuration.DBServer, _DatabaseContext.Configuration.DBUserName, _DatabaseContext.Configuration.DBPassword, _DatabaseContext.Configuration.GlobalDBName, _DatabaseContext.Configuration.MaxConnectCount);
		}

		public void AssignDatabase(cDatabaseContext _DatabaseContext)
		{
			if (_DatabaseContext.DBVendor == EDBVendor.MSSQL)
			{
				Database = new cSqlServerDatabase<TBaseEntity>(_DatabaseContext, false);
				Database.DataService = this;
			}
			if (Database.ControlDBConnection() && Database.DBInfo.DBVersion < _DatabaseContext.Configuration.VersionEntity.DBVersion &&
				(
					_DatabaseContext.Configuration.BootType == EBootType.Console
					|| App.Cfg<cDataConfiguration>().BootType == EBootType.Batch
					|| _DatabaseContext.Configuration.BootType == EBootType.Web
				)
			)
			{
				Perform<cDataService<TServiceContext, TBaseEntity>, TBaseEntity>(SynchronizeDB, this);
				Database.LoadVersion();
			}
		}

		public void AssignDatabaseManager(cDatabaseContext _DatabaseContext)
		{
			if (_DatabaseContext.DBVendor == EDBVendor.MSSQL)
			{
				DatabaseManager = new cSqlServerDatabase<TBaseEntity>(_DatabaseContext, true);
			}
			if (DatabaseManager.ControlDBConnection() &&
				(
					_DatabaseContext.Configuration.BootType == EBootType.Console
					|| App.Cfg<cDataConfiguration>().BootType == EBootType.Batch
					|| _DatabaseContext.Configuration.BootType == EBootType.Web
				))
			{
			}
		}

		public TBaseEntity SynchronizeDB(cDataService<TServiceContext, TBaseEntity> _DataService)
		{
			Database.DifferenceManager.CalculateDifferences();
			Database.DifferenceManager.Synchronize();
			Database.DBInfo.DBVersion = DatabaseContext.Configuration.VersionEntity.DBVersion;
			return null;
		}

		public void Perform(Func<bool> _ServiceMethod)
		{
			Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());
			InvokeTransactionalAction(_ServiceMethod);
			Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
		}

		public TOutput Perform<TInput, TOutput>(Func<TInput, TOutput> _ServiceMethod, TInput _Input)
		{
			Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());
			TOutput __Output = default(TOutput);

			InvokeTransactionalAction(() =>
			{
				__Output = _ServiceMethod.Invoke(_Input);
				return true;
			});
			Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
			return __Output;
		}

		public void Perform<TInput>(Action<TInput> _ServiceMethod, TInput _Input)
		{
			Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());

			InvokeTransactionalAction(() =>
			{
				_ServiceMethod.Invoke(_Input);
				return true;
			});
			Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
		}


		public void Perform(Action _ServiceMethod)
		{
			Console.WriteLine("Perform Begin : " + _ServiceMethod.Method.ToString());

			InvokeTransactionalAction(() =>
			{
				_ServiceMethod.Invoke();
				return true;
			});
			Console.WriteLine("Perform End : " + _ServiceMethod.Method.ToString());
		}

		public abstract void InvokeTransactionalAction(Func<bool> _ServiceMethod);
	}
}
