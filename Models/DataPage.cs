using System.Collections.Generic;

namespace Qanx.Linq.Extensions.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataPage<T>
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public virtual int PageIndex { get; private set; }

        /// <summary>
        /// 页面容量
        /// </summary>
        public virtual int PageSize { get; private set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public virtual int TotalCount { get; private set; }

        /// <summary>
        /// 当前页展示的数据
        /// </summary>
        public virtual IEnumerable<T> DataList { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex">default(1)</param>
        /// <param name="pageSize">default(10)</param>
        public DataPage(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex <= 0 ? 1 : pageIndex;
            this.PageSize = pageSize <= 0 ? 10 : pageSize;
            this.TotalCount = 0;
            this.DataList = new List<T>();
        }

        /// <summary>
        /// Set totalcount
        /// </summary>
        /// <param name="totalCount"></param>
        public virtual void SetTotalCount(int totalCount) => this.TotalCount = totalCount;

        /// <summary>
        /// Set DataList
        /// </summary>
        /// <param name="dataList"></param>
        public virtual void SetDataList(IEnumerable<T> dataList)
        {
            this.DataList = dataList;
        }
    }
}
