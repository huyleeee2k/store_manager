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
    /// 
    /// </summary>
    public class EmployeePaginationResult : BasePaginationResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Employee> Data { get; set; }
    }
}