 
namespace SF.Core.EFCore.Maps.Fluent
{
    /// <summary>
    /// Extension methods for Entity Framework 7 code first mappings
    /// </summary>
    public static partial class CodeFirstMappingExtensions
    {
        /// <summary>
        /// The default (128) max length used in metadata fields
        /// </summary>
        public const int DefaultMaxLength = 128;

        /// <summary>
        /// The default value when a property can have an index. By default is false.
        /// </summary>
        public const bool DefaultPropertyNeedsIndex = false;
    }
}
