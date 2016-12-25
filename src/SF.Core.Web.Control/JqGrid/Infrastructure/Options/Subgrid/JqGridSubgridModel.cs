using System;
using System.Collections.Generic;

namespace SF.Web.Control.JqGrid.Infrastructure.Options.Subgrid
{
    /// <summary>
    /// Class which represents subgrid model for jqGrid.
    /// </summary>
    public class JqGridSubgridModel
    {
        #region Fields
        private readonly IList<JqGridSubgridColumnModel> _columnsModels = new List<JqGridSubgridColumnModel>();
        private readonly IList<string> _parameters = new List<string>();
        #endregion

        #region Properties
        /// <summary>
        /// Gets the list of columns parameters descriptions.
        /// </summary>
        public IReadOnlyList<JqGridSubgridColumnModel> ColumnsModels { get { return (IReadOnlyList<JqGridSubgridColumnModel>)_columnsModels; } }

        /// <summary>
        /// The names from main grid's column model to pass as additional parameter to the sub grid url. 
        /// </summary>
        public IList<string> Parameters { get { return _parameters; } }
        #endregion

        #region Methods
        /// <summary>
        /// Adds column to model.
        /// </summary>
        /// <param name="columnModel">The column model.</param>
        /// <exception cref="System.ArgumentNullException">
        /// The column name haven't been provided. 
        /// </exception>
        public void AddColumn(JqGridSubgridColumnModel columnModel)
        {
            if (String.IsNullOrWhiteSpace(columnModel.Name))
            {
                throw new ArgumentNullException("columnModel.Name");
            }
            
            _columnsModels.Add(columnModel);
        }
        #endregion
    }
}
