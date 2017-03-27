
namespace SF.Core.EFCore.Maps.Fluent
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entitys.Abstraction;

    public static partial class CodeFirstMappingExtensions
    {
        #region IHaveUpdatedMeta

        /// <summary>
        /// Maps the updated metadata for an entity implementing the <see cref="IHaveUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="byMaxLength">The max length for the <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedBy"/> property.</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedOn"/> needs an index?</param>
        /// <param name="byNeedsIndex">Does <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedBy"/> needs an index?</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapUpdatedMeta<T>(
            this EntityTypeBuilder<T> cfg, int byMaxLength = DefaultMaxLength, bool onNeedsIndex = DefaultPropertyNeedsIndex,
            bool byNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveUpdatedMeta<string>
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.UpdatedOn).IsRequired();
            if (onNeedsIndex)
                cfg.HasIndex(e => e.UpdatedOn);

            cfg.Property(e => e.UpdatedBy).IsRequired().HasMaxLength(byMaxLength);
            if (byNeedsIndex)
                cfg.HasIndex(e => e.UpdatedBy);

            return cfg;
        }

        /// <summary>
        /// Maps the updated metadata for an entity implementing the <see cref="IHaveUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The by property type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedOn"/> needs an index?</param>
        /// <param name="byMapping">An optional lambda for mapping the <see cref="IHaveUpdatedMeta{TUpdatedBy}.UpdatedBy"/></param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapUpdatedMeta<T, TBy>(
            this EntityTypeBuilder<T> cfg, bool onNeedsIndex = DefaultPropertyNeedsIndex, Action<PropertyBuilder<TBy>> byMapping = null)
            where T : class, IHaveUpdatedMeta<TBy>
            where TBy : struct
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.UpdatedOn).IsRequired();
            if (onNeedsIndex)
                cfg.HasIndex(e => e.UpdatedOn);

            var byCfg = cfg.Property(e => e.UpdatedBy).IsRequired();
            byMapping?.Invoke(byCfg);

            return cfg;
        }

        #endregion

        #region IHaveLocalUpdatedMeta

        /// <summary>
        /// Maps the updated metadata for an entity implementing the <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="byMaxLength">The max length for the <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}.UpdatedBy"/> property.</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}.UpdatedOn"/> needs an index?</param>
        /// <param name="byNeedsIndex">Does <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}.UpdatedBy"/> needs an index?</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapLocalUpdatedMeta<T>(
            this EntityTypeBuilder<T> cfg, int byMaxLength = DefaultMaxLength, bool onNeedsIndex = DefaultPropertyNeedsIndex,
            bool byNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveLocalUpdatedMeta<string>
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.UpdatedOn).IsRequired();
            if (onNeedsIndex)
                cfg.HasIndex(e => e.UpdatedOn);

            cfg.Property(e => e.UpdatedBy).IsRequired().HasMaxLength(byMaxLength);
            if (byNeedsIndex)
                cfg.HasIndex(e => e.UpdatedBy);

            return cfg;
        }

        /// <summary>
        /// Maps the updated metadata for an entity implementing the <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The by property type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}.UpdatedOn"/> needs an index?</param>
        /// <param name="byMapping">An optional lambda for mapping the <see cref="IHaveLocalUpdatedMeta{TUpdatedBy}.UpdatedBy"/></param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapLocalUpdatedMeta<T, TBy>(
            this EntityTypeBuilder<T> cfg, bool onNeedsIndex = DefaultPropertyNeedsIndex, Action<PropertyBuilder<TBy>> byMapping = null)
            where T : class, IHaveLocalUpdatedMeta<TBy>
            where TBy : struct
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.UpdatedOn).IsRequired();
            if (onNeedsIndex)
                cfg.HasIndex(e => e.UpdatedOn);

            var byCfg = cfg.Property(e => e.UpdatedBy).IsRequired();
            byMapping?.Invoke(byCfg);

            return cfg;
        }

        #endregion

    }
}
