using Base.Data.nDataService.nDatabase.nEntity;
using Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources.nDataSourceFieldTypes.nValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.GenericWebScaffold.nWebGraph.nComponentManager.nDataSourcesManager.nDataSources
{
    public class cDataSourceFieldTypeProps<TEntity> : cBaseDataSourceFieldTypeProps
        where TEntity : cBaseEntity
    {
        public cBaseListDataSource<TEntity> OwnerDataSource { get; set; }
        public Expression<Func<TEntity, object>> ColumnName_PropertyExpressions { get; set; }
        public LambdaExpression RelatedColumnName_PropertyExpressions { get; private set; }

        public cDataSourceFieldTypeProps()
            :base()
        {
        }

        public void SetRelatedColumnName<TRelatedEntity>(Expression<Func<TRelatedEntity, object>> _RelatedColumn) where TRelatedEntity : cBaseEntity
        {
            RelatedColumnName_PropertyExpressions = _RelatedColumn;
        }
    }
}
