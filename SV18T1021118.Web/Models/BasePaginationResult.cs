using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021118.Web.Models
{
    /// <summary>
    /// Lớp cơ sở cho các lớp lưu trữ dữ liệu
    /// Liên quan đến phân trang
    /// </summary>
    public abstract class BasePaginationResult
    {
        /// <summary>
        /// Trang cần xem
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Số dòng trên mỗi trang
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SearchValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// Số lượng trang
        /// </summary>
        public int PageCount
        {
            get
            {
                int p = RowCount / PageSize;
                if (RowCount % PageSize > 0)
                    p += 1;
                return p;
            }
        }
    }
}