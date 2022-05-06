﻿using SV18T1021118.BusinessLayer;
using SV18T1021118.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021118.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("category")]
    public class CategoryController : Controller
    {
        /// <summary>
        /// Hiển thị loại hàng xoá loại hàng thêm loại hàng và chỉnh sửa loại hàng
        /// </summary>
        /// <returns></returns>
        ///

        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfCategories(page, pageSize, searchValue, out rowCount);
            Models.CategoryPaginationResults model = new Models.CategoryPaginationResults()
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
        /// Bổ sung loại hàng
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Category model = new Category()
            {
                CategoryID = 0
            };
            ViewBag.Title = "Bổ sung  loại hàng ";
            return View(model);
        }
        /// <summary>
        /// Chỉnh sửa loại hàng
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("edit/{categoryID}")]
        public ActionResult Edit(int categoryID)
        {
            Category model = CommonDataService.GetCategory(categoryID);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Thay đổi thông tin loaị hàng";
            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Category model)
        {
            if (string.IsNullOrWhiteSpace(model.CategoryName))
                ModelState.AddModelError("CategoryName", "Tên mặt hàng không được để trống");
            if (string.IsNullOrWhiteSpace(model.Description))
                model.Description = "";

            if (!ModelState.IsValid)
            {
                ViewBag.Title = model.CategoryID == 0 ? "Bổ sung mặt hàng" : "Thay đổi mặt hàng";
                return View("Create", model);
            }

            if (model.CategoryID == 0)
            {
                CommonDataService.AddCategory(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateCategory(model);
                return RedirectToAction("Index");
            }

        }
        /// <summary>
        /// Xoá Loại Hàng
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("delete/{categoryID}")]
        public ActionResult Delete(int categoryID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteCategory(categoryID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCategory(categoryID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}