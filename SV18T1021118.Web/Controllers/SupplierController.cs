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
    [RoutePrefix("supplier")]
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
            Supplier model = new Supplier()
            {
                SupplierID = 0
            };
            return View(model);
        }
        /// <summary>
        /// Chỉnh sửa thông tin ncc
        /// </summary>
        /// <returns></returns>
        [Route("edit/{supplierId}")]
        public ActionResult Edit(int supplierID)
        {
            Supplier model = CommonDataService.GetSuppliers(supplierID);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Supplier model)
        {
            //TODO: Kiểm tra dữ liệu đầu vào

            if (model.SupplierID == 0)
            {
                CommonDataService.AddSupplier(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateSupplier(model);
                return RedirectToAction("Index");
            }
        }
        /// <summary>
        /// Xác nhận có muốn xóa hay không
        /// </summary>
        /// <returns></returns>
        [Route("delete/{supplierID}")]
        public ActionResult Delete(int SupplierID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteSupplier(SupplierID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSuppliers(SupplierID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }

}