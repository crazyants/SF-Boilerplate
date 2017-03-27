 
namespace SF.Entitys.Abstraction
{
    using System;

#if !NET20

    /// <summary>
    /// Metadata information about the entity last update
    /// </summary>
    /// <typeparam name="TUpdatedBy">The identifier or entity type</typeparam>
    public interface IHaveUpdatedMeta<TUpdatedBy>
    {
        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was last updated
        /// </summary>
        DateTimeOffset UpdatedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which last updated this entity
        /// </summary>
        TUpdatedBy UpdatedBy { get; set; }
    }

    /// <summary>
    /// Metadata information about the entity last update, using a <see cref="string"/>
    /// as an identifier for the <see cref="IHaveUpdatedMeta{T}.UpdatedBy"/>
    /// </summary>
    public interface IHaveUpdatedMeta : IHaveUpdatedMeta<string>
    {

    }

#endif
}
