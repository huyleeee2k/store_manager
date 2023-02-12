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
    [RoutePrefix("customer")]
    public class CustomerController : Controller
    {
        /// <summary>
        /// Giao diện để hiển thị tìm kiếm 
        /// </summary>
        /// <returns></returns>
        // GET: Customer
        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["CUSTOMER_SEARCH"] as Models.PaginationSearchInput;
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
        /// Thực hiện chức năng tìm kiếm phân trang
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(input.Page, input.PageSize, input.SearchValue, out rowCount);
            Models.CustomerPaginationResult model = new Models.CustomerPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["CUSTOMER_SEARCH"] = input;
            return View(model);
        }

        /// <summary>
        /// Giao diện bổ sung một khách hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Customer model = new Customer()
            {
                CustomerID = 0
            };           
            return View(model);
        }

        /// <summary>
        /// Giao diện chỉnh sửa thông tin khách hàng
        /// </summary>
        /// <returns></returns>
        [Route("edit/{customerID}")]
        public ActionResult Edit(int customerID)
        {
            Customer model = CommonDataService.GetCustomer(customerID);
            if (model == null)
                return RedirectToAction("Index");
            Session["CUSTOMER_NAME"] = "";
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Customer model)
        {
            //Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrWhiteSpace(model.CustomerName))
                ModelState.AddModelError("CustomerName", "Tên khách hàng không được để trống");

            if (string.IsNullOrWhiteSpace(model.ContactName))
                ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống");

            if (string.IsNullOrWhiteSpace(model.Address))
                ModelState.AddModelError("Address", "Địa chỉ không được để trống");

            if (string.IsNullOrWhiteSpace(model.Country))
                ModelState.AddModelError("Country", "Quốc gia không được để trống");

            if (string.IsNullOrWhiteSpace(model.City))
                model.City = "";

            if (string.IsNullOrWhiteSpace(model.PostalCode))
                model.PostalCode = "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CustomerID == 0 ? "Bổ sung khách hàng" : "Thay đổi khách hàng";
                return View("Create", model);
            }

            //Lưu dữ liệu
            if (model.CustomerID == 0)
            {
                CommonDataService.AddCustomer(model);
                Session["CUSTOMER_NAME"] = model.CustomerName;
            }
            else
                CommonDataService.UpdateCustomer(model);              
            
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xác nhận có muốn xóa hay không
        /// </summary>
        /// <returns></returns>
        [Route("delete/{customerID}")]
        public ActionResult Delete(int customerID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCustomer(customerID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCustomer(customerID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }

}