﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SV18T1021118.Web.Controllers
{
    public class FileUploadController : Controller
    {
        // GET: FileUpload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {


                    if (file != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/UploadFiles"), Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                    }
                    ViewBag.FileStatus = "File uploaded successfully.";
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                }

            }
            return View("Index");
        }
    }
}