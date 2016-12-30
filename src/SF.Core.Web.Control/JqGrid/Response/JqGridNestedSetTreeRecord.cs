using System.Collections.Generic;

namespace SF.Web.Control.JqGrid.Core.Response
{
    /// <summary>
    /// Class which represents TreeGrid record for jqGrid in nested set model.
    /// </summary>
    public class JqGridNestedSetTreeRecord : JqGridTreeRecord
    {
        #region Properties
        /// <summary>
        /// Gets the rowid of the record to the left.
        /// </summary>
        public int LeftField { get; private set; }

        /// <summary>
        /// Gets the rowid of the record to the right.
        /// </summary>
        public int RightField { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridNestedSetTreeRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="values">The record cells values.</param>
        /// <param name="level">The level of the record in the hierarchy.</param>
        /// <param name="leftField">The rowid of the record to the left.</param>
        /// <param name="rightField">The rowid of the record to the right.</param>
        public JqGridNestedSetTreeRecord(string id, IList<object> values, int level, int leftField, int rightField)
            : base(id, values, level)
        {
            LeftField = leftField;
            RightField = rightField;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridNestedSetTreeRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="value">The record value.</param>
        /// <param name="level">The level of the record in the hierarchy.</param>
        /// <param name="leftField">The rowid of the record to the left.</param>
        /// <param name="rightField">The rowid of the record to the right.</param>
        public JqGridNestedSetTreeRecord(string id, object value, int level, int leftField, int rightField)
            : base(id, value, level)
        {
            LeftField = leftField;
            RightField = rightField;
        }
        #endregion
    }
}
