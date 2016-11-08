using System;
using System.Collections.Generic;

namespace WebApplication.Monitoring
{
    /// <summary>
    /// A DTO to return some paginated data
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedData<T>
    {
        /// <summary>
        /// total number of pages
        /// </summary>
        public int TotalPagesCount { get; set; }

        /// <summary>
        /// a single page of data
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        public PaginatedData(IEnumerable<T> data, int totalCount, int pageSize)
        {
            Data = data;
            TotalPagesCount = Math.Max(1, Convert.ToInt32(totalCount / pageSize));
        }
    }
}