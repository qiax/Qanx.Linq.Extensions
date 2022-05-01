using Qanx.Linq.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Qanx.Linq.Extensions
{
    /// <summary>
    ///
    /// </summary>
    public static class DataPageExtensions
    {
        /// <summary>
        /// Process the data items after paging
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataPage"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static DataPage<T> ForEach<T>(this DataPage<T> dataPage, Action<T> action)
        {
            if (dataPage.DataList == null)
            {
                throw new ArgumentNullException(nameof(DataPage<T>.DataList));
            }
            foreach (var item in dataPage.DataList)
            {
                action(item);
            }
            return dataPage;
        }

        /// <summary>
        /// Process the data items after paging by async
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataPage"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<DataPage<T>> ForEachAsync<T>(this DataPage<T> dataPage, Action<T> action)
        {
            return await Task.Run(() =>
            {
                if (dataPage.DataList == null)
                    throw new ArgumentNullException(nameof(DataPage<T>.DataList));

                foreach (var item in dataPage.DataList)
                {
                    action(item);
                }
                return dataPage;
            });
        }
    }
}
