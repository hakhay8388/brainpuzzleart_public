using Base.Data.nDataService.nDatabase.nEntity;
using Base.Data.nDataService.nDatabase.nEntity.nEntityTable;
using Base.Data.nDataService.nDatabase.nQuery.nGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.nDataService.nDatabase.nQuery.nQueryElements.nFilter.nFilterElements.nOperators
{
    public class cEqAny<TOwnerEntity, TEntity> : cBaseCompareOperator<TOwnerEntity, TEntity>
        where TOwnerEntity : cBaseEntity
        where TEntity : cBaseEntity
    {
        public cEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand)
        : base(_QueryFilterOperand, new object[] { })
        {
        }
        public cEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, params object[] _Value)
            : base(_QueryFilterOperand, _Value)
        {
        }

        public cEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, IQuery _Value)
            : base(_QueryFilterOperand, _Value)
        {
        }

        public cEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, params Expression<Func<object>>[] _PropertyExpression)
            : base(_QueryFilterOperand, _PropertyExpression)
        {
        }

        public cEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Type _Alias, Expression<Func<object>> _PropertyExpression)
          : base(_QueryFilterOperand, _Alias, _PropertyExpression)
        {
        }

        public cEqAny(cQueryFilterOperand<TOwnerEntity, TEntity> _QueryFilterOperand, Type _AliaslessEntityType, Type _ReleatedEntityType)
            : base(_QueryFilterOperand, _AliaslessEntityType, _ReleatedEntityType)
        {
        }

        public override string ToElementString(params object[] _Params)
        {
            if (IsConstValue)
            {
                string __Items = "";
                foreach (var __Item in Parameters)
                {
                    if (!string.IsNullOrEmpty(__Items)) __Items += ", ";
                    __Items += ":" + __Item.ParamName;
                }
                return QueryFilterOperand.FullName + " IN ( " + __Items + ") ";
            }
            else
            {
                string __Items = "";
                foreach (var __Item in Parameters)
                {
                    if (!string.IsNullOrEmpty(__Items)) __Items += " , ";
                    //__Items += "(" + __Item.ParamName + ")";
                    __Items += __Item.ParamName;
                }
                if (IsPlacedInParentheses)
                {
                    return QueryFilterOperand.FullName + " IN " + __Items;
                }
                else
                {
                    return QueryFilterOperand.FullName + " IN ( " + __Items + ") ";
                }

            }
        }
    }
}
