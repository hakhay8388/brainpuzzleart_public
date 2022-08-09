using Base.Core.nApplication;
using Base.Core.nCore;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager
{
    public class cDataSourceManager : cCoreObject
    {
        List<IDataSource> DataSourceList { get; set; }

        List<ILookupDataSource> LookupDataSourceList { get; set; }
        public cDataSourceManager(cApp _App)
                : base(_App)
        {
            DataSourceList = new List<IDataSource>();
            LookupDataSourceList = new List<ILookupDataSource>();
        }
      

		public override void Init()
		{
			List<Type> __LookupDataSources = App.Handlers.AssemblyHandler.GetTypesFromBaseInterface<ILookupDataSource>();
			__LookupDataSources.ForEach(__Type =>
			{
				ILookupDataSource __LookupDataSource = (ILookupDataSource)App.Factories.ObjectFactory.ResolveInstance(__Type);
				LookupDataSourceList.Add(__LookupDataSource);
			});

			List<Type> __DataSources = App.Handlers.AssemblyHandler.GetTypesFromBaseInterface<IDataSource>();
			__DataSources.ForEach(__Type =>
			{
				IDataSource __DataSource = (IDataSource)App.Factories.ObjectFactory.ResolveInstance(__Type);
				__DataSource.Init();
				DataSourceList.Add(__DataSource);
			});
		}


		public void FirtsRequestInit()
        {
            foreach (var __Item in DataSourceList)
            {
                __Item.SynchronizeColumnNames();
            }
        }


        public IDataSource GetDataSourceByID(DataSourceIDs _DataSourceID)
        {
            return DataSourceList.Find(__Item => __Item.DataSourceID.ID == _DataSourceID.ID);
        }

        public IDataSource GetDataSourceByDataSourceClientComponentName(string _ClientComponentName)
        {
            return DataSourceList.Find(__Item => __Item.DataSourceID.ClientComponentName == _ClientComponentName);
        }
    }
}
