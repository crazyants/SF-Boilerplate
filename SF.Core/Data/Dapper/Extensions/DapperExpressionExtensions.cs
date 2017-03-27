using System;
using System.Linq.Expressions;
using DapperExtensions;
using SF.Entitys.Abstraction;

namespace SF.Core.Dapper.Expressions
{
    internal static class DapperExpressionExtensions
    {
     
        public static IPredicate ToPredicateGroup<TEntity, TPrimaryKey>( this Expression<Func<TEntity, bool>> expression) where TEntity :  BaseEntity<TPrimaryKey>
        {
          
            var dev = new DapperExpressionVisitor<TEntity, TPrimaryKey>();
            IPredicate pg = dev.Process(expression);

            return pg;
        }
    }
}
