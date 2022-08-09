using Base.Data.nDataService;
using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nQuery;
using Base.Data.nDataService.nDatabase.nQuery.nQueryDemonstratorInterfaces;
using Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes
{
    public abstract class cMappedBaseFieldType<TEntity, TMapEntity, TMappedEntity> : cBaseDataSourceFieldType<TEntity, TMappedEntity>
        where TEntity : cBaseEntity
        where TMapEntity : cBaseEntity
        where TMappedEntity : cBaseEntity
    {
        protected Expression<Func<TMappedEntity>> MappedAlias { get; set; }
        protected Expression<Func<TMappedEntity, object>> RelatedColumn { get; set; }

        public cMappedBaseFieldType(cDataSourceFieldTypeProps<TEntity> _Props)
            : base(_Props)
        {
            RelatedColumn = (Expression<Func<TMappedEntity, object>>)_Props.RelatedColumnName_PropertyExpressions;
        }

        public override void AddColumn(cQuery<TEntity> _Query, Expression<Func<TMappedEntity>> _Alias)
        {
            MappedAlias = _Alias;
            _Query.SelectAliasColumn(_Alias, RelatedColumn, Props.ColumnAs);            
        }
        public override void AddIDColumn(List<IAliasMatcher<TEntity>> _IDFieldList, Expression<Func<TMappedEntity>> _Alias)
        {
            IDField = Props.OwnerDataSource.GetMappedIDField<TMapEntity, TMappedEntity>(_IDFieldList, _Alias);
        }

        public override void AddJoin(cQuery<TEntity> _Query, Expression<Func<object>> _MainAlias, Expression<Func<TMappedEntity>> _Alias)
        {
            TMapEntity __MapEntity = null;
            if (Props.InnerJoin)
            {
                _Query.Inner<TMapEntity>().Join(() => __MapEntity).On().Operand<TEntity>().Eq(_MainAlias);
                _Query.Inner<TMappedEntity>().Join(_Alias).On().Operand(__Item => __Item.ID).Eq(() => __MapEntity);
            }
            else
            {
                _Query.Left<TMapEntity>().Join(() => __MapEntity).On().Operand<TEntity>().Eq(_MainAlias);
                _Query.Left<TMappedEntity>().Join(_Alias).On().Operand(__Item => __Item.ID).Eq(() => __MapEntity);
            }
        }

        public override string GetFullName()
        {
            return GetColumnRoot() + "." + ColumnName;
        }

        public override void Update(long _ID, object _Value)
        {
            IDataService __DataService = Props.OwnerDataSource.DataServiceManager.GetDataService();
            TMappedEntity __Entity = __DataService.Database.GetEntityByID<TMappedEntity>(_ID);
            __Entity.GetType().GetProperty(ColumnName).GetSetMethod().Invoke(__Entity, new object[] { _Value });
            __Entity.Save();

        }

    }
}
