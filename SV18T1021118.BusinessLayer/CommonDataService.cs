using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021118.DataLayer;
using SV18T1021118.DomainModel;
using System.Configuration;

namespace SV18T1021118.BusinessLayer
{
    /// <summary>
    /// Cung cấp các chức năng xử lý dữ liệu chung  
    /// </summary>
    public static class CommonDataService
    {
        private static readonly ICommonDAL<Category> categoryDB;
        private static readonly ICommonDAL<Customer> customerDB;
        private static readonly ICommonDAL<Supplier> supllierDB;
        private static readonly ICommonDAL<Shipper> shipperDB;
        private static readonly ICommonDAL<Employee> employeeDB;
        private static readonly ICommonDAL<Country> countryDB;
        private static readonly ICommonDAL<Product> productDB;
        private static readonly ICommonDAL<ProductAttribute> productAttributeDB;
        private static readonly ICommonDAL<ProductPhoto> productPhotoDB;
        static CommonDataService()
        {
            string provider = ConfigurationManager.ConnectionStrings["DB"].ProviderName;
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            switch (provider)
            {
                case "SQLServer":
                    categoryDB = new DataLayer.SQLServer.CategoryDAL(connectionString);
                    customerDB = new DataLayer.SQLServer.CustomerDAL(connectionString);
                    supllierDB = new DataLayer.SQLServer.SupplierDAL(connectionString);
                    shipperDB = new DataLayer.SQLServer.ShipperDAL(connectionString);
                    employeeDB = new DataLayer.SQLServer.EmployeeDAL(connectionString);
                    countryDB = new DataLayer.SQLServer.CountryDAL(connectionString);
                    productDB = new DataLayer.SQLServer.ProductDAL(connectionString);
                    productAttributeDB = new DataLayer.SQLServer.ProductAttributeDAL(connectionString);
                    productPhotoDB = new DataLayer.SQLServer.ProductPhotoDAL(connectionString);
                    break;
                default:
                    break;
            }
        }

        //public static ICategoryDAL CategoryDB { get => CategoryDB1; set => CategoryDB1 = value; }
        //public static ICategoryDAL CategoryDB1 { get => categoryDB; set => categoryDB = value; }
        //public static ICategoryDAL CategoryDB2 { get => categoryDB; set => categoryDB = value; }

        /// <summary>
        /// Lấy danh sách mặt hàng và phân trang
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="supplierName"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Product> ListOfProducts(string categoryName, string supplierName, int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = productDB.CountByValue(categoryName, supplierName, searchValue);
            return productDB.ListProducts(categoryName, supplierName, page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductAttribute> ListOfProductAttributes(int productID)
        {
            return productAttributeDB.List(productID).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return productAttributeDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProductAttribute(ProductAttribute data)
        {
            return productAttributeDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteProductAttribute(int id)
        {
            return productAttributeDB.Delete(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="attributeID"></param>
        /// <returns></returns>
        public static ProductAttribute GetProductAttribute(int attributeID)
        {
            return productAttributeDB.Get(attributeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        public static List<ProductPhoto> ListOfProductPhotos(int productID)
        {
            return productPhotoDB.List(productID).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProductPhoto(ProductPhoto data)
        {
            return productPhotoDB.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static ProductPhoto GetProductPhoto(int photoID)
        {
            return productPhotoDB.Get(photoID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoID"></param>
        /// <returns></returns>
        public static bool DeleteProductPhoto(int photoID)
        {
            return productPhotoDB.Delete(photoID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProductPhoto(ProductPhoto data)
        {
            return productPhotoDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddProduct(Product data)
        {
            return productDB.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteProduct(int productID)
        {
            if (productDB.InUsed(productID))
                return false;
            return productDB.Delete(productID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Product GetProduct(int productID)
        {
            return productDB.Get(productID);
        }

        /// <summary>
        /// Kiểm tra mat hang có liên kết trong csdl hay chưa
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUsedProduct(int productID)
        {
            return productDB.InUsed(productID);
        }

        /// <summary>
        /// Cập nhật mặt hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateProduct(Product data)
        {
            return productDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Customer> List()
        {
            return customerDB.List().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return categoryDB.Add(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Category> ListOfNameCategories()
        {
            return categoryDB.List().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return categoryDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            if (categoryDB.InUsed(categoryID))
                return false;
            return categoryDB.Delete(categoryID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return categoryDB.Get(categoryID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return categoryDB.InUsed(categoryID);
        }
        /// <summary>
        /// Trả về danh sách các quốc gia từ CSDL
        /// </summary>
        /// <returns></returns>
        public static List<Country> ListOfCoutries(int page = 1, int pageSize = 0, string searchValue = "")
        {
            return countryDB.List().ToList();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách mặt hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = categoryDB.Count(searchValue);
            return categoryDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = customerDB.Count(searchValue);
            return customerDB.List(page, pageSize, searchValue).ToList();           
        }
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        

        /// <summary>
        /// Thêm một khách hàng vào trong csdl
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return customerDB.Add(data);
        }
        /// <summary>
        /// Cập nhật khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return customerDB.Update(data);
        }

        /// <summary>
        /// Xóa một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            if (customerDB.InUsed(customerID))
                return false;
            return customerDB.Delete(customerID);
        }
        /// <summary>
        /// Trả về khách hàng dựa trên ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return customerDB.Get(customerID);
        }
        /// <summary>
        /// Kiểm tra khách hàng có liên kết trong csdl hay chưa
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return customerDB.InUsed(customerID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = supllierDB.Count(searchValue);
            return supllierDB.List(page, pageSize, searchValue).ToList();
        }

        public static List<Supplier> ListOfNameSuppliers()
        {
            return supllierDB.List().ToList();
        }

        /// <summary>
        /// Thêm một nha cung cap vào trong csdl
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return supllierDB.Add(data);
        }
        /// <summary>
        /// Cập nhật nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return supllierDB.Update(data);
        }

        /// <summary>
        /// Xóa một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int supplierID)
        {
            if (supllierDB.InUsed(supplierID))
                return false;
            return supllierDB.Delete(supplierID);
        }
        /// <summary>
        /// Trả về khách hàng dựa trên ID
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static Supplier GetSuppliers(int supplierID)
        {
            return supllierDB.Get(supplierID);
        }
        /// <summary>
        /// Kiểm tra khách hàng có liên kết trong csdl hay chưa
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static bool InUsedSupplier(int supplierId)
        {
            return supllierDB.InUsed(supplierId);
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách nguoi giao hang dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = shipperDB.Count(searchValue);
            return shipperDB.List(page, pageSize, searchValue).ToList();
        }

        /// <summary>
        /// Thêm một nha cung cap vào trong csdl
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return shipperDB.Add(data);
        }
        /// <summary>
        /// Cập nhật nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return shipperDB.Update(data);
        }

        /// <summary>
        /// Xóa một nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperId)
        {
            if (shipperDB.InUsed(shipperId))
                return false;
            return shipperDB.Delete(shipperId);
        }
        /// <summary>
        /// Trả về khách hàng dựa trên ID
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return shipperDB.Get(shipperID);
        }
        /// <summary>
        /// Kiểm tra khách hàng có liên kết trong csdl hay chưa
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return shipperDB.InUsed(shipperID);
        }

        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = employeeDB.Count(searchValue);
            return employeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public static int AddEmployee(Employee data)
        {
            return employeeDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return employeeDB.Update(data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            if (employeeDB.InUsed(employeeID))
                return false;
            return employeeDB.Delete(employeeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return employeeDB.Get(employeeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return employeeDB.InUsed(employeeID);
        }
    }
}
