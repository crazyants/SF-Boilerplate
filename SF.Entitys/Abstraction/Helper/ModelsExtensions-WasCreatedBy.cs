 
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
        /// Checks who created the <see cref="IHaveCreatedMeta{TCreatedBy}"/> instance.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <param name="by">Who created the entity</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The created by type</typeparam>
        /// <returns>True if the instance was created by the given parameter</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool WasCreatedBy<T, TBy>(this T entity, TBy by)
            where T : IHaveCreatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (by == null) throw new ArgumentNullException(nameof(by));

            return by.Equals(entity.CreatedBy);
        }

#endif

        /// <summary>
        /// Checks who created the <see cref="IHaveLocalCreatedMeta{TCreatedBy}"/> instance.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <param name="by">Who created the entity</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The created by type</typeparam>
        /// <returns>True if the instance was created by the given parameter</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static bool WasCreatedLocallyBy<T, TBy>(T entity, TBy by)
#else
        public static bool WasCreatedLocallyBy<T, TBy>(this T entity, TBy by)
#endif
            where T : IHaveLocalCreatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (by == null) throw new ArgumentNullException(nameof(by));

            return by.Equals(entity.CreatedBy);
        }
    }
}
