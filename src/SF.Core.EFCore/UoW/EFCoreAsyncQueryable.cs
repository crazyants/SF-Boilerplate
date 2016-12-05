/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： EFCoreAsyncQueryable
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:53:46 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SF.Core.EFCore.UoW
{
    /// <summary>
    /// Specialized <see cref="IQueryable{T}"/> for async executions using the Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public partial class EFCoreAsyncQueryable<T> : IEFCoreAsyncQueryable<T>
    {
        private readonly IQueryable<T> _queryable;

        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="queryable">The queryable to wrap</param>
        public EFCoreAsyncQueryable(IQueryable<T> queryable)
        {
            if (queryable == null) throw new ArgumentNullException(nameof(queryable));
            _queryable = queryable;
        }

        #region Implementation of IEnumerable

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator() => _queryable.GetEnumerator();

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region Implementation of IQueryable

        /// <summary>Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable" />.</summary>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression" /> that is associated with this instance of <see cref="T:System.Linq.IQueryable" />.</returns>
        public Expression Expression => _queryable.Expression;

        /// <summary>Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable" /> is executed.</summary>
        /// <returns>A <see cref="T:System.Type" /> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        public Type ElementType => _queryable.ElementType;

        /// <summary>Gets the query provider that is associated with this data source.</summary>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider" /> that is associated with this data source.</returns>
        public IQueryProvider Provider => _queryable.Provider;

        #endregion

        #region Implementation of IAsyncQueryable<T>

        /// <summary>
        /// Asynchronously enumerates the query result into memory
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task LoadAsync(CancellationToken ct = new CancellationToken())
        {
            await _queryable.LoadAsync(ct);
        }

        /// <summary>
        /// Asynchronously enumerates the query results and performs the specified action on each element.
        /// </summary>
        /// <param name="action">The action to perform on each element.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task ForEachAsync(Action<T> action, CancellationToken ct = new CancellationToken())
        {
            await _queryable.ForEachAsync(action, ct);
        }

        /// <summary>
        /// Creates a <see cref="T:System.Collections.Generic.List`1" /> from an <see cref="T:System.Linq.IQueryable" /> by enumerating it asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains a <see cref="T:System.Collections.Generic.List`1" /> that contains elements from the input sequence.
        /// </returns>
        public async Task<List<T>> ToListAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToListAsync(ct);
        }

        /// <summary>
        /// Creates an array from an <see cref="T:System.Linq.IQueryable`1" /> by enumerating it asynchronously.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an array that contains elements from the input sequence.
        /// </returns>
        public async Task<T[]> ToArrayAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToArrayAsync(ct);
        }

        /// <summary>
        ///     Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Linq.IQueryable`1" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains selected keys and values.
        /// </returns>
        public async Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(Func<T, TKey> keySelector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToDictionaryAsync(keySelector, ct);
        }

        /// <summary>
        ///     Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Linq.IQueryable`1" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function and a comparer.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="comparer">
        ///     An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.
        /// </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains selected keys and values.
        /// </returns>
        public async Task<Dictionary<TKey, T>> ToDictionaryAsync<TKey>(
            Func<T, TKey> keySelector, IEqualityComparer<TKey> comparer, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToDictionaryAsync(keySelector, comparer, ct);
        }

        /// <summary>
        ///     Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Linq.IQueryable`1" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector and an element selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <typeparam name="TElement">
        ///     The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="elementSelector"> A transform function to produce a result element value from each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type
        ///     <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        public async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            Func<T, TKey> keySelector, Func<T, TElement> elementSelector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToDictionaryAsync(keySelector, elementSelector, ct);
        }

        /// <summary>
        ///     Creates a <see cref="T:System.Collections.Generic.Dictionary`2" /> from an <see cref="T:System.Linq.IQueryable`1" /> by enumerating it
        ///     asynchronously
        ///     according to a specified key selector function, a comparer, and an element selector function.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of the key returned by <paramref name="keySelector" /> .
        /// </typeparam>
        /// <typeparam name="TElement">
        ///     The type of the value returned by <paramref name="elementSelector" />.
        /// </typeparam>
        /// <param name="keySelector"> A function to extract a key from each element. </param>
        /// <param name="elementSelector"> A transform function to produce a result element value from each element. </param>
        /// <param name="comparer">
        ///     An <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> to compare keys.
        /// </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains a <see cref="T:System.Collections.Generic.Dictionary`2" /> that contains values of type
        ///     <typeparamref name="TElement" /> selected from the input sequence.
        /// </returns>
        public async Task<Dictionary<TKey, TElement>> ToDictionaryAsync<TKey, TElement>(
            Func<T, TKey> keySelector, Func<T, TElement> elementSelector, IEqualityComparer<TKey> comparer, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ToDictionaryAsync(keySelector, elementSelector, comparer, ct);
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence or a default value if no such element is found.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public async Task<T> FirstOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.FirstOrDefaultAsync(ct);
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.FirstOrDefaultAsync(predicate, ct);
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public async Task<T> FirstAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.FirstAsync(ct);
        }

        /// <summary>
        /// Asynchronously returns the first element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the first element in source.
        /// </returns>
        public async Task<T> FirstAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.FirstAsync(predicate, ct);
        }

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> AnyAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.AnyAsync(ct);
        }

        /// <summary>
        /// Asynchronously determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">The cancellation token</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains <c>true</c> if the source sequence contains any elements; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.AnyAsync(predicate, ct);
        }

        /// <summary>
        ///     Asynchronously determines whether all the elements of a sequence satisfy a condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>true</c> if every element of the source sequence passes the test in the specified
        ///     predicate; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> AllAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.AllAsync(predicate, ct);
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public async Task<int> CountAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.CountAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.CountAsync(predicate, ct);
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public async Task<long> LongCountAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.LongCountAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the number of elements in a sequence.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the number of elements in the input sequence.
        /// </returns>
        public async Task<long> LongCountAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.LongCountAsync(predicate, ct);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element.
        /// </returns>
        public async Task<T> LastAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.LastAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="predicate"> A function to test each element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the last element that passes the test in
        ///     <paramref name="predicate" />.
        /// </returns>
        public async Task<T> LastAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.LastAsync(predicate, ct);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        public async Task<T> LastOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.LastOrDefaultAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>default</c> ( <typeparamref name="T" /> ) if empty; otherwise, the last element.
        /// </returns>
        public async Task<T> LastOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.LastOrDefaultAsync(predicate, ct);
        }

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the sequence.
        /// </returns>
        public async Task<T> SingleAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SingleAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition,
        ///     and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />.
        /// </returns>
        public async Task<T> SingleAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SingleAsync(predicate, ct);
        }

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the sequence, or <c>default</c> ( <typeparamref name="T" /> ) if no such element is found.
        /// </returns>
        public async Task<T> SingleOrDefaultAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SingleOrDefaultAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the only element of a sequence that satisfies a specified condition or
        ///     a default value if no such element exists; this method throws an exception if more than one element
        ///     satisfies the condition.
        /// </summary>
        /// <param name="predicate"> A function to test an element for a condition. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the single element of the input sequence that satisfies the condition in
        ///     <paramref name="predicate" />, or <c>default</c> ( <typeparamref name="T" /> ) if no such element is found.
        /// </returns>
        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SingleOrDefaultAsync(predicate, ct);
        }

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        public async Task<T> MinAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.MinAsync(ct);
        }

        /// <summary>
        ///     Asynchronously returns the minimum value of a sequence.
        /// </summary>
        /// <remarks>
        ///     Multiple active operations on the same context instance are not supported.  Use 'await' to ensure
        ///     that any asynchronous operations have completed before calling another method on this context.
        /// </remarks>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the minimum value in the sequence.
        /// </returns>
        public async Task<TResult> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.MinAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        public async Task<T> MaxAsync(CancellationToken ct = new CancellationToken())
        {
            return await _queryable.MaxAsync(ct);
        }

        /// <summary>
        ///     Asynchronously invokes a projection function on each element of a sequence and returns the maximum resulting value.
        /// </summary>
        /// <typeparam name="TResult">
        ///     The type of the value returned by the function represented by <paramref name="selector" /> .
        /// </typeparam>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the maximum value in the sequence.
        /// </returns>
        public async Task<TResult> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.MaxAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        /// <param name="item"> The object to locate in the sequence. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains <c>true</c> if the input sequence contains the specified value; otherwise, <c>false</c>.
        /// </returns>
        public async Task<bool> ContainsAsync(T item, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.ContainsAsync(item, ct);
        }

        #endregion
    }
}
