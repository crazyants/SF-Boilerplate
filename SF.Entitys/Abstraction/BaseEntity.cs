using System.ComponentModel.DataAnnotations;

namespace SF.Entitys.Abstraction
{
    /// <summary>
    ///     Provides a base class for your objects which will be persisted to the database.
    ///     Benefits include the addition of an Id property along with a consistent manner for comparing
    ///     entities.
    ///     Since nearly all of the entities you create will have a type of int Id, this
    ///     base class leverages this assumption.  If you want an entity with a type other
    ///     than int, such as string, then use <see cref="EntityWithTypedId{IdT}" /> instead.
    /// </summary>
    public abstract class BaseEntity : BaseEntity<long>
    {
        
 
    }

    public abstract class BaseEntity<TKey> : EntityWithAllMeta<TKey>
    {

        /// <summary>
        /// A Serial number for the value in the code table.  This can be used to sort (required).
        /// 排序码
        /// </summary>
        [Required]
        public int SortIndex { get; set; }
    }

    /// <summary>
    /// Represents an entity without any unique identifier
    /// </summary>
    public abstract class Entity : IEntity
    {

    }
}
