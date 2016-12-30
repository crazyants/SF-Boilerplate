using System;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.UoW.Helper
{



    public static partial class UnitOfWorkFactoryExtensions
    {
        #region GetAndReleaseAsync

        #region Task<T>

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        public static async Task<T> GetAndReleaseAsync<TFactory, TUoW, T>(
                   this TFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
                   where TFactory : IUnitOfWorkFactory
                   where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();
            try
            {
                return await toExecute(uow, ct);
            }
            finally
            {
                factory.Release(uow);
            }
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        public static async Task<T> GetAndReleaseAsync<TUoW, T>(
                  this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
                  where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW, T>(toExecute, ct);
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        public static async Task<T> GetAndReleaseAsync<TFactory, TUoW, T>(
                   this TFactory factory, Func<TUoW, Task<T>> toExecute)
                   where TFactory : IUnitOfWorkFactory
                   where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAsync<TFactory, TUoW, T>(async (uow, ct) => await toExecute(uow));
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        public static async Task<T> GetAndReleaseAsync<TUoW, T>(this IUnitOfWorkFactory factory, Func<TUoW, Task<T>> toExecute)
                  where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW, T>(toExecute);
        }


        #endregion

        #region Task

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        public static async Task GetAndReleaseAsync<TFactory, TUoW>(
                   this TFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
                   where TFactory : IUnitOfWorkFactory
                   where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            var uow = factory.Get<TUoW>();
            try
            {
                await toExecute(uow, ct);
            }
            finally
            {
                factory.Release(uow);
            }
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        public static async Task GetAndReleaseAsync<TUoW>(
                   this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
                   where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW>(toExecute, ct);
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        public static async Task GetAndReleaseAsync<TFactory, TUoW>(
                  this TFactory factory, Func<TUoW, Task> toExecute)
                  where TFactory : IUnitOfWorkFactory
                  where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAsync<TFactory, TUoW>(async (uow, ct) =>
            {
                await toExecute(uow);
            });
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        public static async Task GetAndReleaseAsync<TUoW>(
                 this IUnitOfWorkFactory factory, Func<TUoW, Task> toExecute)
                 where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAsync<IUnitOfWorkFactory, TUoW>(toExecute);
        }


        #endregion

        #endregion

        #region GetAndReleaseAfterExecuteAndCommitAsync

        #region Task<T>

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static async Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>(
                   this TFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
                   where TFactory : IUnitOfWorkFactory
                   where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            return await factory.GetAndReleaseAsync<TFactory, TUoW, T>(async (uow01, ct01) =>
            {
                return await uow01.ExecuteAndCommitAsync(async (uow02, ct02) => await toExecute(uow02, ct02), ct01);
            }, ct);
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static async Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TUoW, T>(
                   this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
                   where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW, T>(toExecute, ct);
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static async Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>(
                   this TFactory factory, Func<TUoW, Task<T>> toExecute)
                   where TFactory : IUnitOfWorkFactory
                   where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW, T>(
                async (uow, ct) => await toExecute(uow));
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance and returns the value. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited for the result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static async Task<T> GetAndReleaseAfterExecuteAndCommitAsync<TUoW, T>(
                   this IUnitOfWorkFactory factory, Func<TUoW, Task<T>> toExecute)
                   where TUoW : IUnitOfWork
        {
            return await factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW, T>(toExecute);
        }


        #endregion

        #region Task

        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static async Task GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>(
                   this TFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
                   where TFactory : IUnitOfWorkFactory
                   where TUoW : IUnitOfWork
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            await factory.GetAndReleaseAsync<TFactory, TUoW>(async (uow01, ct01) =>
            {
                await uow01.ExecuteAndCommitAsync(async (uow02, ct02) => await toExecute(uow02, ct02), ct01);
            }, ct);
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static async Task GetAndReleaseAfterExecuteAndCommitAsync<TUoW>(
                   this IUnitOfWorkFactory factory, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
                   where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW>(toExecute, ct);
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TFactory">The <see cref="IUnitOfWorkFactory"/> type</typeparam>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static async Task GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>(
                  this TFactory factory, Func<TUoW, Task> toExecute)
                  where TFactory : IUnitOfWorkFactory
                  where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAfterExecuteAndCommitAsync<TFactory, TUoW>(
                async (uow, ct) => await toExecute(uow));
        }


        /// <summary>
        /// Gets an <see cref="IUnitOfWork"/> from the given <see cref="IUnitOfWorkFactory"/> and, after 
        /// executing the function asynchronously, releases the UoW instance. 
        /// The function execution will be encapsulated inside a <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope.
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="factory">The factory to be used</param>
        /// <param name="toExecute">The action to be executed</param>
        /// <returns>A task that can be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="ConcurrencyException"/>
        /// <exception cref="CommitException"/>
        public static async Task GetAndReleaseAfterExecuteAndCommitAsync<TUoW>(
                  this IUnitOfWorkFactory factory, Func<TUoW, Task> toExecute)
                  where TUoW : IUnitOfWork
        {
            await factory.GetAndReleaseAfterExecuteAndCommitAsync<IUnitOfWorkFactory, TUoW>(toExecute);
        }


        #endregion

        #endregion
    }


}
