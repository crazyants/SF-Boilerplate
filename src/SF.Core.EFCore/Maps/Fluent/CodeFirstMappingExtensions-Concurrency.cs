
namespace SF.Core.EFCore.Maps.Fluent
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entitys.Abstraction;

    public static partial class CodeFirstMappingExtensions
    {
        /// <summary>
        /// Maps the property <see cref="IHaveVersion{TVersion}.Version"/> as a concurrency token
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <param name="mapping">An optional lambda for mapping the <see cref="IHaveVersion{TVersion}.Version"/> property</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"/>
        public static EntityTypeBuilder<T> MapByteArrayVersion<T>(
            this EntityTypeBuilder<T> cfg, Action<PropertyBuilder<byte[]>> mapping = null)
            where T : class, IHaveVersion<byte[]>
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            var versionCfg = cfg.Property(e => e.Version).ValueGeneratedOnAddOrUpdate().IsConcurrencyToken();
            mapping?.Invoke(versionCfg);

            return cfg;
        }
    }
}
