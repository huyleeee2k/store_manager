using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021118.Web.Models
{
    /// <summary>
    /// Dữ liệu đầu vào cho tìm kiếm, phân trang
    /// </summary>
    public class PaginationSearchInput
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SearchValue { get; set; }

    }
}