using SF.Web.Control.JqGrid.Infrastructure.Options;

namespace SF.Web.Control.JqGrid.Helper.Options
{
    /// <summary>
    /// jqGrid options
    /// </summary>
    /// <typeparam name="TModel">Type of model for grid</typeparam>
    public sealed class JqGridOptions<TModel> : JqGridOptions
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the JqGridOptions class.
        /// </summary>
        /// <param name="id">Identifier, which will be used for table (id='{0}'), pager div (id='{0}Pager'), filter grid div (id='{0}Search') and in JavaScript.</param>
        public JqGridOptions(string id)
            : base(id)
        { }
        #endregion
    }
}
