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
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["SUPPLIER_SEARCH"] as Models.PaginationSearchInput;
            if (model == null)
            {
                model = new Models.PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = 10,
                    SearchValue = ""
                };
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfSuppliers(input.Page, input.PageSize, input.SearchValue, out rowCount);
            Models.SupplierPaginationResult model = new Models.SupplierPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data
            };               
            Session["SUPPLIER_SEARCH"] = input;
            return View(model);
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
            Session["SUPPLIER_NAME"] = "";
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
            Session["SUPPLIER_NAME"] = "";
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
            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(model.SupplierName))
                ModelState.AddModelError("SupplierName", "Tên nhà cung cấp không được để trống");
            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");
            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Địa chỉ không được để trống");
            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Quốc gia không được để trống");
            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "SĐT không được để trống");

            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";

            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.SupplierID == 0 ? "Bổ sung nhà cung cấp" : "Thay đổi nhà cung cấp";
                return View("Create", model);
            }
            //Lưu dữ liệu
            if (model.SupplierID == 0)
            {
                CommonDataService.AddSupplier(model);
                Session["SUPPLIER_NAME"] = model.SupplierName;
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