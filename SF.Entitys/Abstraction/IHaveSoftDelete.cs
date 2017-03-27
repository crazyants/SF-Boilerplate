 
namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// Metadata information about the entity delete state
    /// </summary>
    public interface IHaveSoftDelete
    {
        /// <summary>
        /// Is the entity deleted?
        /// </summary>
        bool Deleted { get; set; }
    }
}
