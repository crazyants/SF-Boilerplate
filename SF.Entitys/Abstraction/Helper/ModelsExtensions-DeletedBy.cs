 
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
        /// Sets the given <see cref="IHaveDeletedMeta{TDeletedBy}"/> in the deleted state
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">Who deleted the entity</param>
        /// <param name="on">The <see cref="DateTimeOffset"/> when it was deleted. If null <see cref="DateTimeOffset.Now"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The deleted by type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
        public static T DeletedBy<T, TBy>(this T entity, TBy by = default(TBy), DateTimeOffset? on = null)
            where T : IHaveDeletedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.DeletedOn = on ?? DateTimeOffset.Now;
            entity.DeletedBy = by;
            return entity;
        }

#endif

        /// <summary>
        /// Sets the given <see cref="IHaveLocalDeletedMeta{TDeletedBy}"/> in the deleted state
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">Who deleted the entity</param>
        /// <param name="on">The <see cref="DateTime"/> when it was deleted. If null <see cref="DateTime.UtcNow"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The deleted by type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static T DeletedLocallyBy<T, TBy>(T entity, TBy by = default(TBy), DateTime? on = null)
#else
        public static T DeletedLocallyBy<T, TBy>(this T entity, TBy by = default(TBy), DateTime? on = null)
#endif
            where T : IHaveLocalDeletedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.DeletedOn = on ?? DateTime.UtcNow;
            entity.DeletedBy = by;
            return entity;
        }

        /// <summary>
        /// Updates the <see cref="IHaveSoftDelete"/> deleted state
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="delete">The delete state. By default will be set to true</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static T Deleted<T>(T entity, bool delete = true)
#else
        public static T Deleted<T>(this T entity, bool delete = true)
#endif
            where T : IHaveSoftDelete
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.Deleted = delete;
            return entity;
        }
    }
}
