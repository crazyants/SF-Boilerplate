using System.Collections.Generic;

namespace SF.Web.Control.JqGrid.Core.Response
{
    /// <summary>
    /// Class which represents TreeGrid record for jqGrid in adjacency model.
    /// </summary>
    public class JqGridAdjacencyTreeRecord : JqGridTreeRecord
    {
        #region Properties
        /// <summary>
        /// Gets the id of parent of this record.
        /// </summary>
        public string ParentId { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridAdjacencyTreeRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="values">The record cells values.</param>
        /// <param name="level">The level of the record in the hierarchy.</param>
        /// <param name="parentId">The id of parent of this record.</param>
        public JqGridAdjacencyTreeRecord(string id, IList<object> values, int level, string parentId)
            : base(id, values, level)
        {
            ParentId = parentId;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridAdjacencyTreeRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="value">The record value.</param>
        /// <param name="level">The level of the record in the hierarchy.</param>
        /// <param name="parentId">The id of parent of this record.</param>
        public JqGridAdjacencyTreeRecord(string id, object value, int level, string parentId)
            : base(id, value, level)
        {
            ParentId = parentId;
        }
        #endregion
    }
}
