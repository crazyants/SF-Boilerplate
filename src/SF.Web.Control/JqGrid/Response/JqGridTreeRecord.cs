using System.Collections.Generic;

namespace SF.Web.Control.JqGrid.Core.Response
{
    /// <summary>
    /// Abstract class which represents TreeGrid record for jqGrid.
    /// </summary>
    public abstract class JqGridTreeRecord : JqGridRecord
    {
        #region Properties
        /// <summary>
        /// Gets or set the level of the record in the hierarchy.
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        /// Gets or sets value wich defines if the record is leaf (default false).
        /// </summary>
        public bool Leaf { get; set; }

        /// <summary>
        /// Gets or sets the value which defines whether this element should be expanded during the loading (default false).
        /// </summary>
        public bool Expanded { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridTreeRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="values">The record cells values.</param>
        /// <param name="level">The level of the record in the hierarchy.</param>
        public JqGridTreeRecord(string id, IList<object> values, int level)
            : base(id, values)
        {
            Level = level;
            Leaf = false;
            Expanded = false;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridTreeRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="value">The record value.</param>
        /// <param name="level">The level of the record in the hierarchy.</param>
        public JqGridTreeRecord(string id, object value, int level)
            : base(id, value)
        {
            Level = level;
            Leaf = false;
            Expanded = false;
        }
        #endregion
    }
}
