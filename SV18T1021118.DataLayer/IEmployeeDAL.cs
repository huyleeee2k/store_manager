using SV18T1021118.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV18T1021118.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến nhân viên
    /// </summary>
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhân viên dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm (tên hoặc địa chỉ), tìm kiếm tương đối</param>
        /// Nếu đầu vào rỗng thì lấy toàn bộ dữ liệu
        /// <returns></returns>       
        IList<Employee> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm số nhân viên thỏa mãn tìm kiếm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lấy thông tin nhân viên dựa vào mã nhân viên
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        Employee Get(int EmployeeID);

        /// <summary>
        /// Bo sung mot nhân viên. Ham tra ve ma nhân viên duoc tra ve
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);

        /// <summary>
        /// Cap nhat thong tin nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Employee data);

        /// <summary>
        /// Xoa thong tin nhân viên
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        bool Delete(int EmployeeId);

        /// <summary>
        /// Kiểm tra xem một nhân viên có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="EmployeeId"></param>
        /// <returns></returns>
        bool InUsed(int EmployeeId);
    }
}
