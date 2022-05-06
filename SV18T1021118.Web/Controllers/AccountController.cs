using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            if (username == "admin@gmail.com" && password == "admin")
            {
                //Ghi cookie sau khi đăng nhập
                FormsAuthentication.SetAuthCookie(username, false);
                //Quay về trang chủ
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.username = username;
                ViewBag.Message = "Đăng nhập thất bại";
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
        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}