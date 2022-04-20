using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace SV18T1021118.DataLayer.SQLServer
{
    /// <summary>
    /// Lớp cơ sở cho các lớp xử lý dữ liệu trên SQL
    /// </summary>
    public abstract class _BaseDAL
    {
        protected string _connectionString;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionString"></param>
        public _BaseDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Tao va mo ket noi den SQL
        /// </summary>
        /// <returns></returns>
        protected SqlConnection OpenConnection()
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = _connectionString;
            cn.Open();
            return cn;
        }
    }
}
