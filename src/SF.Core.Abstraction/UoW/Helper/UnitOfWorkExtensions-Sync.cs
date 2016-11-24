using System;
namespace SF.Core.Abstraction.UoW.Helper
{
    /// <summary>
    /// Contains extension methods for <see cref="IUnitOfWork"/> instances.
    /// </summary>
    public static partial class UnitOfWorkExtensions
    {
        #region T

        /// <summary>
        /// Executes the given function inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static T ExecuteAndCommit<TUoW, T>(this TUoW uow, Func<TUoW, T> toExecute)
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            uow.Begin();

            var result = toExecute(uow);

            uow.Commit();

            return result;
        }

        /// <summary>
        /// Executes the given function inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static T ExecuteAndCommit<TUoW, T>(this TUoW uow, Func<T> toExecute)
                    where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            uow.Begin();

            var result = toExecute();

            uow.Commit();

            return result;
        }

        /// <summary>
        /// Executes the given function inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="T">The result type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <returns>The function result</returns>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static T ExecuteAndCommit<T>(this IUnitOfWork uow, Func<T> toExecute)
        {
            return ExecuteAndCommit<IUnitOfWork, T>(uow, toExecute);
        }

        #endregion

        #region Void

        /// <summary>
        /// Executes the given action inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static void ExecuteAndCommit<TUoW>(this TUoW uow, Action<TUoW> toExecute)
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            uow.Begin();

            toExecute(uow);

            uow.Commit();
        }

        /// <summary>
        /// Executes the given action inside a <see cref="IUnitOfWork.Begin"/>
        /// and <see cref="IUnitOfWork.Commit"/> scope
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <param name="uow">The <see cref="IUnitOfWork"/> to be used</param>
        /// <param name="toExecute">The function to be executed inside the scope</param>
        /// <exception cref="ArgumentNullException"/>
        /// <exception cref="CommitException"/>
        /// <exception cref="ConcurrencyException"/>
        public static void ExecuteAndCommit<TUoW>(this TUoW uow, Action toExecute)
            where TUoW : IUnitOfWork
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            if (toExecute == null) throw new ArgumentNullException(nameof(toExecute));

            uow.Begin();

            toExecute();

            uow.Commit();
        }

        #endregion
    }
}
