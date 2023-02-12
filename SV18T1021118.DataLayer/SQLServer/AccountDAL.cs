using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SV18T1021118.DataLayer.SQLServer
{
    public class AccountDAL : _BaseDAL
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public AccountDAL(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Kiểm tra xem tài khoản và mật khẩu đúng hay không?
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool IsValidUser(string email, string password)
        {
            bool result = false;
            using(SqlConnection con = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT * FROM Employees
                                    WHERE Email = @email and Password = @password";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                result = Convert.ToBoolean(cmd.ExecuteScalar());
                con.Close();
            }
            return result;
        }

        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ChangePassword(string email, string password)
        {
            bool result = false;
            using (SqlConnection cn = OpenConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"UPDATE Employees
                                    SET Password = @Password
                                    WHERE Email = @Email";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = cn;
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Email", email);

                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }
            return result;
        }
    }
}
