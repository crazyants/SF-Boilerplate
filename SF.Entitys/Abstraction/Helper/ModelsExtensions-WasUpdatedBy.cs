 
namespace SF.Entitys.Abstraction.Helper
{
    using System;

    /// <summary>
    /// Models extension methods
    /// </summary>
    public static partial class ModelsExtensions
    {

#if !NET20

        /// <summary>
        /// Checks who last updated the <see cref="IHaveUpdatedMeta{TUpdatedBy}"/> instance.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <param name="by">Who updated the entity</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The updated by type</typeparam>
        /// <returns>True if the instance was updated by the given parameter</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool WasUpdatedBy<T, TBy>(this T entity, TBy by)
            where T : IHaveUpdatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (by == null) throw new ArgumentNullException(nameof(by));

            return by.Equals(entity.UpdatedBy);
        }

#endif

        /// <summary>
        /// Checks who last updated the <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}"/> instance.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <param name="by">Who updated the entity</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The updated by type</typeparam>
        /// <returns>True if the instance was updated by the given parameter</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static bool WasUpdatedLocallyBy<T, TBy>(T entity, TBy by)
#else
        public static bool WasUpdatedLocallyBy<T, TBy>(this T entity, TBy by)
#endif
            where T : IHaveLocalUpdatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (by == null) throw new ArgumentNullException(nameof(by));

            return by.Equals(entity.UpdatedBy);
        }
    }
}
