
using SF.Web.Control.JqGrid.Infrastructure.Enums;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.Subgrid
{
    /// <summary>
    /// Class which represents subgrid column for jqGrid.
    /// </summary>
    public struct JqGridSubgridColumnModel
    {
        #region Properties
        /// <summary>
        /// The name of the column.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The alignment of the column.
        /// </summary>
        public JqGridAlignments Alignment { get; private set; }

        /// <summary>
        /// The width of the column.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// The mapping for the column.
        /// </summary>
        public string Mapping { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates new jqGrid subgrid column.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="alignment">The alignment of the column.</param>
        /// <param name="width">The width of the column.</param>
        public JqGridSubgridColumnModel(string name, JqGridAlignments alignment, int width)
            : this(name, alignment, width, null)
        { }

        /// <summary>
        /// Creates new jqGrid subgrid column.
        /// </summary>
        /// <param name="name">The name of the column.</param>
        /// <param name="alignment">The alignment of the column.</param>
        /// <param name="width">The width of the column.</param>
        /// <param name="mapping">The mapping for the column.</param>
        public JqGridSubgridColumnModel(string name, JqGridAlignments alignment, int width, string mapping)
            : this()
        {
            Name = name;
            Alignment = alignment;
            Width = width;
            Mapping = mapping;
        }
        #endregion
    }
}
