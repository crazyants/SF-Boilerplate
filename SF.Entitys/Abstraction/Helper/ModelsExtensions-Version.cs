 
namespace SF.Entitys.Abstraction.Helper
{
    using System;

    /// <summary>
    /// Models extension methods
    /// </summary>
    public static partial class ModelsExtensions
    {
        /// <summary>
        /// Replaces the content of a given <see cref="IHaveVersion{TVersion}.Version"/> byte[] 
        /// with the given one.
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="entity">The entity to be used</param>
        /// <param name="version">The version to be replaced with</param>
        /// <returns>The entity after changes</returns>
        /// <exception cref="ArgumentNullException">Thrown if any of the parameters are null</exception>
        /// <exception cref="ArgumentException">Thrown if the arrays length does not match</exception>
#if NET20
        public static TEntity ByteArrayVersion<TEntity>(TEntity entity, byte[] version)
#else
        public static TEntity ByteArrayVersion<TEntity>(this TEntity entity, byte[] version)
#endif
            where TEntity : IHaveVersion<byte[]>
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            if (version == null) throw new ArgumentNullException(nameof(version));
            if (entity.Version.Length != version.Length)
                throw new ArgumentException(
                    $"Entity version length [{entity.Version.Length}] does not match with the recieved [{version.Length}]",
                    nameof(version));

            Array.Copy(version, entity.Version, entity.Version.Length);
            return entity;
        }
    }
}
