 
namespace SF.Entitys.Abstraction
{
    using System;

    /// <summary>
    /// Metadata information about the entity last update
    /// </summary>
    /// <typeparam name="TUpdatedBy">The identifier or entity type</typeparam>
    public interface IHaveLocalUpdatedMeta<TUpdatedBy>
    {
        /// <summary>
        /// The <see cref="DateTime"/> when it was last updated
        /// </summary>
        DateTime UpdatedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which last updated this entity
        /// </summary>
        TUpdatedBy UpdatedBy { get; set; }
    }

    /// <summary>
    /// Metadata information about the entity last update, using a <see cref="string"/>
    /// as an identifier for the <see cref="IHaveLocalUpdatedMeta{T}.UpdatedBy"/>
    /// </summary>
    public interface IHaveLocalUpdatedMeta : IHaveLocalUpdatedMeta<string>
    {

    }
}
