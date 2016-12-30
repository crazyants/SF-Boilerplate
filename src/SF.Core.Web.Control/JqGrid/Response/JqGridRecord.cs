using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SF.Web.Control.JqGrid.Core.Response
{
    /// <summary>
    /// Class which represents record for jqGrid.
    /// </summary>
    public class JqGridRecord
    {
        #region Properties
        /// <summary>
        /// Gets the record cells values.
        /// </summary>
        public IList<object> Values { get; private set; }

        /// <summary>
        /// Gets the record value.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets the record identifier.
        /// </summary>
        public string Id { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the JqGridRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="values">The record cells values.</param>
        public JqGridRecord(string id, IList<object> values)
        {
            Id = id;
            Values = values;
            Value = null;
        }

        /// <summary>
        /// Initializes a new instance of the JqGridRecord class.
        /// </summary>
        /// <param name="id">The record identifier.</param>
        /// <param name="value">The record values</param>
        public JqGridRecord(string id, object value)
        {
            Id = id;
            Value = value;
            Values = GetValuesAsList();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns the object stored in value property as dictionary.
        /// </summary>
        /// <returns></returns>
        internal IDictionary<string, object> GetValueAsDictionary()
        {
            IDictionary<string, object> valueAsDictionary = new Dictionary<string, object>();

            if (Value != null)
            {
                foreach (PropertyInfo property in Value.GetType().GetRuntimeProperties().Where(IsInterestingProperty))
                {
                    valueAsDictionary.Add(property.Name, property.GetValue(Value));
                }
            }

            return valueAsDictionary;
        }

        private List<object> GetValuesAsList()
        {
            List<object> values = new List<object>();

            if (Value != null)
            {
                foreach (PropertyInfo property in Value.GetType().GetRuntimeProperties().Where(IsInterestingProperty))
                {
                    values.Add(property.GetValue(Value));
                }
            }

            return values;
        }

        private static bool IsInterestingProperty(PropertyInfo property)
        {
            return (property.GetIndexParameters().Length) == 0 && (property.GetMethod != null) && (property.GetMethod.IsPublic) && (!property.GetMethod.IsStatic);
        }
        #endregion
    }
}
