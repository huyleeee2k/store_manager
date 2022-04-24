using SV18T1021118.BusinessLayer;
using SV18T1021118.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021118.Web.Controllers
{
    /// <summary>
    /// Controller shipper
    /// </summary>
    [Authorize]
    [RoutePrefix("shipper")]
    public class ShipperController : Controller
    {
        /// <summary>
        /// Hiển thị giao diện người giao hàng
        /// </summary>
        /// <returns></returns>
        // GET: Shipper
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 5;
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
        /// Tạo mới một nhân viên giao hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Shipper model = new Shipper()
            {
                ShipperID = 0
            };
            return View(model);
        }
        /// <summary>
        /// Chỉnh sửa thông tin ncc
        /// </summary>
        /// <returns></returns>
        [Route("edit/{shipperID}")]
        public ActionResult Edit(int shipperId)
        {
            Shipper model = CommonDataService.GetShipper(shipperId);
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
        /// Xác nhận có muốn xóa hay không
        /// </summary>
        /// <returns></returns>
        [Route("delete/{shipperID}")]
        public ActionResult Delete(int shipperId)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteShipper(shipperId);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetShipper(shipperId);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}