using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021118.DomainModel
{
    /// <summary>
    /// Khách hàng
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// ID
        /// </summary>
        public int CustomerID { get; set; }
        /// <summary>
        /// Ten khach hang
        /// </summary>
        public string CustomerName { get; set; }
        /// <summary>
        /// Ten giao dich
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Dia chi
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }
    }
}
