 
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
        /// Checks if the <see cref="IHaveDeletedMeta{TDeletedBy}"/> instance is deleted.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The deleted by type</typeparam>
        /// <returns>True if the instance is deleted</returns>
        /// <exception cref="ArgumentNullException"/>
        public static bool IsDeleted<T, TBy>(this T entity)
            where T : IHaveDeletedMeta<TBy>
            where TBy : class 
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return entity.DeletedBy == null;
        }

#endif

        /// <summary>
        /// Checks if the <see cref="IHaveLocalDeletedMeta{TDeletedBy}"/> instance is deleted.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The deleted by type</typeparam>
        /// <returns>True if the instance is deleted</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static bool IsDeletedLocally<T, TBy>(T entity)
#else
        public static bool IsDeletedLocally<T, TBy>(this T entity)
#endif
            where T : IHaveLocalDeletedMeta<TBy>
            where TBy : class
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return entity.DeletedBy == null;
        }

        /// <summary>
        /// Checks if the <see cref="IHaveSoftDelete"/> instance is deleted.
        /// </summary>
        /// <param name="entity">The entity to check</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>True if the instance is deleted</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static bool IsDeleted<T>(T entity)
#else
        public static bool IsDeleted<T>(this T entity)
#endif
            where T : IHaveSoftDelete
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            return entity.Deleted;
        }
    }
}
