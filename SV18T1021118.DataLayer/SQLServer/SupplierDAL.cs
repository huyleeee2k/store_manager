using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021118.DomainModel;
using System.Data;

namespace SV18T1021118.DataLayer.SQLServer
{
    /// <summary>
    /// Xử lý dữ liệu từ database của nhà cung cấp
    /// </summary>
    public class SupplierDAL : _BaseDAL, ICommonDAL<Supplier>
    {
        /// <summary>
        /// contructor
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString) : base(connectionString)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Supplier data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Suppliers (SupplierName,ContactName,Address,City,PostalCode,Country,Phone)
                                        values (@SupplierName,@contactName,@address,@city,@postalCode,@country,@Phone)
                                        select scope_identity()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@CustomerName", data.SupplierID);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                ///
                result = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            int count = 0;

            searchValue = (searchValue != "") ? "%" + searchValue + "%" : searchValue;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT COUNT(*)
                                    FROM Suppliers
                                    WHERE (@searchValue = N'')
                                        OR    (
                                                (SupplierName LIKE @searchValue)
                                                OR (ContactName LIKE @searchValue)
                                                OR (Address LIKE @searchValue)
                                                OR (Phone LIKE @searchValue)
                                              )";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@searchValue", searchValue);
                count = Convert.ToInt32(cmd.ExecuteScalar());
                cn.Close();
            }
            return count;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        public bool Delete(int SupplierId)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Suppliers WHERE SupplierId = @SupplierId";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public Supplier Get(int SupplierID)
        {
            Supplier result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Suppliers WHERE SupplierID=@SupplierID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@SupplierID", SupplierID);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Supplier()
                    {
                        SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                        SupplierName = Convert.ToString(dbReader["SupplierName"]),
                        ContactName = Convert.ToString(dbReader["ContactName"]),
                        Address = Convert.ToString(dbReader["Address"]),
                        City = Convert.ToString(dbReader["City"]),
                        PostalCode = Convert.ToString(dbReader["PostalCode"]),
                        Country = Convert.ToString(dbReader["Country"]),
                        Phone = Convert.ToString(dbReader["Phone"])                        
                    };
                }
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        public bool InUsed(int SupplierId)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE WHEN EXISTS(SELECT * FROM Products WHERE SupplierId = @SupplierId) THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierId", SupplierId);
                result = Convert.ToBoolean(cmd.ExecuteScalar());
                cn.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public IList<Supplier> List(int page, int pageSize, string searchValue)
        {
            List<Supplier> data = new List<Supplier>();
            searchValue = searchValue != "" ? "%" + searchValue + "%" : searchValue;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                    (
                                        SELECT ROW_NUMBER() OVER(ORDER BY SupplierName) AS RowNumber, *
                                        FROM Suppliers
                                        WHERE (@searchValue = N'')
                                             OR (
                                                    (SupplierName LIKE @searchValue)
                                                 OR (ContactName LIKE @searchValue)
                                                 OR (Address LIKE @searchValue)
                                                 OR (Phone LIKE @searchValue)
                                                )
                                    ) AS t
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                
                //Add param
                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new Supplier()
                    {
                        SupplierID = Convert.ToInt32(result["SupplierID"]),
                        SupplierName = Convert.ToString(result["SupplierName"]),
                        ContactName = Convert.ToString(result["ContactName"]),
                        Address = Convert.ToString(result["Address"]),
                        City = Convert.ToString(result["City"]),
                        Country = Convert.ToString(result["Country"]),
                        Phone = Convert.ToString(result["Phone"]),
                        PostalCode = Convert.ToString(result["PostalCode"]),
                    });
                }
                result.Close();
                connection.Close();
            }
            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Supplier data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update Suppliers
                                    set SupplierName=@SupplierName,
                                        ContactName=@contactName,
                                        Address=@address,
                                        City=@city,
                                        PostalCode=@postalCode,
                                        Country=@country,
                                        Phone = @Phone    
                                        where SupplierID=@SupplierID";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@SupplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@ContactName", data.ContactName);
                cmd.Parameters.AddWithValue("@Address", data.Address);
                cmd.Parameters.AddWithValue("@City", data.City);
                cmd.Parameters.AddWithValue("@PostalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@Country", data.Country);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@SupplierID", data.SupplierID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// Lấy danh sách nhà cung cấp
        /// </summary>
        /// <returns></returns>
        public IList<Supplier> List()
        {
            List<Supplier> data = new List<Supplier>();
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT SupplierName FROM Suppliers";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dbReader.Read())
                {
                    data.Add(new Supplier()
                    {
                        SupplierName = Convert.ToString(dbReader["SupplierName"])
                    });
                }
                dbReader.Close();
                cn.Close();
            }
            return data;
        }

        public IList<Supplier> ListProducts(string categoryName, string supplierName, int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        public int CountByValue(string categoryName, string supplierName, string searchValue)
        {
            throw new NotImplementedException();
        }

        public IList<Supplier> List(int id)
        {
            throw new NotImplementedException();
        }
    }
}
