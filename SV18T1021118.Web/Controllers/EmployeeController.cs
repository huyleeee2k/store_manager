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
    [RoutePrefix("employee")]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// hiển thị danh sách nhân viên, xoá nhân viên,edit nhân viên,thêm nhân viên
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(page, pageSize, searchValue, out rowCount);
            Models.EmployeePaginationResult model = new Models.EmployeePaginationResult
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
        /// Bổ sung nhân viên
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            Employee model = new Employee()
            {
                EmployeeID = 0
            };
            ViewBag.Title = "Bổ sung nhân viên ";
            return View(model);
        }
        /// <summary>
        /// Chỉnh sửa nhân viên
        /// </summary>
        /// <returns></returns>
        /// 
        /// 

        [Route("edit/{employeeID}")]
        public ActionResult Edit(int employeeID)
        {
            Employee model = CommonDataService.GetEmployee(employeeID);
            if (model == null)
                return RedirectToAction("Index");

            ViewBag.Title = "Thay đổi thông tin nhân viên";
            return View("Create", model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Save(Employee model, HttpPostedFileBase file)
        {
            if (model.EmployeeID == 0)
            {
                string path = "";
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/UploadFiles/") + file.FileName);
                    path = "/UploadFiles/" + file.FileName;
                }
                Console.WriteLine(path);
                model.Photo = path;
                CommonDataService.AddEmployee(model);
                return RedirectToAction("Index");
            }
            else
            {
                CommonDataService.UpdateEmployee(model);
                return RedirectToAction("Index");
            }

        }

        /// <summary>
        /// Xoá nhân viên
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("delete/{employeeID}")]
        public ActionResult Delete(int employeeID)
        {
            if (Request.HttpMethod == "POST")
            {
                CommonDataService.DeleteEmployee(employeeID);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(employeeID);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
    }
}