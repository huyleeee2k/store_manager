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
    public class SupplierController : Controller
    {
        /// <summary>
        /// Hiển thị giao diện nhà cung cấp
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        // GET: Supplier
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(page, pageSize, searchValue, out rowCount);
            Models.SupplierPaginationResult model = new Models.SupplierPaginationResult()
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
            //var model = CommonDataService.ListOfSupplier(page, pageSize, searchValue, out rowCount);
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
        /// Tạo mới nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Chỉnh sửa thông tin ncc
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