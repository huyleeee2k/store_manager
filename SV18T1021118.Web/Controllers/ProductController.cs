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
    [RoutePrefix("product")]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string categoryName = "", string supplierName = "", int page = 1, string searchValue = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            var data = CommonDataService.ListOfProducts(categoryName, supplierName, page, pageSize, searchValue, out rowCount);

            Models.ProductPaginationResults model = new Models.ProductPaginationResults()
            {
                Page = page,
                PageSize = pageSize,
                SearchValue = searchValue,
                RowCount = rowCount,
                CategoryName = categoryName,
                SupllierName = supplierName,
                Data = data
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Product model = new Product()
            {
                ProductID = 0
            };
            ViewBag.Title = "Bổ sung mặt hàng";
            return View(model);
        }

        [HttpPost]
        public ActionResult Save(Product model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(model.ProductName))
                ModelState.AddModelError("ProductName", "Tên sản phẩm không được để trống");
            if (string.IsNullOrWhiteSpace(model.Price.ToString()))
                ModelState.AddModelError("Price", "Giá không được để trống");
            if (string.IsNullOrWhiteSpace(model.Unit))
                ModelState.AddModelError("Unit", "Đơn vị tính không được để trống");

            if (!ModelState.IsValid)
            {       
                return View("Create", model);
            }

            if (uploadPhoto != null)
            {
                string path = Server.MapPath("~/Images/Products");
                string fileName = $"{DateTime.Now.Ticks} - {uploadPhoto.FileName}";
                string filePath = System.IO.Path.Combine(path, fileName);
                uploadPhoto.SaveAs(filePath);
                model.Photo = fileName;
            }
            if (model.Photo == null)
                model.Photo = "";

            if (model.ProductID == 0)
            {
                CommonDataService.AddProduct(model);
            }
            else
            {
                CommonDataService.UpdateProduct(model);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("edit/{productID}")]
        public ActionResult Edit(int productID, HttpPostedFileBase uploadPhoto)
        {
            Product model = CommonDataService.GetProduct(productID);
            if (model == null)
                return RedirectToAction("Index");
        
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        [Route("delete/{productID}")]
        public ActionResult Delete(int productID)
        {
            if (Request.HttpMethod == "POST")
            {
                if (CommonDataService.DeleteProduct(productID))
                    return RedirectToAction("Index");
            }
            var model = CommonDataService.GetProduct(productID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="photoID"></param>
        /// <returns></returns>
        [Route("photo/{method}/{productID}/{photoID?}")]
        public ActionResult Photo(string method, int productID, int? photoID)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh";
                    break;
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh";
                    break;
                case "delete":
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productID"></param>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        [Route("attribute/{method}/{productID}/{attributeID?}")]
        public ActionResult Attribute(string method, int productID, int? attributeID)
        {
            switch (method)
            {
                case "add":
                    ProductAttribute model = new ProductAttribute()
                    {
                        AttributeID = 0                      
                    };
                    model.ProductID = productID;
                    if (string.IsNullOrWhiteSpace(model.AttributeName))
                        ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống");
                    if (string.IsNullOrWhiteSpace(model.AttributeValue))
                        ModelState.AddModelError("AttributeValue", "Giá trị thuộc tính không được để trống");
                    if (string.IsNullOrWhiteSpace(model.DisplayOrder.ToString()))
                        ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị không được để trống");

                    if (!ModelState.IsValid)
                    {
                        return View(model);
                    }
                    ViewBag.Title = "Bổ sung thuộc tính";
                    return View(model);
                case "edit":
                    ViewBag.Title = "Thay đổi thuộc tính";
                    break;
                case "delete":
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View();
        }

        //[HttpPost]
        //public ActionResult Save(ProductAttribute model, int productID)
        //{
        //    if (string.IsNullOrWhiteSpace(model.AttributeName))
        //        ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống");
        //    if (string.IsNullOrWhiteSpace(model.AttributeValue))
        //        ModelState.AddModelError("AttributeValue", "Giá trị thuộc tính không được để trống");
        //    if (string.IsNullOrWhiteSpace(model.DisplayOrder.ToString()))
        //        ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị không được để trống");

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    if (model.AttributeID == 0)
        //    {
        //        CommonDataService.AddProductAttribute(model);
        //    }
        //    else
        //    {
        //        //TODO: Update
        //    }
        //    return RedirectToAction("product/edit", new { productID = productID });
        //}
    }
}