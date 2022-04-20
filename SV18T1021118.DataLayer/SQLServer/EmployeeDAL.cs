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
    public class EmployeeDAL : _BaseDAL, IEmployeeDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public EmployeeDAL(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Employee data)
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
                                    FROM Employees
                                    WHERE (@searchValue = N'')
                                        OR    (
                                                (FirstName LIKE @searchValue)
                                                OR (LastName LIKE @searchValue)
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
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public bool Delete(int EmployeeId)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public Employee Get(int EmployeeID)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        public bool InUsed(int EmployeeId)
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
        public IList<Employee> List(int page, int pageSize, string searchValue)
        {
            List<Employee> data = new List<Employee>();

            //if (searchValue != "")
            //    searchValue = "%" + searchValue + "%";
            searchValue = (searchValue != "") ? "%" + searchValue + "%" : searchValue;

            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM
                                    (
                                        SELECT ROW_NUMBER() OVER(ORDER BY FirstName) AS RowNumber, *
                                        FROM Employees
                                        WHERE (@searchValue = N'')
                                             OR (
                                                    (FirstName LIKE @searchValue)
                                                 OR (LastName LIKE @searchValue)                                                
                                                )
                                    ) AS t
                                    WHERE t.RowNumber BETWEEN (@page - 1) * @pageSize + 1 AND @page * @pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                var result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (result.Read())
                {
                    data.Add(new Employee()
                    {
                        EmployeeID = Convert.ToInt32(result["EmployeeID"]),
                        LastName = Convert.ToString(result["LastName"]),
                        FirstName = Convert.ToString(result["FirstName"]),
                        BirthDate = Convert.ToDateTime(result["BirthDate"]),
                        Photo = Convert.ToString(result["Photo"]),
                        Notes = Convert.ToString(result["Notes"]),
                        Email = Convert.ToString(result["Email"]),
                        Password = Convert.ToString(result["Password"]),
                    });
                }
                result.Close();
                cn.Close();
            }

            return data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Employee data)
        {
            throw new NotImplementedException();
        }
    }
}

