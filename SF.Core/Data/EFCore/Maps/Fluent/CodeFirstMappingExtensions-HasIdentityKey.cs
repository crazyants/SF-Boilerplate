
namespace SF.Core.EFCore.Maps.Fluent
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Entitys.Abstraction;

    public static partial class CodeFirstMappingExtensions
    {
        /// <summary>
        /// Maps the property <see cref="IEntity{TIdentity}.Id"/> as an identity in the database
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <typeparam name="TKey">The key type</typeparam>
        /// <param name="cfg">The entity configuration</param>
        /// <returns>The entity configuration after changes</returns>
        /// <exception cref="ArgumentNullException"/>
        public static EntityTypeBuilder<T> HasIdentityKey<T, TKey>(this EntityTypeBuilder<T> cfg)
            where T : class, IEntityWithTypedId<TKey>
        {
            if (cfg == null) throw new ArgumentNullException(nameof(cfg));

            cfg.HasKey(e => e.Id);
            cfg.Property(e => e.Id).ValueGeneratedOnAdd();

            return cfg;
        }
    }
}
