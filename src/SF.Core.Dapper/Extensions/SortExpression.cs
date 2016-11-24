using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Dapper.Extensions
{
    /// <summary>
    /// Represents Sort Direction
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// Represents Ascending Order
        /// </summary>
        Ascending,

        /// <summary>
        /// Represents Descending Order
        /// </summary>
        Descending
    }

    public class SortExpression<TEntity>
    {
        public SortExpression(Expression<Func<TEntity, object>> sortBy, SortDirection sortDirection)
        {
            this.SortBy = sortBy;
            this.SortDirection = sortDirection;
        }

        public Expression<Func<TEntity, object>> SortBy { get; set; }

        public SortDirection SortDirection { get; set; }
    }
}
