using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021118.DomainModel;

namespace SV18T1021118.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến người giao hàng
    /// </summary>
    public interface IShipperDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách người giao hàng dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm (tên hoặc địa chỉ), tìm kiếm tương đối</param>
        /// Nếu đầu vào rỗng thì lấy toàn bộ dữ liệu
        /// <returns></returns>       
        IList<Shipper> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm số người giao hàng thỏa mãn tìm kiếm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lấy thông tin người giao hàng dựa vào mã người giao hàng
        /// </summary>
        /// <param name="ShipperID"></param>
        /// <returns></returns>
        Shipper Get(int ShipperID);

        /// <summary>
        /// Bo sung mot người giao hàng. Ham tra ve ma người giao hàng duoc tra ve
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);

        /// <summary>
        /// Cap nhat thong tin người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);

        /// <summary>
        /// Xoa thong tin người giao hàng
        /// </summary>
        /// <param name="ShipperId"></param>
        /// <returns></returns>
        bool Delete(int ShipperId);

        /// <summary>
        /// Kiểm tra xem một người giao hàng có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="ShipperId"></param>
        /// <returns></returns>
        bool InUsed(int ShipperId);
    }
}
