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

        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfShippers(page, pageSize, searchValue, out rowCount);
            Models.ShipperPaginationResult model = new Models.ShipperPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                Data = data
            };

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
            ViewBag.Title = "Bổ sung  nguoi giao hang ";
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
            Console.Write(shipperID);
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
            //TODO: Kiểm tra dữ liệu đầu vào
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