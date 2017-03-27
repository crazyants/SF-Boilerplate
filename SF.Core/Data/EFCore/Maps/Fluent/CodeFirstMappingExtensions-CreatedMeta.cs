
namespace SF.Core.EFCore.Maps.Fluent
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entitys.Abstraction;

    public static partial class CodeFirstMappingExtensions
    {
        #region IHaveCreatedMeta

        /// <summary>
        /// Maps the created metadata for an entity implementing the <see cref="IHaveCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="byMaxLength">The max length for the <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedBy"/> property.</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedOn"/> needs an index?</param>
        /// <param name="byNeedsIndex">Does <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedBy"/> needs an index?</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapCreatedMeta<T>(
            this EntityTypeBuilder<T> cfg, int byMaxLength = DefaultMaxLength, bool onNeedsIndex = DefaultPropertyNeedsIndex,
            bool byNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveCreatedMeta<string>
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.CreatedOn).IsRequired();
            if (onNeedsIndex)
                cfg.HasIndex(e => e.CreatedOn);

            cfg.Property(e => e.CreatedBy).IsRequired().HasMaxLength(byMaxLength);
            if (byNeedsIndex)
                cfg.HasIndex(e => e.CreatedBy);

            return cfg;
        }

        /// <summary>
        /// Maps the created metadata for an entity implementing the <see cref="IHaveCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The by property type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedOn"/> needs an index?</param>
        /// <param name="byMapping">An optional lambda for mapping the <see cref="IHaveCreatedMeta{TCreatedBy}.CreatedBy"/></param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapCreatedMeta<T, TBy>(
            this EntityTypeBuilder<T> cfg, bool onNeedsIndex = DefaultPropertyNeedsIndex, Action<PropertyBuilder<TBy>> byMapping = null)
            where T : class, IHaveCreatedMeta<TBy>
            where TBy : struct
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.CreatedOn).IsRequired();
            if (onNeedsIndex)
                cfg.HasIndex(e => e.CreatedOn);

            var byCfg = cfg.Property(e => e.CreatedBy).IsRequired();
            byMapping?.Invoke(byCfg);

            return cfg;
        }

        #endregion

        #region IHaveLocalCreatedMeta

        /// <summary>
        /// Maps the created metadata for an entity implementing the <see cref="IHaveLocalCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="byMaxLength">The max length for the <see cref="IHaveLocalCreatedMeta{TCreatedBy}.CreatedBy"/> property.</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveLocalCreatedMeta{TCreatedBy}.CreatedOn"/> needs an index?</param>
        /// <param name="byNeedsIndex">Does <see cref="IHaveLocalCreatedMeta{TCreatedBy}.CreatedBy"/> needs an index?</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapLocalCreatedMeta<T>(
            this EntityTypeBuilder<T> cfg, int byMaxLength = DefaultMaxLength, bool onNeedsIndex = DefaultPropertyNeedsIndex,
            bool byNeedsIndex = DefaultPropertyNeedsIndex)
            where T : class, IHaveLocalCreatedMeta<string>
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.CreatedOn).IsRequired();
            if (onNeedsIndex)
                cfg.HasIndex(e => e.CreatedOn);

            cfg.Property(e => e.CreatedBy).IsRequired().HasMaxLength(byMaxLength);
            if (byNeedsIndex)
                cfg.HasIndex(e => e.CreatedBy);

            return cfg;
        }

        /// <summary>
        /// Maps the created metadata for an entity implementing the <see cref="IHaveLocalCreatedMeta{TCreatedBy}"/>
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TBy">The by property type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="onNeedsIndex">Does <see cref="IHaveLocalCreatedMeta{TCreatedBy}.CreatedOn"/> needs an index?</param>
        /// <param name="byMapping">An optional lambda for mapping the <see cref="IHaveLocalCreatedMeta{TCreatedBy}.CreatedBy"/></param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static EntityTypeBuilder<T> MapLocalCreatedMeta<T, TBy>(
            this EntityTypeBuilder<T> cfg, bool onNeedsIndex = DefaultPropertyNeedsIndex, Action<PropertyBuilder<TBy>> byMapping = null)
            where T : class, IHaveLocalCreatedMeta<TBy>
            where TBy : struct
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.Property(e => e.CreatedOn).IsRequired();
            if (onNeedsIndex)
                cfg.HasIndex(e => e.CreatedOn);

            var byCfg = cfg.Property(e => e.CreatedBy).IsRequired();
            byMapping?.Invoke(byCfg);

            return cfg;
        }

        #endregion
    }
}
