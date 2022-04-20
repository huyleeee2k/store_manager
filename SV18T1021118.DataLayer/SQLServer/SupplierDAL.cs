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
    public class SupplierDAL : _BaseDAL, ISupllierDAL
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        public Supplier Get(int SupplierID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        public bool InUsed(int SupplierId)
        {
            throw new NotImplementedException();
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
                                    WHERE t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize";
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
            throw new NotImplementedException();
        }
    }
}
