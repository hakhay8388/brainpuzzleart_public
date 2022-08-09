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
    public abstract class cRelationalBaseFieldType<TEntity, TRelationalEntity> : cBaseDataSourceFieldType<TEntity, TRelationalEntity>
        where TEntity : cBaseEntity
        where TRelationalEntity : cBaseEntity
    {
        protected Expression<Func<TRelationalEntity>> RelatedAlias { get; set; }

        protected Expression<Func<TRelationalEntity, object>> RelatedColumn { get; set; }
        public cRelationalBaseFieldType(cDataSourceFieldTypeProps<TEntity> _Props)
            :base(_Props)
        {
            RelatedColumn = (Expression<Func<TRelationalEntity, object>>)_Props.RelatedColumnName_PropertyExpressions;
        }

        public override void AddColumn(cQuery<TEntity> _Query, Expression<Func<TRelationalEntity>> _Alias)
        {
            RelatedAlias = _Alias;
            _Query.SelectAliasColumn(_Alias, RelatedColumn, Props.ColumnAs);
        }
        public override void AddIDColumn(List<IAliasMatcher<TEntity>> _IDFieldList, Expression<Func<TRelationalEntity>> _Alias)
        {
            IDField = Props.OwnerDataSource.GetRelatedIDField<TRelationalEntity>(_IDFieldList, _Alias);
        }
        public override void AddJoin(cQuery<TEntity> _Query, Expression<Func<object>> _MainAlias, Expression<Func<TRelationalEntity>> _Alias)
        {
            if (Props.InnerJoin)
            {
                _Query.Inner<TRelationalEntity>().Join(_Alias).On().Operand<TEntity>().Eq(_MainAlias);
            }
            else
            {
                _Query.Left<TRelationalEntity>().Join(_Alias).On().Operand<TEntity>().Eq(_MainAlias);
            }            
        }

        public override string GetFullName()
        {
            return GetColumnRoot() + "." + ColumnName;
        }

        public override void Update(long _ID, object _Value)
        {
            IDataService __DataService = Props.OwnerDataSource.DataServiceManager.GetDataService();
            TRelationalEntity __Entity = __DataService.Database.GetEntityByID<TRelationalEntity>(_ID);
            __Entity.GetType().GetProperty(ColumnName).GetSetMethod().Invoke(__Entity, new object[] { _Value });
            __Entity.Save();

        }
    }
}
