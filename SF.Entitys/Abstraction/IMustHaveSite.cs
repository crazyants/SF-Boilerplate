namespace SF.Entitys.Abstraction
{
    /// <summary>
    /// Implement this interface for an entity which must have SiteId.
    /// </summary>
    public interface IMustHaveSite
    {
        /// <summary>
        /// SiteId of this entity.
        /// </summary>
        long SiteId { get; set; }
    }
}