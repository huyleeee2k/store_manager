using SV18T1021118.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021118.BusinessLayer;
using SV18T1021118.DataLayer;

namespace SV18T1021118.Web.Models
{
    /// <summary>
    /// Kết quả tìm kiếm phân trang của khách hàng
    /// </summary>
    public class ShipperPaginationResult : BasePaginationResult
    {
        public List<Shipper> Data { get; set; }
    }
}