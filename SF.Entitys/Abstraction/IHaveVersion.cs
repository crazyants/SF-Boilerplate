 
namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// Metadata information about the entity version
    /// </summary>
    /// <example>
    /// For Entity Framework you should use IHaveVersion&lt;byte[]&gt; 
    /// (<see cref="IHaveVersionAsByteArray"/> may be used instead)
    /// <code>
    /// public class Person : IEntity&lt;long&gt;, IHaveVersion&lt;byte[]&gt; {
    /// 
    ///     public long Id { get; set; }
    ///     public string Name { get; set; }
    ///     public byte[] Version { get; set; }
    /// 
    /// }
    /// </code>
    /// </example>
    /// <typeparam name="TVersion">
    /// The version property type.
    /// </typeparam>
    public interface IHaveVersion<TVersion>
    {
        /// <summary>
        /// The entity version
        /// </summary>
        TVersion Version { get; set; }
    }
}
