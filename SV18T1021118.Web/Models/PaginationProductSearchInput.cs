using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021118.Web.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class PaginationProductSearchInput : PaginationSearchInput
    {
        public int CategoryID { get; set; }
        public int SupplierID { get; set; }
    }
}