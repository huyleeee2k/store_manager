using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021118.DomainModel
{
    //Nhà cung cấp 
    public class Supplier
    {
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public int SupplierID { get; set; }

        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// Tên giao dịch
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Tên thành phố
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Mã bưa điện
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Điện thoại
        /// </summary>
        public string Phone { get; set; }
    }
}
