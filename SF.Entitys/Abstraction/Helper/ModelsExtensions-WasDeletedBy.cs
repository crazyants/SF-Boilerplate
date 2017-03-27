 
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
        /// Checks who deleted the <see cref="IHaveDeletedMeta{TDeletedBy}"/> instance.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <param name="by">Who deleted the entity</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The deleted by type</typeparam>
        /// <returns>True if the instance was deleted by the given parameter</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool WasDeletedBy<T, TBy>(this T entity, TBy by)
            where T : IHaveDeletedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (by == null) throw new ArgumentNullException(nameof(by));

            return by.Equals(entity.DeletedBy);
        }

#endif

        /// <summary>
        /// Checks who deleted the <see cref="IHaveLocalDeletedMeta{TDeletedBy}"/> instance.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <param name="by">Who deleted the entity</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The deleted by type</typeparam>
        /// <returns>True if the instance was deleted by the given parameter</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static bool WasDeletedLocallyBy<T, TBy>(T entity, TBy by)
#else
        public static bool WasDeletedLocallyBy<T, TBy>(this T entity, TBy by)
#endif
            where T : IHaveLocalDeletedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (by == null) throw new ArgumentNullException(nameof(by));

            return by.Equals(entity.DeletedBy);
        }
    }
}
