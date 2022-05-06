using SV18T1021118.BusinessLayer;
using SV18T1021118.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021118.Web.Controllers
{
    [Authorize]
    [RoutePrefix("shipper")]
    public class ShipperController : Controller
    {
        /// <summary>
        /// Tim kiem,hien thi và xoá danh sách
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult Index()
        {
            Models.PaginationSearchInput model = Session["SHIPPER_SEARCH"] as Models.PaginationSearchInput;
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

        public ActionResult Search(Models.PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(input.Page, input.PageSize, input.SearchValue, out rowCount);
            Models.ShipperPaginationResult model = new Models.ShipperPaginationResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                Data = data
            };
            Session["SHIPPER_SEARCH"] = input;
            return View(model);
        }

        /// <summary>
        /// Bổ sung người giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
            ViewBag.Title = "Bổ sung nguoi giao hang";
            return View(model);
        }

        
        /// <summary>
        /// Chỉnh sửa người giao hàng
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("edit/{shipperID}")]
        public ActionResult Edit(int shipperID)
        {
            Shipper model = CommonDataService.GetShipper(shipperID);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Thay đổi thông tin nguoi giao hang";
            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public ActionResult Save(Shipper model)
        {
            if (string.IsNullOrWhiteSpace(model.ShipperName))
                ModelState.AddModelError("ShipperName", "Tên shipper không được để trống");

            if (string.IsNullOrWhiteSpace(model.Phone))
                ModelState.AddModelError("Phone", "SĐT không được để trống");

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.ShipperID == 0 ? "Bổ sung người giao hàng" : "Thay đổi người giao hàng";
                return View("Create", model);
            }

            if (model.ShipperID == 0)
            {
                CommonDataService.AddShipper(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateShipper(model);
                return RedirectToAction("Index");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("delete/{shipperID}")]
        public ActionResult Delete(int shipperID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteShipper(shipperID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetShipper(shipperID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }


    }
}