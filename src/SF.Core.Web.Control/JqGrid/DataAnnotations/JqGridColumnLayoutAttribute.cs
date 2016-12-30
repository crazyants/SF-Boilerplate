using System;
using SF.Web.Control.JqGrid.Infrastructure.Enums;
using SF.Web.Control.JqGrid.Infrastructure.Constants;

namespace SF.Web.Control.JqGrid.DataAnnotations
{
    /// <summary>
    /// Specifies the layout attributes of grid column
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class JqGridColumnLayoutAttribute : Attribute
    {
        #region Properties
        /// <summary>
        /// Gets or sets the alignment of the cell in the grid body layer.
        /// </summary>
        public JqGridAlignments Alignment { get; set; }

        /// <summary>
        /// Gets or sets the function which can add attributes to the cell during the creation of the data (dynamically).
        /// </summary>
        public string CellAttributes { get; set; }

        /// <summary>
        /// Gets or sets additional CSS classes for the column (separated by space).
        /// </summary>
        public string Classes { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if internal recalculation of the width of the column is disabled (default false).
        /// </summary>
        public bool Fixed { get; set; }

        /// <summary>
        /// Gets or sets the value indicating if column shouldn't scroll out of view when user is moving horizontally across the grid.
        /// </summary>
        public bool Frozen { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if column will appear in the modal dialog where users can choose which columns to show or hide.
        /// </summary>
        public bool HideInDialog { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if column can be resized (default true).
        /// </summary>
        public bool Resizable { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if the title should be displayed in the column when user hovers a cell with the mouse.
        /// </summary>
        public bool Title { get; set; }

        /// <summary>
        /// Gets or sets the initial width in pixels of the column (default 150).
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the value which defines if the column should appear in view form.
        /// </summary>
        public bool Viewable { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridColumnLayoutAttribute class.
        /// </summary>
        public JqGridColumnLayoutAttribute()
        {
            Alignment = JqGridOptionsDefaults.ColumnModel.Alignment;
            CellAttributes = null;
            Classes = null;
            Fixed = JqGridOptionsDefaults.ColumnModel.Fixed;
            Frozen = JqGridOptionsDefaults.ColumnModel.Frozen;
            HideInDialog = JqGridOptionsDefaults.ColumnModel.HideInDialog;
            Resizable = JqGridOptionsDefaults.ColumnModel.Resizable;
            Title = JqGridOptionsDefaults.ColumnModel.Title;
            Width = JqGridOptionsDefaults.ColumnModel.Width;
            Viewable = JqGridOptionsDefaults.ColumnModel.Viewable;
        }
        #endregion
    }
}
