using System;
namespace SF.Core.Abstraction.UoW.Helper
{



    /// <summary>
    /// Contains extension methods for <see cref="IUnitOfWorkFactory"/> instances.
    /// </summary>
    public static partial class UnitOfWorkFactoryExtensions
    {
        #region GetAndRelease

        #region T

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        public static T GetAndRelease<TFactory, TUoW, T>(this TFactory factory, Func<TUoW, T> toExecute)
                  where TFactory : IUnitOfWorkFactory
                  where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();
            try
            {
                return toExecute(uow);
            }
            finally
            {
                factory.Release(uow);
            }
        }

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        public static T GetAndRelease<TUoW, T>(this IUnitOfWorkFactory factory, Func<TUoW, T> toExecute)
                   where TUoW : IUnitOfWork
        {
            return GetAndRelease<IUnitOfWorkFactory, TUoW, T>(factory, toExecute);
        }

        #endregion

        #region Void

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <exception cref="ArgumentNullException"/>
        public static void GetAndRelease<TFactory, TUoW>(this TFactory factory, Action<TUoW> toExecute)

                   where TFactory : IUnitOfWorkFactory
                   where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();
            try
            {
                toExecute(uow);
            }
            finally
            {
                factory.Release(uow);
            }
        }

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <exception cref="ArgumentNullException"/>
        public static void GetAndRelease<TUoW>(this IUnitOfWorkFactory factory, Action<TUoW> toExecute)

                   where TUoW : IUnitOfWork
        {
            GetAndRelease<IUnitOfWorkFactory, TUoW>(factory, toExecute);
        }

        #endregion

        #endregion

        #region GetAndReleaseAfterExecuteAndCommit

        #region T

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value. The function execution will
        /// be encapsulated inside a <see cref="IUnitOfWork.Begin"/> and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static T GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW, T>(
                   this TFactory factory, Func<TUoW, T> toExecute)
                   where TFactory : IUnitOfWorkFactory
                   where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            return GetAndRelease<TFactory, TUoW, T>(
                // ReSharper disable once InvokeAsExtensionMethod
                factory, uow => UnitOfWorkExtensions.ExecuteAndCommit(uow, toExecute));
        }

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance and returns the value. The function execution will
        /// be encapsulated inside a <see cref="IUnitOfWork.Begin"/> and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static T GetAndReleaseAfterExecuteAndCommit<TUoW, T>(this IUnitOfWorkFactory factory, Func<TUoW, T> toExecute)
                  where TUoW : IUnitOfWork
        {
            return GetAndReleaseAfterExecuteAndCommit<IUnitOfWorkFactory, TUoW, T>(factory, toExecute);
        }

        #endregion

        #region Void

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance. The function execution will
        /// be encapsulated inside a <see cref="IUnitOfWork.Begin"/> and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static void GetAndReleaseAfterExecuteAndCommit<TFactory, TUoW>(
                  this TFactory factory, Action<TUoW> toExecute)
                  where TFactory : IUnitOfWorkFactory
                  where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            GetAndRelease<TFactory, TUoW>(
                // ReSharper disable once InvokeAsExtensionMethod
                factory, uow => UnitOfWorkExtensions.ExecuteAndCommit(uow, toExecute));
        }

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function, releases the UoW instance. The function execution will
        /// be encapsulated inside a <see cref="IUnitOfWork.Begin"/> and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The function to be executed</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static void GetAndReleaseAfterExecuteAndCommit<TUoW>(this IUnitOfWorkFactory factory, Action<TUoW> toExecute)
                  where TUoW : IUnitOfWork
        {
            GetAndReleaseAfterExecuteAndCommit<IUnitOfWorkFactory, TUoW>(factory, toExecute);
        }

        #endregion

        #endregion
    }
}
