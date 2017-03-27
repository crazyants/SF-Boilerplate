
namespace SF.Core.EFCore.Maps.Fluent
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entitys.Abstraction;

    public static partial class CodeFirstMappingExtensions
    {
        #region IHaveDeletedMeta

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveDeletedMeta{TDeletedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="byMaxLength">The max length for the <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedBy"/> property.</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedOn"/> needs an index?</param>
        /// <param name="byNeedsIndex">Does <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedBy"/> needs an index?</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapDeletedMeta<T>(
            this EntityTypeBuilder<T> cfg, int byMaxLength = DefaultMaxLength, bool onNeedsIndex = DefaultPropertyNeedsIndex,
            bool byNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveDeletedMeta<string>
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.DeletedOn).IsRequired(false);
            if (onNeedsIndex)
                cfg.HasIndex(e => e.DeletedOn);

            cfg.Property(e => e.DeletedBy).IsRequired(false).HasMaxLength(byMaxLength);
            if (byNeedsIndex)
                cfg.HasIndex(e => e.DeletedBy);

            return cfg;
        }

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveDeletedMeta{TDeletedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The by property type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedOn"/> needs an index?</param>
        /// <param name="byMapping">An optional lambda for mapping the <see cref="IHaveDeletedMeta{TDeletedBy}.DeletedBy"/></param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapDeletedMeta<T, TBy>(
            this EntityTypeBuilder<T> cfg, bool onNeedsIndex = DefaultPropertyNeedsIndex, Action<PropertyBuilder<TBy>> byMapping = null)
            where T : class, IHaveDeletedMeta<TBy>
            where TBy : struct
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.DeletedOn).IsRequired(false);
            if (onNeedsIndex)
                cfg.HasIndex(e => e.DeletedOn);

            var byCfg = cfg.Property(e => e.DeletedBy).IsRequired(false);
            byMapping?.Invoke(byCfg);

            return cfg;
        }

        #endregion

        #region IHaveLocalDeletedMeta

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveLocalDeletedMeta{TDeletedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="byMaxLength">The max length for the <see cref="IHaveLocalDeletedMeta{TDeletedBy}.DeletedBy"/> property.</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveLocalDeletedMeta{TDeletedBy}.DeletedOn"/> needs an index?</param>
        /// <param name="byNeedsIndex">Does <see cref="IHaveLocalDeletedMeta{TDeletedBy}.DeletedBy"/> needs an index?</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapLocalDeletedMeta<T>(
            this EntityTypeBuilder<T> cfg, int byMaxLength = DefaultMaxLength, bool onNeedsIndex = DefaultPropertyNeedsIndex,
            bool byNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveLocalDeletedMeta<string>
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.DeletedOn).IsRequired(false);
            if (onNeedsIndex)
                cfg.HasIndex(e => e.DeletedOn);

            cfg.Property(e => e.DeletedBy).IsRequired(false).HasMaxLength(byMaxLength);
            if (byNeedsIndex)
                cfg.HasIndex(e => e.DeletedBy);

            return cfg;
        }

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveLocalDeletedMeta{TDeletedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The by property type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveLocalDeletedMeta{TDeletedBy}.DeletedOn"/> needs an index?</param>
        /// <param name="byMapping">An optional lambda for mapping the <see cref="IHaveLocalDeletedMeta{TDeletedBy}.DeletedBy"/></param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapLocalDeletedMeta<T, TBy>(
            this EntityTypeBuilder<T> cfg, bool onNeedsIndex = DefaultPropertyNeedsIndex, Action<PropertyBuilder<TBy>> byMapping = null)
            where T : class, IHaveLocalDeletedMeta<TBy>
            where TBy : struct
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.DeletedOn).IsRequired(false);
            if (onNeedsIndex)
                cfg.HasIndex(e => e.DeletedOn);

            var byCfg = cfg.Property(e => e.DeletedBy).IsRequired(false);
            byMapping?.Invoke(byCfg);

            return cfg;
        }

        #endregion

        /// <summary>
        /// Maps the deleted metadata for an entity implementing the <see cref="IHaveSoftDelete"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="needsIndex">Does <see cref="IHaveSoftDelete.Deleted"/> needs an index?</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapSoftDeleteMeta<T>(
            this EntityTypeBuilder<T> cfg, bool needsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveSoftDelete
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.Deleted).IsRequired();
            if (needsIndex)
                cfg.HasIndex(e => e.Deleted);

            return cfg;
        }
    }
}
