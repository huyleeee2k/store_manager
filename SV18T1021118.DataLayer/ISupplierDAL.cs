using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021118.DomainModel;

namespace SV18T1021118.DataLayer
{
    /// <summary>
    /// Định nghĩa các phép xử lý liên quan đến nhà cung cấp
    /// </summary>
    public interface ISupllierDAL
    {
        /// <summary>
        /// Tìm kiếm và lấy danh sách nhà cung cấp dưới dạng phân trang
        /// </summary>
        /// <param name="page">Trang cần xem</param>
        /// <param name="pageSize">Số dòng trên mỗi trang</param>
        /// <param name="searchValue">Giá trị cần tìm (tên hoặc địa chỉ), tìm kiếm tương đối</param>
        /// Nếu đầu vào rỗng thì lấy toàn bộ dữ liệu
        /// <returns></returns>       
        IList<Supplier> List(int page, int pageSize, string searchValue);
        
        /// <summary>
        /// Đếm số nhà cung cấp thỏa mãn tìm kiếm
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        /// Lấy thông tin nhà cung cấp dựa vào mã nhà cung cấp
        /// </summary>
        /// <param name="SupplierID"></param>
        /// <returns></returns>
        Supplier Get(int SupplierID);
        
        /// <summary>
        /// Bo sung mot nhà cung cấp. Ham tra ve ma nhà cung cấp duoc tra ve
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Supplier data);
        
        /// <summary>
        /// Cap nhat thong tin nhà cung cấp
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Supplier data);
        
        /// <summary>
        /// Xoa thong tin nhà cung cấp
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        bool Delete(int SupplierId);
        
        /// <summary>
        /// Kiểm tra xem một nhà cung cấp có dữ liệu liên quan hay không?
        /// </summary>
        /// <param name="SupplierId"></param>
        /// <returns></returns>
        bool InUsed(int SupplierId);
    }
}
