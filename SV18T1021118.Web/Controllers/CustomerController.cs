using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021118.BusinessLayer;
using SV18T1021118.DataLayer;
using SV18T1021118.DomainModel;

namespace SV18T1021118.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Tìm kiếm hiển thị thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        // GET: Customer
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(page, pageSize, searchValue, out rowCount);
            Models.CustomerPaginationResult model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data
            };
            return View(model);
            //int pageSize = 10;
            //int rowCount = 0;
            //var model = CommonDataService.ListOfCustomers(page, pageSize, searchValue, out rowCount);
            //int pageCount = rowCount / pageSize;
            //if (rowCount % pageSize > 0)
            //    pageCount += 1;
            //ViewBag.PageCount = pageCount;
            //ViewBag.RowCount = rowCount;
            //ViewBag.SearchValue = searchValue;
            //ViewBag.CurrentPage = page;
            //return View(model);
        }

        /// <summary>
        /// Giao diện bổ sung một khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Giao diện chỉnh sửa thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return View();
        }

        /// <summary>
        /// Xác nhận có muốn xóa hay không
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return View();
        }
    }

}