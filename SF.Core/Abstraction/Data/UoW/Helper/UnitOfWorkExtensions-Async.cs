using System;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.UoW.Helper
{



    public static partial class UnitOfWorkExtensions
    {
        #region Task<T>

        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static async Task<T> ExecuteAndCommitAsync<TUoW, T>(
            this TUoW uow, Func<TUoW, CancellationToken, Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            await uow.BeginAsync(ct);

            var result = await toExecute(uow, ct);

            await uow.CommitAsync(ct);

            return result;
        }


        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static async Task<T> ExecuteAndCommitAsync<TUoW, T>(
            this TUoW uow, Func<Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            return await uow.ExecuteAndCommitAsync(async (u, c) => await toExecute(), ct);
        }


        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static async Task<T> ExecuteAndCommitAsync<T>(
            this IUnitOfWork uow, Func<Task<T>> toExecute, CancellationToken ct = default(CancellationToken))
        {
            return await uow.ExecuteAndCommitAsync<IUnitOfWork, T>(toExecute, ct);
        }


        #endregion

        #region Task

        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static async Task ExecuteAndCommitAsync<TUoW>(
            this TUoW uow, Func<TUoW, CancellationToken, Task> toExecute, CancellationToken ct = default(CancellationToken))
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            await uow.BeginAsync(ct);

            await toExecute(uow, ct);

            await uow.CommitAsync(ct);
        }


        /// <summary>
        /// Asynchronously executes the given function inside an <see cref="IUnitOfWork.BeginAsync"/> 
        /// and <see cref="IUnitOfWork.CommitAsync"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task to be awaited</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static async Task ExecuteAndCommitAsync<TUoW>(
                   this TUoW uow, Func<Task> toExecute, CancellationToken ct = default(CancellationToken))
                   where TUoW : IUnitOfWork
        {
            await uow.ExecuteAndCommitAsync(async (u, c) =>
            {
                await toExecute();
            }, ct);
        }


        #endregion
    }


}
