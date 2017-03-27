 
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
        /// Fills all metadata of a given <see cref="IHaveUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">Who updated the entity</param>
        /// <param name="on">The <see cref="DateTimeOffset"/> when it was updated. If null <see cref="DateTimeOffset.Now"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The updated by type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
        public static T UpdatedBy<T, TBy>(this T entity, TBy by = default(TBy), DateTimeOffset? on = null)
            where T : IHaveUpdatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.UpdatedOn = on ?? DateTimeOffset.Now;
            entity.UpdatedBy = by;
            return entity;
        }

#endif

        /// <summary>
        /// Fills all metadata of a given <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">Who updated the entity</param>
        /// <param name="on">The <see cref="DateTime"/> when it was updated. If null <see cref="DateTime.UtcNow"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The updated by type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static T UpdatedLocallyBy<T, TBy>(T entity, TBy by = default(TBy), DateTime? on = null)
#else
        public static T UpdatedLocallyBy<T, TBy>(this T entity, TBy by = default(TBy), DateTime? on = null)
#endif
            where T : IHaveLocalUpdatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.UpdatedOn = on ?? DateTime.UtcNow;
            entity.UpdatedBy = by;
            return entity;
        }
    }
}
