 
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
        /// Fills all metadata of a given <see cref="IHaveCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">Who created the entity</param>
        /// <param name="on">The <see cref="DateTimeOffset"/> when it was created. If null <see cref="DateTimeOffset.Now"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The created by type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
        public static T CreatedBy<T, TBy>(this T entity, TBy by = default(TBy), DateTimeOffset? on = null)
            where T : IHaveCreatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.CreatedOn = on ?? DateTimeOffset.Now;
            entity.CreatedBy = by;
            return entity;
        }

#endif

        /// <summary>
        /// Fills all metadata of a given <see cref="IHaveLocalCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <param name="entity">The entity to fill</param>
        /// <param name="by">Who created the entity</param>
        /// <param name="on">The <see cref="DateTime"/> when it was created. If null <see cref="DateTime.UtcNow"/> will be used</param>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The created by type</typeparam>
        /// <returns>The received entity after changes</returns>
        /// <exception cref="ArgumentNullException"/>
#if NET20
        public static T CreatedLocallyBy<T, TBy>(T entity, TBy by = default(TBy), DateTime? on = null)
#else
        public static T CreatedLocallyBy<T, TBy>(this T entity, TBy by = default(TBy), DateTime? on = null)
#endif
            where T : IHaveLocalCreatedMeta<TBy>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            entity.CreatedOn = on ?? DateTime.UtcNow;
            entity.CreatedBy = by;
            return entity;
        }
    }
}
