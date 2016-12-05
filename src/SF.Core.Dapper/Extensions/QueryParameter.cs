using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Dapper.Extensions
{
    /// <summary>
    /// Class that models the data structure in covert the expression tree into SQL and Parameters.
    /// </summary>
    internal class QueryParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryParameter" /> class.
        /// </summary>
        /// <param name="linkingOperator">linkingOperator parameter</param>
        /// <param name="propertyName">propertyName parameter</param>
        /// <param name="propertyValue">propertyValue parameter</param>
        /// <param name="queryOperator">queryOperator parameter</param>
        internal QueryParameter(string linkingOperator, string propertyName, object propertyValue, string queryOperator)
        {
            this.LinkingOperator = linkingOperator;
            this.PropertyName = propertyName;
            this.PropertyValue = propertyValue;
            this.QueryOperator = queryOperator;
        }

        public string LinkingOperator { get; set; }

        public string PropertyName { get; set; }

        public object PropertyValue { get; set; }

        public string QueryOperator { get; set; }
    }
}
