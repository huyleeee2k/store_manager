using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SV18T1021118.DataLayer;
using SV18T1021118.DomainModel;

namespace SV18T1021118.Web.Models
{
    public class ProductPaginationResults : BasePaginationResult
    {
        public List<Product> Data { get; set; }
        public int categoryID { get; set; }
        public int supplierID { get; set; }
    }
}