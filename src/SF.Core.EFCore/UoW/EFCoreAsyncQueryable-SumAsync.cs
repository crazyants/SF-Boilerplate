/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： EFCoreAsyncQueryable_SumAsync
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:55:32 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SF.Core.EFCore.UoW
{
    public partial class EFCoreAsyncQueryable<T>
    {
        #region Implementation of IAsyncQueryable<T>

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<decimal> SumAsync(Expression<Func<T, decimal>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<decimal?> SumAsync(Expression<Func<T, decimal?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<int> SumAsync(Expression<Func<T, int>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<int?> SumAsync(Expression<Func<T, int?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<long> SumAsync(Expression<Func<T, long>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<long?> SumAsync(Expression<Func<T, long?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<double> SumAsync(Expression<Func<T, double>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<double?> SumAsync(Expression<Func<T, double?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<float> SumAsync(Expression<Func<T, float>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        /// <summary>
        ///     Asynchronously computes the sum of the sequence of values that is obtained by invoking a projection function on
        ///     each element of the input sequence.
        /// </summary>
        /// <param name="selector"> A projection function to apply to each element. </param>
        /// <param name="ct">
        ///     A <see cref="T:System.Threading.CancellationToken" /> to observe while waiting for the task to complete.
        /// </param>
        /// <returns>
        ///     A task that represents the asynchronous operation.
        ///     The task result contains the sum of the projected values..
        /// </returns>
        public async Task<float?> SumAsync(Expression<Func<T, float?>> selector, CancellationToken ct = new CancellationToken())
        {
            return await _queryable.SumAsync(selector, ct);
        }

        #endregion
    }
}
