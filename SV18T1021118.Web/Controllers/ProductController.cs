using System;
using System.Collections.Generic;
using System.IO;
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
            //Models.PaginationProductSearchInput model = Session["PRODUCT_SEARCH"] as Models.PaginationProductSearchInput;
            //if (model == null)
            //{
            //    model = new Models.PaginationProductSearchInput()
            //    {
            //        Page = 1,
            //        PageSize = 10,
            //        SearchValue = "",
            //        CategoryName = "",
            //        SupplierName = ""
            //    };
            //}
            //return View(model);
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
        /// <param name="input"></param>
        /// <returns></returns>
        public ActionResult Search(Models.PaginationProductSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfProducts(input.CategoryName, input.SupplierName, input.Page, input.PageSize, input.SearchValue, out rowCount);

            Models.ProductPaginationResults model = new Models.ProductPaginationResults()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue,
                RowCount = rowCount,
                CategoryName = input.CategoryName,
                SupllierName = input.SupplierName,
                Data = data
            };
            Session["PRODUCT_SEARCH"] = input;
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
            ProductPhoto model = new ProductPhoto();
            switch (method)
            {
                case "add":
                    model.PhotoID = 0;
                    ViewBag.Title = "Bổ sung ảnh";
                    break;
                case "edit":
                    model = CommonDataService.GetProductPhoto(photoID.Value);
                    if (model == null)
                        return RedirectToAction("Edit", new { productID = productID });
                    ViewBag.Title = "Thay đổi ảnh";
                    break;
                case "delete":
                    var mode = CommonDataService.GetProductPhoto(photoID.Value);
                    string fullPath = Server.MapPath("~/Images/ProductPhotos/" + mode.Photo);
                    if (System.IO.File.Exists(fullPath))
                        System.IO.File.Delete(fullPath);
                    CommonDataService.DeleteProductPhoto(photoID.Value);
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SavePhoto(ProductPhoto model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(model.Description))
                ModelState.AddModelError("Description", "Mô tả/ tiêu đề không được để trống.");
            if (model.DisplayOrder < 0)
                ModelState.AddModelError("DisplayOrder", "Thứ tự không bé hơn 0.");
            else
            {
                foreach (var item in CommonDataService.ListOfProductPhotos(model.ProductID))
                {
                    if (item.DisplayOrder == model.DisplayOrder && item.PhotoID != model.PhotoID)
                    {
                        ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị đã tồn tại.");
                        break;
                    }
                }
            }
            // upload file picture
            if (uploadPhoto != null)
            {
                string _FileName = DateTime.Now.Ticks + "-" + Path.GetFileName(uploadPhoto.FileName);
                string _path = Path.Combine(Server.MapPath("~/Images/ProductPhotos"), _FileName);
                uploadPhoto.SaveAs(_path);
                model.Photo = _FileName;
            }
            else if (model.PhotoID == 0) ModelState.AddModelError("Photo", "Ảnh không được để trống.");

            if (!ModelState.IsValid)
                return View("Photo", model);

            if (model.PhotoID == 0)
                CommonDataService.AddProductPhoto(model);
            else
            {
                if (uploadPhoto != null)
                {
                    string fullPath = Server.MapPath("~/Images/ProductPhotos/" + CommonDataService.GetProductPhoto(model.PhotoID).Photo);
                    if (System.IO.File.Exists(fullPath))
                        System.IO.File.Delete(fullPath);
                }
                CommonDataService.UpdateProductPhoto(model);
            }
            return RedirectToAction("Edit", new { productID = model.ProductID });
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
            ProductAttribute model = new ProductAttribute();
            switch (method)
            {
                case "add":
                    model.AttributeID = 0;
                    ViewBag.Title = "Bổ sung thuộc tính";
                    break;
                case "edit":
                    model = CommonDataService.GetProductAttribute(attributeID.Value);
                    if (model == null)
                        return RedirectToAction("Edit", new { productID = productID });
                    ViewBag.Title = "Thay đổi thuộc tính";
                    break;
                case "delete":
                    CommonDataService.DeleteProductAttribute(attributeID.Value);
                    return RedirectToAction("Edit", new { productID = productID });
                default:
                    return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="uploadPhoto"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveAttribute(ProductAttribute model, HttpPostedFileBase uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(model.AttributeName))
                ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống.");
            if (string.IsNullOrWhiteSpace(model.AttributeValue))
                ModelState.AddModelError("AttributeValue", "Giá trị thuộc tính không được để trống.");
            if (model.DisplayOrder < 0)
                ModelState.AddModelError("DisplayOrder", "Thứ tự không bé hơn 0.");
            else
            {
                foreach (var item in CommonDataService.ListOfProductAttributes(model.ProductID))
                {
                    if (item.DisplayOrder == model.DisplayOrder && item.AttributeID != model.AttributeID)
                    {
                        ModelState.AddModelError("DisplayOrder", "Thứ tự hiển thị đã tồn tại.");
                        break;
                    }
                }
            }
            if (!ModelState.IsValid)
                return View("Attribute", model);
            if (model.AttributeID == 0)
                CommonDataService.AddProductAttribute(model);
            else
                CommonDataService.UpdateProductAttribute(model);
            return RedirectToAction("Edit", new { productID = model.ProductID });
        }
    }
}