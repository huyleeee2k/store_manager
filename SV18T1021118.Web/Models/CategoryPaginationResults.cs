using SV18T1021118.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV18T1021118.Web.Models
{
    public class CategoryPaginationResults :BasePaginationResult
    {
        public List<Category> Data { get; set; }
    }
}