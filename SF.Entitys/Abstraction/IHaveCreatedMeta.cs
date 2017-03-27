 
namespace SF.Entitys.Abstraction
{
    using System;

#if !NET20

    /// <summary>
    /// Metadata information about the entity creation
    /// </summary>
    /// <typeparam name="TCreatedBy">The identifier or entity type</typeparam>
    public interface IHaveCreatedMeta<TCreatedBy>
    {
        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was created
        /// </summary>
        DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// The identifier (or entity) which first created this entity
        /// </summary>
        TCreatedBy CreatedBy { get; set; }
    }

    /// <summary>
    /// Metadata information about the entity creation, using a <see cref="string"/>
    /// as an identifier for the <see cref="IHaveCreatedMeta{T}.CreatedBy"/>
    /// </summary>
    public interface IHaveCreatedMeta : IHaveCreatedMeta<string>
    {

    }

#endif

}
