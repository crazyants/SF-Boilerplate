 
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
        /// Sets the given <see cref="IHaveDeletedMeta{TDeletedBy}"/> in the deleted state, and sets
        /// the <see cref="IHaveUpdatedMeta{TUpdatedBy}"/> information
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">Who deleted the entity</param>
        /// <param name="on">The <see cref="DateTimeOffset"/> when it was deleted. If null <see cref="DateTimeOffset.Now"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The deleted by type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
        public static T UpdatedAndDeletedBy<T, TBy>(this T entity, TBy by = default(TBy), DateTimeOffset? on = null)
            where T : IHaveDeletedMeta<TBy>, IHaveUpdatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.DeletedOn = entity.UpdatedOn = on ?? DateTimeOffset.Now;
            entity.DeletedBy = entity.UpdatedBy = by;
            return entity;
        }

#endif

        /// <summary>
        /// Sets the given <see cref="IHaveLocalDeletedMeta{TDeletedBy}"/> in the deleted state, and sets
        /// the <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}"/> information
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">Who deleted the entity</param>
        /// <param name="on">The <see cref="DateTime"/> when it was deleted. If null <see cref="DateTime.UtcNow"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The deleted by type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static T UpdatedAndDeletedLocallyBy<T, TBy>(T entity, TBy by = default(TBy), DateTime? on = null)
#else
        public static T UpdatedAndDeletedLocallyBy<T, TBy>(this T entity, TBy by = default(TBy), DateTime? on = null)
#endif
            where T : IHaveLocalDeletedMeta<TBy>, IHaveLocalUpdatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.DeletedOn = entity.UpdatedOn = on ?? DateTime.UtcNow;
            entity.DeletedBy = entity.UpdatedBy = by;
            return entity;
        }
    }
}
