using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SF.Core.Errors.Internal;

namespace SF.Core.Errors
{
    /// <summary>
    /// An error model that is output to the client
    /// </summary>
    public class Error
    {
        /// <summary>
        /// A unique id to identify error messages in the logs
        /// </summary>
        public Guid Identifier { get; private set; }

        /// <summary>
        /// A URI to an absolute or relative html resource to identify the problem.
        /// </summary>
        public Uri Type { get; set; }

        /// <summary>
        /// A short description of the error
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The http Status code 
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// A code to identify what error it is.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Extra parameters to clarify the error
        /// </summary>
        public Dictionary<string, object> ExtraParameters { get; set; }

        public Error(Dictionary<string, object> extraParameters = null)
            : this(Guid.NewGuid(), extraParameters)
        { }

        public Error(Guid identifier, Dictionary<string, object> extraParameters = null)
        {
            if (identifier == default(Guid))
                throw new ArgumentException("An empty Guid is not allowed", nameof(identifier));

            Identifier = identifier;
            ExtraParameters = extraParameters ?? new Dictionary<string, object>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder($"Error ( Id = {Identifier}, Messages = ");
            if (!ExtraParameters.Any())
                builder.Append("'none' )");
            else
            {
                
                foreach (var message in ExtraParameters)
                {
                    string errorMessage;
                    var type = message.Value.GetType();
                    if (type != typeof(string) && type.GetTypeInfo().GetInterface("IEnumerable") != null)
                    {
                        errorMessage = string.Join(", ", (IEnumerable<object>) message.Value);
                    }
                    else errorMessage = message.Value.ToString();
                    builder.Append($"ErrorMessage ( Key = {message.Key}, Message = {errorMessage} ), ");
                }
                builder.Remove(builder.Length - 2, 2);
            }
            return builder.ToString();
        }
    }
}
