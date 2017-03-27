 
namespace SF.Entitys.Abstraction
{
    using System;

    /// <summary>
    /// Metadata information about the entity creation
    /// </summary>
    /// <typeparam name="TCreatedBy">The identifier or entity type</typeparam>
    public interface IHaveLocalCreatedMeta<TCreatedBy>
    {
        /// <summary>
        /// The <see cref="DateTime"/> when it was created
        /// </summary>
        DateTime CreatedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which first created this entity
        /// </summary>
        TCreatedBy CreatedBy { get; set; }
    }

    /// <summary>
    /// Metadata information about the entity creation, using a <see cref="string"/>
    /// as an identifier for the <see cref="IHaveLocalCreatedMeta{T}.CreatedBy"/>
    /// </summary>
    public interface IHaveLocalCreatedMeta : IHaveLocalCreatedMeta<string>
    {

    }
}
