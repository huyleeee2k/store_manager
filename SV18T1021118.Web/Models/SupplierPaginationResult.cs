using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021118.DomainModel;

namespace SV18T1021118.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm và phân trang nhà cung cấp
    /// </summary>
	public class SupplierPaginationResult: BasePaginationResult
    {
        /// <summary>
        /// List supplier
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}