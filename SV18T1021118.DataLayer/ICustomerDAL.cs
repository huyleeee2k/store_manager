using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021118.DomainModel;

namespace SV18T1021118.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến khách hàng
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách khách hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm (tên hoặc địa chỉ), tìm kiếm tương đối</param>
        /// Nếu đầu vào rỗng thì lấy toàn bộ dữ liệu
        /// <returns></returns>
        IList<Customer> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// Đếm số khách hàng thỏa mãn tìm kiếm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lấy thông tin khách hàng dựa vào mã khách hàng
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        Customer Get(int customerID);
        /// <summary>
        /// Bo sung mot khach hang. Ham tra ve ma khach hang duoc tra ve
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Customer data);
        /// <summary>
        /// Cap nhat thong tin khach hang
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);
        /// <summary>
        /// Xoa thong tin khach hang
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        bool Delete(int customerId);
        /// <summary>
        /// Kiểm tra xem một khách hàng có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        bool InUsed(int customerId);
    }
}
