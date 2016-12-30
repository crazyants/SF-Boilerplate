namespace SF.Web.Control.JqGrid.Core.Request
{
    /// <summary>
    /// Class which represents cell update request from jqGrid.
    /// </summary>
    public class JqGridCellUpdateRequest
    {
        #region Properties
        /// <summary>
        /// Gets or sets the record identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the cell to be updated.
        /// </summary>
        public string CellName { get; set; }

        /// <summary>
        /// Gets or sets the new value for cell to be updated.
        /// </summary>
        public object CellValue { get; set; }
        #endregion
    }
}
