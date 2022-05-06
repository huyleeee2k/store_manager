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
    /// Xử lý dữ liệu tử database
    /// </summary>
    public class ShipperDAL : _BaseDAL, ICommonDAL<Shipper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public ShipperDAL(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Shipper data)
        {
            int result = 0;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"insert into Shippers (ShipperName,Phone)
                                        values (@ShipperName,@Phone)
                                        select scope_identity()";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
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
                                    FROM Shippers
                                    WHERE (@searchValue = N'')
                                        OR    (
                                                (ShipperName LIKE @searchValue)
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

        public int CountByValue(string categoryName, string supplierName, string searchValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShipperId"></param>
        /// <returns></returns>
        public bool Delete(int ShipperId)
        {
            bool result = false;
            using (SqlConnection connection = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"DELETE FROM Shippers WHERE ShipperId = @ShipperId";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;
                cmd.Parameters.AddWithValue("@ShipperId", ShipperId);
                result = cmd.ExecuteNonQuery() > 0;
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        public Shipper Get(int ShipperID)
        {
            Shipper result = null;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Shippers WHERE ShipperID=@ShipperID";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@ShipperID", ShipperID);

                var dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (dbReader.Read())
                {
                    result = new Shipper()
                    {
                        ShipperID = Convert.ToInt32(dbReader["ShipperID"]),
                        ShipperName = Convert.ToString(dbReader["ShipperName"]),
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
        /// <param name="ShipperId"></param>
        /// <returns></returns>
        public bool InUsed(int ShipperId)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT CASE WHEN EXISTS(SELECT * FROM Orders WHERE ShipperId = @ShipperId) THEN 1 ELSE 0 END";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ShipperId", ShipperId);
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
        public IList<Shipper> List(int page, int pageSize, string searchValue)
        {
            List<Shipper> data = new List<Shipper>();

            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            //searchValue = (searchValue != "") ? "%" + searchValue + "%" : searchValue;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                    (
                                        SELECT ROW_NUMBER() OVER(ORDER BY ShipperName) AS RowNumber, *
                                        FROM Shippers
                                        WHERE (@searchValue = N'')
                                             OR (
                                                    (ShipperName LIKE @searchValue)
                                                 OR (Phone LIKE @searchValue)
                                                )
                                    ) AS t
                                    WHERE (@PageSize = 0) OR (t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize)";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new Shipper()
                    {
                        ShipperID = Convert.ToInt32(result["ShipperID"]),
                        ShipperName = Convert.ToString(result["ShipperName"]),
                        Phone = Convert.ToString(result["Phone"]),
                    });
                }
                result.Close();
                cn.Close();
            }

            return data;
        }

        public IList<Shipper> List()
        {
            throw new NotImplementedException();
        }

        public IList<Shipper> List(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Shipper> ListProducts(string categoryName, string supplierName, int page = 1, int pageSize = 0, string searchValue = "")
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Shipper data)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"Update Shippers
                                    set ShipperName=@ShipperName,
                                        Phone = @Phone    
                                        where ShipperId=@ShipperId";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@ShipperName", data.ShipperName);
                cmd.Parameters.AddWithValue("@Phone", data.Phone);
                cmd.Parameters.AddWithValue("@ShipperID", data.ShipperID);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}

