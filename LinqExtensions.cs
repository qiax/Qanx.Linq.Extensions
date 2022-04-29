using Qanx.Linq.Extensions.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class LinqExtensions
    {
        /// <summary>
        /// If the query value(the query value must be a struct) is null，Then return to the source query,
        /// else it will filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TStruct">Structural constraints</typeparam>
        /// <param name="source">source IQueryable</param>
        /// <param name="queryVal">the query value must be a struct</param>
        /// <param name="predicate">query predicate</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIf<TEntity, TStruct>(this IQueryable<TEntity> source, TStruct? queryVal, Expression<Func<TEntity, bool>> predicate)
            where TStruct : struct
        {
            if (queryVal != null)
            {
                source = source.Where(predicate);
            }
            return source;
        }

        /// <summary>
        /// If the query value is null，Then return to the source query,
        /// else it will filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="queryVal">The type of the query value is string</param>
        /// <param name="predicate">query predicate</param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> source, string queryVal, Expression<Func<TEntity, bool>> predicate)
            where TEntity : class, new()
        {
            if (!string.IsNullOrEmpty(queryVal))
            {
                source = source.Where(predicate);
            }
            return source;
        }

        /// <summary>
        /// Convert query object to paging query object
        /// </summary>
        /// <typeparam name="T">Paging data type</typeparam>
        /// <param name="source">source query object</param>
        /// <param name="pageIndex">page index</param>
        /// <param name="pageSize">page size</param>
        /// <returns></returns>
        public static DataPage<T> ToPagedList<T>(this IOrderedQueryable<T> source, int pageIndex, int pageSize)
        {
            var dataPage = new DataPage<T>(pageIndex, pageSize);

            int skipCount = (dataPage.PageIndex - 1) * dataPage.PageSize;
            var dataList = source.Skip(skipCount).Take(pageSize).ToList();

            dataPage.SetDataList(dataList);
            dataPage.SetTotalCount(source.Count());

            return dataPage;
        }

        /// <summary>
        /// Convert query object to paging query object by async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">source query object</param>
        /// <param name="pageIndex">page index, default(1)</param>
        /// <param name="pageSize">page size, default(10)</param>
        /// <returns></returns>
        public static async Task<DataPage<T>> ToPagedListAsync<T>(this IOrderedQueryable<T> source, int pageIndex, int pageSize)
        {
            return await Task.Run(() =>
            {
                var dataPage = new DataPage<T>(pageIndex, pageSize);

                int skipCount = (dataPage.PageIndex - 1) * dataPage.PageSize;
                var dataList = source.Skip(skipCount).Take(pageSize).ToList();

                dataPage.SetDataList(dataList);
                dataPage.SetTotalCount(source.Count());

                return dataPage;
            });
        }
    }
}
