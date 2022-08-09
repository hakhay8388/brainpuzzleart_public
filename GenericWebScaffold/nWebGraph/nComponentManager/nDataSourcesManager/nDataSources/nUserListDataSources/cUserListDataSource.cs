using Base.Core.nApplication;
using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Base.Data.nDataService.nDatabase.nSql;
using Base.Data.nDataServiceManager;
using Core.GenericWebScaffold.Controllers;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nEnumDataSources;
using Core.GenericWebScaffold.nWebGraph.nSessionManager;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_CreateCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_DeleteCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetMetaDataCommand; 
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_GetSettingsCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_ReadCommand;
using Core.GenericWebScaffold.nWebGraph.nWebApiGraph.nCommandGraph.nCommands.nDataSource_UpdateCommand;
using Data.GenericWebScaffold.nDataService.nDataManagers;
using Data.GenericWebScaffold.nDataService.nEntityServices.nEntities;
using Data.GenericWebScaffold.nDefaultValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nUserListDataSources
{
    public class cUserListDataSource : cBaseListDataSource<cUserEntity>
    {
        cRoleDataManager RoleDataManager { get; set; }

        cUserStateDataSource UserStateDataSource { get; set; }
        cUserDetailEntity m_UserDetailAlias { get; set; }
        cActorEntity m_ActorAlias { get; set; }

        public cUserListDataSource(cApp _App, IDataServiceManager _DataServiceManager, cDataSourceManager _DataSourceManager,
            cDataSourceDataManager _DataSourceDataManager,
            cRoleDataManager _RoleDataManager,
            cWebGraph _WebGraph,
            cUserStateDataSource _UserStateDataSource)
            : base(DataSourceIDs.UserList, _App, _WebGraph, _DataServiceManager, _DataSourceManager, _DataSourceDataManager)
        {
            RoleDataManager = _RoleDataManager;
        }


        public void InitMainFileds()
        {
            /////////////////////////////////////////////////////////////////////////////////////////////////////

            /*            cDataSourceFieldTypeProps<cUserEntity> __Props = new cDataSourceFieldTypeProps<cUserEntity>();
                        __Props.OwnerDataSource = this;
                        __Props.ColumnName_PropertyExpressions = __Item => __Item.ID;
                        __Props.Editable = ColumnEditableIDs.Never;
                        __Props.Visible = false;
                        __Props.OrderFromLeft = 2;

                        cBaseDataSourceFieldType<cUserEntity, cUserEntity> __Field = new cMainNumericFieldType<cUserEntity>(__Props);
                        FieldList.Add(new cAliasMatcher<cUserEntity, cUserEntity>(() => m_MainAlias.ID, () => m_MainAlias, __Field));*/

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            cDataSourceFieldTypeProps<cUserEntity> __Props = new cDataSourceFieldTypeProps<cUserEntity>();
            __Props.OwnerDataSource = this;
            __Props.ColumnName_PropertyExpressions = __Item => __Item.Email;
            __Props.Editable = ColumnEditableIDs.Never;
            __Props.Title = "Email";
            __Props.ElasticSearch = true;
            //__Props.ColumnAs = "Deneme";
            __Props.OrderFromLeft = 1;

            cBaseDataSourceFieldType<cUserEntity, cUserEntity> __Field = new cMainStringFieldType<cUserEntity>(__Props);
            FieldList.Add(new cAliasMatcher<cUserEntity, cUserEntity>(() => m_MainAlias.ID, () => m_MainAlias, __Field));

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            __Props = new cDataSourceFieldTypeProps<cUserEntity>();
            __Props.OwnerDataSource = this;
            __Props.ColumnName_PropertyExpressions = __Item => __Item.State;
            __Props.Editable = ColumnEditableIDs.Always;
            __Props.Title = "Status";
            __Props.OrderFromLeft = 3;
            __Props.LookUpDataSource = new cUserStateLookUpDataSource();

            __Field = new cMainStringFieldType<cUserEntity>(__Props);
            FieldList.Add(new cAliasMatcher<cUserEntity, cUserEntity>(() => m_MainAlias.ID, () => m_MainAlias, __Field));
        }


        public void InitRelatedField()
        {
            cDataSourceFieldTypeProps<cUserEntity> __Props = new cDataSourceFieldTypeProps<cUserEntity>();
            __Props.OwnerDataSource = this;
            __Props.Editable = ColumnEditableIDs.Always;
            __Props.Title = "Telephone";
            __Props.SetRelatedColumnName<cUserDetailEntity>(__Item => __Item.Telephone);
            __Props.ElasticSearch = true;
            __Props.InnerJoin = true;
            __Props.OrderFromLeft = 4;

            cBaseDataSourceFieldType<cUserEntity, cUserDetailEntity> __Field = new cRelationalStringFieldType<cUserEntity, cUserDetailEntity>(__Props);
            FieldList.Add(new cAliasMatcher<cUserEntity, cUserDetailEntity>(() => m_MainAlias.ID, () => m_UserDetailAlias, __Field));


        }

        public void InitMappedField()
        {
            cDataSourceFieldTypeProps<cUserEntity> __Props = new cDataSourceFieldTypeProps<cUserEntity>();
            __Props.OwnerDataSource = this;
            __Props.Editable = ColumnEditableIDs.Always;
            __Props.Title = "ActorName";
            __Props.SetRelatedColumnName<cActorEntity>(__Item => __Item.Name);
            __Props.ElasticSearch = true;
            __Props.InnerJoin = true;
            __Props.OrderFromLeft = 0;

            cBaseDataSourceFieldType<cUserEntity, cActorEntity> __Field = new cMappedStringFieldType<cUserEntity, cUserActorMapEntity, cActorEntity>(__Props);
            FieldList.Add(new cAliasMatcher<cUserEntity, cActorEntity>(() => m_MainAlias.ID, () => m_ActorAlias, __Field));
        }


        public override void Init()
        {
            InitMainFileds();
            InitRelatedField();
            InitMappedField();
        }


        public override void ReceiveDataSource_CreateData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_CreateCommandData _ReceivedData)
        {
            //throw new NotImplementedException();
        }

        public override void ReceiveDataSource_DeleteData(cListenerEvent _ListenerEvent, IController _Controller, cDataSource_DeleteCommandData _ReceivedData)
        {
            //throw new NotImplementedException();
        }

    }
}
