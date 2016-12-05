using System;
using System.Collections.Generic;
using SF.Web.Control.JqGrid.Infrastructure.Options;

namespace SF.Web.Control.JqGrid.Core.Response
{
    /// <summary>
    /// Class which represents response for jqGrid.
    /// </summary>
    public class JqGridResponse
    {
        #region Fields
        private static JqGridJsonReader _jsonReader;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the customized JSON reader for jqGrid (will be also used as defaults for JqGridHelper).
        /// </summary>
        public static JqGridJsonReader JsonReader
        {
            get { return _jsonReader; }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _jsonReader = value;
            }
        }

        /// <summary>
        /// Gets or sets the index (zero based) of page to return
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Gets the value indicating if this is a response for subgrid.
        /// </summary>
        public bool IsSubgridResponse { get; private set; }

        /// <summary>
        /// Gets or sets total pages count
        /// </summary>
        public int TotalPagesCount { get; set; }

        /// <summary>
        /// Gets or sets total records count
        /// </summary>
        public int TotalRecordsCount { get; set; }

        /// <summary>
        /// Gets the records list
        /// </summary>
        public IList<JqGridRecord> Records { get; private set; }

        /// <summary>
        /// Gets or sets the customized JSON reader for this response.
        /// </summary>
        public JqGridJsonReader Reader { get; set; }

        /// <summary>
        /// Gets or sets custom data that will be send with the response.
        /// </summary>
        public object UserData { get; set; }
        #endregion

        #region Constructor
        static JqGridResponse()
        {
            JsonReader = new JqGridJsonReader();
        }

        /// <summary>
        /// Initializes a new instance of the JqGridResponse class.
        /// </summary>
        /// <param name="isSubgridResponse">Value indicating if this is a response for subgrid.</param>
        public JqGridResponse(bool isSubgridResponse = false)
        {
            IsSubgridResponse = isSubgridResponse;
            Records = new List<JqGridRecord>();
            Reader = JsonReader.ShallowCopy();
        }
        #endregion
    }
}
