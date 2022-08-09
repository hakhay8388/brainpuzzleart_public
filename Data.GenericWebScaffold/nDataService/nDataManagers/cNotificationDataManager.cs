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
using Base.Data.nDataService.nDatabase.nQuery.nWrappers.nRowNumber;

namespace Data.GenericWebScaffold.nDataService.nDataManagers
{
    public class cNotificationDataManager : cBaseDataManager
    {
        public cNotificationDataManager(cGenericWebScaffoldDataServiceContext _CoreServiceContext, IDataServiceManager _DataServiceManager, IFileDateService _FileDataService)
          : base(_CoreServiceContext, _DataServiceManager, _FileDataService)
        {
        }

        public cNotificationEntity AddNotification(List<cActorEntity> _ReceiverActors, ENotificationChannel _NotificationChannel, ENotificationType _ENotificationType, string _ParamObject,  DateTime _ValidUntilDate, bool _Broadcasted)
        {
            //cNotificationEntity

            IDataService __DataService = DataServiceManager.GetDataService();

            cNotificationEntity __NotificationEntity = __DataService.Database.CreateNew<cNotificationEntity>();
            __NotificationEntity.ChannelID = _NotificationChannel.ID;
            __NotificationEntity.Type = _ENotificationType.ID;
            __NotificationEntity.ParameterObjects = _ParamObject;
            __NotificationEntity.ValidUntilDate = _ValidUntilDate;
            __NotificationEntity.NotificationBroadcasted = _Broadcasted;
            __NotificationEntity.Save();

            _ReceiverActors.ForEach(__Item =>
            {
				cNotificationActorDetailEntity __NotificationActorDetailEntity = __DataService.Database.CreateNew<cNotificationActorDetailEntity>();
				__NotificationActorDetailEntity.Readed = false;
				__NotificationActorDetailEntity.Save(__NotificationEntity);
				__NotificationActorDetailEntity.Actor.SetValue(__Item);
            });

            return __NotificationEntity;
        }


		public cNotificationActorDetailEntity GetNotificationByIDAndActorID(long _ActorID, long _NotificationID)
		{
			IDataService __DataService = DataServiceManager.GetDataService();

			cNotificationActorDetailEntity __NotificationActorDetailAlias = null;


			cQuery<cNotificationActorDetailEntity> __Query = __DataService.Database.Query<cNotificationActorDetailEntity>(() => __NotificationActorDetailAlias)
				.SelectAll()
				.Where()
				.Operand<cNotificationEntity>().Eq(_NotificationID)
				.And
				.Exists
				(
					__DataService.Database.Query<cNotificationDetailToActorMapEntity>()
					.SelectID()
					.Where()
					.Operand<cNotificationActorDetailEntity>().Eq(() => __NotificationActorDetailAlias.ID)
					.And
					.Operand<cActorEntity>().Eq(_ActorID)
					.ToQuery()
				)
				.ToQuery();


			return __Query.ToList().FirstOrDefault();
		}

		public List<cNotificationEntity> GetMyNotification(cActorEntity _Actors, ENotificationChannel _NotificationChannel)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cNotificationEntity __NotificationAlias = null;
			cNotificationActorDetailEntity __NotificationActorDetailAlias = null;


			return __DataService.Database.Query<cNotificationEntity>(() => __NotificationAlias)
                .SelectAll()
                .Where()
                .Exists(
                    __DataService.Database.Query<cNotificationActorDetailEntity>(() => __NotificationActorDetailAlias)
                    .SelectID()
                    .Where()
                    .Operand<cNotificationEntity>().Eq(() => __NotificationAlias.ID)
                    .And
					.Exists(
						__DataService.Database.Query<cNotificationDetailToActorMapEntity>()
						.SelectID()
						.Where()
						.Operand<cNotificationActorDetailEntity>().Eq(() => __NotificationActorDetailAlias.ID)
						.And
						.Operand<cActorEntity>().Eq(_Actors.ID)
						.ToQuery()
					)
                    .ToQuery()
                )
                .And
                .Operand(__Item => __Item.ChannelID).Eq(_NotificationChannel.ID)
                .ToQuery()
                .OrderBy().Desc(__Item => __Item.CreateDate)
                .ToQuery()
                .ToList();
        }


        public List<dynamic> GetTopNotificationsForAllChannel(cActorEntity _Actors, int _TopNotificationCount, Action<dynamic> _Action)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            cNotificationEntity __NotificationAlias = null;
			cNotificationActorDetailEntity __NotificationActorDetailInnerAlias = null;
			cNotificationActorDetailEntity __NotificationActorDetailExternalAlias = null;


			cQuery<cNotificationEntity> __Query = __DataService.Database.Query<cNotificationEntity>(() => __NotificationAlias)
                .SelectAliasAllColumns(() => __NotificationAlias)
				.SelectAliasColumn<cNotificationActorDetailEntity>(() => __NotificationActorDetailExternalAlias, __Item => __Item.Readed)
				.Cross().Apply(

				__DataService.Database.Query<cNotificationActorDetailEntity>(() => __NotificationActorDetailInnerAlias)
					.SelectAll()
					.Where()
					.Operand<cNotificationEntity>().Eq(() => __NotificationAlias.ID)
					.And
					.Exists(
						__DataService.Database.Query<cNotificationDetailToActorMapEntity>()
						.SelectID()
						.Where()
						.Operand<cNotificationActorDetailEntity>().Eq(() => __NotificationActorDetailInnerAlias.ID)
						.And
						.Operand<cActorEntity>().Eq(_Actors.ID)
						.ToQuery()
					)
					.ToQuery()
				, () => __NotificationActorDetailExternalAlias)
				.EndApply()

                .Where()
                .Operand(__Item => __Item.ValidUntilDate).Ge(DateTime.Now)
                .ToQuery();
                            
            
            cRowNumber<cNotificationEntity> __RowNumber = __Query.RowNumber();
            string __GroupRowColumn = __RowNumber.RowNumberColumnName;

            __RowNumber.PartitionBy(__Item => __Item.ChannelID).OrderBy().Desc(__Item => __Item.CreateDate);


            return __DataService.Database.Query<cNotificationEntity>(__Query)
                   .SelectAll()
                   .Where()
                   .Operand(__GroupRowColumn).Le(_TopNotificationCount)
                   .ToQuery()
                   .OrderBy().Desc(__GroupRowColumn)
                   .ToQuery()
                   .ToDynamicObjectList(_Action);

        }

        public List<cNotificationEntity> GetNotBroadcasstedNotification()
        {
            IDataService __DataService = DataServiceManager.GetDataService();

            return __DataService.Database.Query<cNotificationEntity>()
                .SelectAll()
                .Where()
                .Operand(__Item => __Item.NotificationBroadcasted).Eq(false)
                .ToQuery()
                .ToList();
        }

        public int DeleteOldNotificationDate(DateTime _Date)
        {
            IDataService __DataService = DataServiceManager.GetDataService();

			cNotificationActorDetailEntity __NotificationActorDetailEntity = null;


			__DataService.Database.Delete<cNotificationDetailToActorMapEntity>()
                .Exists
                (
                    __DataService.Database.Query<cNotificationActorDetailEntity>(() => __NotificationActorDetailEntity)
                    .SelectID()
                    .Where()
                    .Operand(__Item => __Item.ID).Eq<cNotificationDetailToActorMapEntity, cNotificationActorDetailEntity>()
                    .And
					.Exists
					(
						__DataService.Database.Query<cNotificationEntity>()
						 .SelectID()	
						 .Where()
					
						 .Operand(__Item => __Item.ValidUntilDate).Lt(_Date)
						 .ToQuery()
					)
                    .ToQuery()
                )
                .ToQuery()
                .ExecuteForDeleteAndUpdate();

			__DataService.Database.Delete<cNotificationActorDetailEntity>()
					.Exists
					(
						__DataService.Database.Query<cNotificationEntity>()
						 .SelectID()
						 .Where()
						 .Operand(__Item => __Item.ValidUntilDate).Lt(_Date)
						 .ToQuery()
					)
				.ToQuery()
				.ExecuteForDeleteAndUpdate();


			return __DataService.Database.Delete<cNotificationEntity>()
                   .Operand(__Item => __Item.ValidUntilDate).Lt(_Date)
                   .ToQuery()
                   .ExecuteForDeleteAndUpdate();
        }
    }
}
