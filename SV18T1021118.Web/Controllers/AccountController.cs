using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SV18T1021118.BusinessLayer;
using SV18T1021118.DomainModel;
using SV18T1021118.DataLayer;

namespace SV18T1021118.Web.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Account
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            //TODO: Thay đổi code để kiểm tra đúng tài khoản
            if (CommonDataService.IsValidUser(username, password))
            {
                //Ghi cookie sau khi đăng nhập
                FormsAuthentication.SetAuthCookie(username, false);
                //Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (username == "" || password == "")                              
                    ViewBag.Message = "Tài khoản và mật khẩu không được để trống";               
                else
                    ViewBag.Message = "Tài khoản hoặc mật khẩu không đúng";

                ViewBag.username = username;                
                return View();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passwordOld"></param>
        /// <param name="passwordNew"></param>
        /// <param name="passwordNewRepeat"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangePassword(string passwordOld, string passwordNew, string passwordNewRepeat)
        {
            if (string.IsNullOrWhiteSpace(passwordOld))
                ModelState.AddModelError("passwordOld", "Mật khẩu cũ không được bỏ trống");
            if (!CommonDataService.IsValidUser(User.Identity.Name, passwordOld))
                ModelState.AddModelError("passwordOld", "Mật khẩu cũ không đúng");

            if (string.IsNullOrWhiteSpace(passwordNew))
                ModelState.AddModelError("passwordNew", "Mật khẩu mới không được bỏ trống");

            if (string.IsNullOrWhiteSpace(passwordNewRepeat))
                ModelState.AddModelError("passwordNewRepeat", "Mật khẩu mới không được bỏ trống");

            if (passwordNew != passwordNewRepeat)
                ModelState.AddModelError("passwordNewRepeat", "Mật khẩu mới phải trùng nhau");

            if (!ModelState.IsValid)
            {
                ViewBag.passwordOld = passwordOld;
                ViewBag.passwordNew = passwordNew;
                ViewBag.passwordNewRepeat = passwordNewRepeat;
                return View();
            }

            if (CommonDataService.ChangePassword(User.Identity.Name, passwordNew))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
    }
}