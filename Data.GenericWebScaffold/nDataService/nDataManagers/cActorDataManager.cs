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
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Data.Boundary.nData;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
	public class cActorDataManager : cBaseDataManager
	{
		public cActorDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
		  : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
		{
		}

		public cActorEntity GetActorBySellerDetailID(cActorType_SellerDetailEntity _SellerDetailEntity)
		{
			IDataService __DataService = DataServiceManager.GetDataService();

			cActorEntity __ActorAlias = null;
			return __DataService.Database.Query<cActorEntity>(() => __ActorAlias)
				.SelectAll()
				.Where()
				.Exists(
					__DataService.Database.Query<cActorType_SellerDetailEntity>()
					.SelectID()
					.Where()
					.Operand<cActorEntity>().Eq(() => __ActorAlias.ID)
					.And
					.Operand(__Item => __Item.ID).Eq(_SellerDetailEntity.ID)
					.ToQuery()
				)
				.ToQuery()
				.ToList()
				.FirstOrDefault();
		}
		public EUserVisibleGroups GetUserVisibleGroup(cActorEntity _ActorEntity)
        {
            if (_ActorEntity == null)
            {
				return EUserVisibleGroups.RealUser;
            }
            else
            {
				return EUserVisibleGroups.GetByID(_ActorEntity.UserVisibleGroup, EUserVisibleGroups.RealUser);
            }
        }

		public List<cMenuEntity> GetMenuByActor(cActorEntity _Actor, string _MenuTypeCode, string _RootMenuCode)
		{
			cQuery<cMenuEntity> __Query = GetMenuByActorQuery(_Actor, _MenuTypeCode, _RootMenuCode);
			return __Query.ToList();
		}
		
		public List<dynamic> GetMenuByActorToDynamicList(cActorEntity _Actor, string _MenuTypeCode, string _RootMenuCode, Action<dynamic> _Action)
		{
			cQuery<cMenuEntity> __Query = GetMenuByActorQueryWithPage(_Actor, _MenuTypeCode, _RootMenuCode);
			return __Query.ToDynamicObjectList(_Action);
		}

		private cQuery<cMenuEntity> GetMenuByActorQueryWithPage(cActorEntity _Actor, string _MenuTypeCode, string _RootMenuCode)
		{
			List<cMenuEntity> __Result = new List<cMenuEntity>();
			IDataService __DataService = DataServiceManager.GetDataService();
			cActorEntity __ActorAlias = null;
			cActorRoleMapEntity __ActorRoleMapAlias = null;
			cRoleEntity __RoleAlias = null;
			cRoleMenuMapEntity __RoleMenuMapAlias = null;
			cMenuEntity __MenuAlias = null;
			cRoleMenuEntity __RoleMenuAlias = null;
			cPageEntity __PageAlias = null;
			cMenuPageMapEntity __MenuPageAlias = null;

			cBaseFilter<cMenuEntity, cMenuEntity> __Fileter = __DataService.Database
				.Query<cMenuEntity>(() => __MenuAlias)
				.SelectAliasAllColumns<cMenuEntity>(() => __MenuAlias)
				.SelectAliasColumn<cPageEntity>(() => __PageAlias, __Item => __Item.Url)
				.Inner<cRoleMenuEntity>().Join(() => __RoleMenuAlias)
				.On()
				.Operand<cMenuEntity>(() => __MenuAlias, __Item => __Item.ID).Eq<cMenuEntity>(() => __RoleMenuAlias)
				.ToQuery()
				.Inner<cMenuPageMapEntity>()
				.Join(()=>__MenuPageAlias)
				.On()
				.Operand<cMenuEntity>(() => __MenuAlias, __Item => __Item.ID)
				.Eq<cMenuEntity>(() => __MenuPageAlias)
				.ToQuery()
				.Inner<cPageEntity>()
				.Join(()=>__PageAlias)
				.On()
				.Operand<cPageEntity>(() => __PageAlias, __Item => __Item.ID)
				.Eq<cPageEntity>(() => __MenuPageAlias)
				.ToQuery()
				.Where()
				.Exists(
					__DataService.Database.Query<cRoleMenuMapEntity>(() => __RoleMenuMapAlias)
						.SelectID()
						.Where()
						.Operand<cMenuEntity>().Eq(() => __MenuAlias.ID)
						.And
						.Operand(() => __RoleMenuAlias, "RoleID").Eq<cRoleEntity>(() => __RoleMenuMapAlias)
						.And
						.Exists(
							__DataService.Database.Query<cRoleEntity>(() => __RoleAlias)
								.SelectID()
								.Where()
								.Operand(__Item => __Item.ID).Eq(() => __RoleMenuMapAlias)
								.And
								.Exists
								(
									__DataService.Database.Query<cActorRoleMapEntity>(() => __ActorRoleMapAlias)
										.SelectID()
										.Where()
										.Operand<cRoleEntity>().Eq(() => __RoleAlias.ID)
										.And
										.Exists
										(
											__DataService.Database.Query<cActorEntity>(() => __ActorAlias)
												.SelectID()
												.Where()
												.Operand(__Item => __Item.ID).Eq(() => __ActorRoleMapAlias)
												.And
												.Operand(__Item => __Item.ID).Eq(_Actor.ID)
												.ToQuery()
										)
										.ToQuery()
								)
								.ToQuery()
						)
						.ToQuery()
				);

			cQuery<cMenuEntity> __Query = __Fileter.ToQuery();

			if (_RootMenuCode.IsNullOrEmpty())
			{
				__Query = __Fileter.And.Operand(__Item => __Item.MenuTypeCode).Eq(_MenuTypeCode)
					.And
					.Operand(__Item => __Item.RootMenu).IsNull()
					.ToQuery();
			}
			else
			{
				__Query = __Fileter.And.Operand(__Item => __Item.MenuTypeCode).Eq(_MenuTypeCode)
					.And
					.Operand(__Item => __Item.RootMenu).Eq(
						__DataService.Database.Query<cMenuEntity>().SelectID().Where().Operand(__Item => __Item.Code)
							.Eq(_RootMenuCode).ToQuery()
					)
					.ToQuery();
			}


			return __Query
				.OrderBy()
				.Asc<cRoleMenuEntity>(() => __RoleMenuAlias, "SortValue")
				.ToQuery();
		}


		private cQuery<cMenuEntity> GetMenuByActorQuery(cActorEntity _Actor, string _MenuTypeCode, string _RootMenuCode)
		{
			List<cMenuEntity> __Result = new List<cMenuEntity>();
			IDataService __DataService = DataServiceManager.GetDataService();

			/*__Result = __DataService.Database.Query<cPageEntity>().Distinct().SelectAll()
                .Where()
                .Operand(__Item => __Item.ID).EqAny
                (
                    __DataService.Database.Query<cRolePageMapEntity>().SelectColumn<cPageEntity>().Where().Operand<cRoleEntity>().EqAny
                    (
                        __DataService.Database.Query<cRoleEntity>().SelectID().Where().Operand(__Item => __Item.ID).EqAny
                         (
                            __DataService.Database.Query<cActorRoleMapEntity>().SelectColumn<cRoleEntity>().Where().Operand<cActorEntity>().Eq(_Actor.ID).ToQuery()
                         ).ToQuery()
                    ).ToQuery()
                )
            .ToQuery()
            .ToList();*/


			cActorEntity __ActorAlias = null;
			cActorRoleMapEntity __ActorRoleMapAlias = null;
			cRoleEntity __RoleAlias = null;
			cRoleMenuMapEntity __RoleMenuMapAlias = null;
			cMenuEntity __MenuAlias = null;
			cRoleMenuEntity __RoleMenuAlias = null;

			cBaseFilter<cMenuEntity, cMenuEntity> __Fileter = __DataService.Database.Query<cMenuEntity>(() => __MenuAlias)
			 .SelectAliasAllColumns<cMenuEntity>(() => __MenuAlias)
				.Inner<cRoleMenuEntity>().Join(() => __RoleMenuAlias)
					  .On()
					  .Operand<cMenuEntity>(() => __MenuAlias, __Item => __Item.ID).Eq<cMenuEntity>(() => __RoleMenuAlias)
					  .ToQuery()
				.Where()
				.Exists(
					 __DataService.Database.Query<cRoleMenuMapEntity>(() => __RoleMenuMapAlias)
					 .SelectID()
					 .Where()
					 .Operand<cMenuEntity>().Eq(() => __MenuAlias.ID)
					 .And
					 .Operand(() => __RoleMenuAlias, "RoleID").Eq<cRoleEntity>(() => __RoleMenuMapAlias)
					 .And
					 .Exists(
						 __DataService.Database.Query<cRoleEntity>(() => __RoleAlias)
						 .SelectID()
						 .Where()
						 .Operand(__Item => __Item.ID).Eq(() => __RoleMenuMapAlias)
						 .And
						 .Exists
						 (
							 __DataService.Database.Query<cActorRoleMapEntity>(() => __ActorRoleMapAlias)
							 .SelectID()
							 .Where()
							 .Operand<cRoleEntity>().Eq(() => __RoleAlias.ID)
							 .And
							 .Exists
							 (
									__DataService.Database.Query<cActorEntity>(() => __ActorAlias)
									 .SelectID()
									 .Where()
									 .Operand(__Item => __Item.ID).Eq(() => __ActorRoleMapAlias)
									 .And
									 .Operand(__Item => __Item.ID).Eq(_Actor.ID)
									 .ToQuery()
							 )
							 .ToQuery()
						  )

						 .ToQuery()
					  )
					 .ToQuery()
				);

			cQuery<cMenuEntity> __Query = __Fileter.ToQuery();

			if (_RootMenuCode.IsNullOrEmpty())
			{
				__Query = __Fileter.And.Operand(__Item => __Item.MenuTypeCode).Eq(_MenuTypeCode)
				.And
				.Operand(__Item => __Item.RootMenu).IsNull()
				.ToQuery();
			}
			else
			{
				__Query = __Fileter.And.Operand(__Item => __Item.MenuTypeCode).Eq(_MenuTypeCode)
				.And
				.Operand(__Item => __Item.RootMenu).Eq(
				   __DataService.Database.Query<cMenuEntity>().SelectID().Where().Operand(__Item => __Item.Code).Eq(_RootMenuCode).ToQuery()
				)
				.ToQuery();
			}




			return __Query
				.OrderBy()
				.Asc<cRoleMenuEntity>(() => __RoleMenuAlias, "SortValue")
				.ToQuery();


			/*List<cRoleEntity> __RoleList = _Actor.Roles.ToList();
            for (int i = 0; i < __RoleList.Count;i++)
            {
                List<cPageEntity> __RolePages = __RoleList[i].Pages.ToList();
                
                __Result = __Result.Union(__RolePages).ToList();
            }

			return __Result;*/
		}


		public List<cPageEntity> GetPageByActor(cActorEntity _Actor)
		{
			List<cPageEntity> __Result = new List<cPageEntity>();
			IDataService __DataService = DataServiceManager.GetDataService();

			cActorEntity __ActorAlias = null;
			cActorRoleMapEntity __ActorRoleMapAlias = null;
			cRoleEntity __RoleAlias = null;
			cRolePageMapEntity __RolePageMapAlias = null;
			cPageEntity __PageAlias = null;
			cPageEntity __WrappedPageAlias = null;
			cMenuEntity __MenuAlias = null;
			cMenuEntity __MenuExternalAlias = null;

			cQuery<cPageEntity> __Query = __DataService.Database.Query<cPageEntity>(() => __PageAlias)
			   .SelectAliasAllColumns<cPageEntity>(() => __PageAlias)
			   .Case("SortValue")
					.When()
					.Operand<cMenuEntity>(() => __MenuExternalAlias, __Item => __Item.SortValue).IsNotNull()
					.Then()
					.SelectAliasColumn<cMenuEntity>(() => __MenuExternalAlias, __Item => __Item.SortValue)
					.Else()
					.SelectValue(9999999)
					.ToQuery()

			   .Outer().Apply(
				   __DataService.Database.Query<cMenuEntity>(() => __MenuAlias)
				   .SelectColumn(__Item => __Item.SortValue)
				   .Where()
				   .Exists(
					   __DataService.Database.Query<cMenuPageMapEntity>()
					   .SelectID()
					   .Where()
					   .Operand<cMenuEntity>().Eq(() => __MenuAlias.ID)
					   .And
					   .Operand<cPageEntity>().Eq(() => __PageAlias.ID)
					   .ToQuery()
				   )
				   .ToQuery()
			   , () => __MenuExternalAlias)
			   .EndApply()

			   .Where()
			   .Exists(
					__DataService.Database.Query<cRolePageMapEntity>(() => __RolePageMapAlias)
					.SelectID()
					.Where()
					.Operand<cPageEntity>().Eq(() => __PageAlias.ID)
					.And
					.Exists(
						__DataService.Database.Query<cRoleEntity>(() => __RoleAlias)
						.SelectID()
						.Where()
						.Operand(__Item => __Item.ID).Eq(() => __RolePageMapAlias)
						.And
						.Exists
						(
							__DataService.Database.Query<cActorRoleMapEntity>(() => __ActorRoleMapAlias)
							.SelectID()
							.Where()
							.Operand<cRoleEntity>().Eq(() => __RoleAlias.ID)
							.And
							.Exists
							(
								   __DataService.Database.Query<cActorEntity>(() => __ActorAlias)
									.SelectID()
									.Where()
									.Operand(__Item => __Item.ID).Eq(() => __ActorRoleMapAlias)
									.And
									.Operand(__Item => __Item.ID).Eq(_Actor.ID)
									.ToQuery()
							)
							.ToQuery()
						 )

						.ToQuery()
					 )
					.ToQuery()
			   )
			   .ToQuery();

			cQuery<cPageEntity> __WrappedQuery = __DataService.Database.Query<cPageEntity>(() => __WrappedPageAlias, __Query)
				.SelectAll()
				.Where()
				.ToQuery()
				.OrderBy().Asc<cPageEntity>(() => __WrappedPageAlias, "SortValue")
			    .ToQuery();


			__Result = __WrappedQuery.ToList();


			return __Result;
		}
	}
}
