using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SV18T1021118.BusinessLayer;
using SV18T1021118.DomainModel;

namespace SV18T1021118.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class SelectListHelper
    {
        /// <summary>
        /// Danh sách quốc gia
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> Coutries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "---- Chọn quốc gia ----"
            });
            foreach (var c in CommonDataService.ListOfCoutries())
            {
                list.Add(new SelectListItem()
                {
                    Value = c.CountryName,
                    Text = c.CountryName
                });
            }
            return list;
        }

        public static List<SelectListItem> SuppliersName()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Nhà cung cấp --"
            });
            foreach (var c in CommonDataService.ListOfNameSuppliers())
            {
                list.Add(new SelectListItem()
                {
                    Value = c.SupplierName,
                    Text = c.SupplierName
                });
            }
            return list;
        }


        public static List<SelectListItem> CategoriesName()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem()
            {
                Value = "",
                Text = "-- Loại hàng --"
            });
            foreach (var c in CommonDataService.ListOfNameCategories())
            {
                list.Add(new SelectListItem()
                {
                    Value = c.CategoryName,
                    Text = c.CategoryName
                });
            }
            return list;
        }
        public static List<SelectListItem> Categories()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Loại hàng --",
            }); ;
            int rowCount = 0;
            foreach (var c in CommonDataService.ListOfCategories(1, 100, "", out rowCount))
            {
                list.Add(new SelectListItem()
                {
                    Value = c.CategoryID.ToString(),
                    Text = c.CategoryName
                });
            }

            return list;
        }
        public static List<SelectListItem> Suppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            list.Add(new SelectListItem()
            {
                Value = "0",
                Text = "-- Nhà cung cấp --",
            }); ;
            int rowCount = 0;
            foreach (var c in CommonDataService.ListOfSuppliers(1, 10, "", out rowCount))
            {
                list.Add(new SelectListItem()
                {
                    Value = c.SupplierID.ToString(),
                    Text = c.SupplierName
                });
            }

            return list;
        }
    }
}