using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021118.DomainModel
{
    /// <summary>
    /// Người giao hàng.
    /// </summary>
    public class Shipper
    {
        /// <summary>
        /// Ma nguoi giao hang
        /// </summary>
        public int ShipperID { get; set; }

        /// <summary>
        /// Ten nguoi giao hang
        /// </summary>
        public string ShipperName { get; set; }

        /// <summary>
        /// SDT
        /// </summary>
        public string Phone { get; set; }
    }
}
