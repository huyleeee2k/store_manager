using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SV18T1021118.DomainModel;

namespace SV18T1021118.DataLayer.FakeDB
{
    public class CategoryDAL : ICategoryDAL
    {
        public int Add(Category data)
        {
            throw new NotImplementedException();
        }

        public int Count(string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int categoryID)
        {
            throw new NotImplementedException();
        }

        public Category Get(int categoryID)
        {
            throw new NotImplementedException();
        }

        public bool InUsed(int CategoryId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<Category> List()
        {
            List<Category> data = new List<Category>();
            data.Add(new Category() {
                CategoryID = 1,
                CategoryName = "Mỹ phẩm",
                Description = "Sản phẩm dành cho chị em phụ nữ"
            });
            data.Add(new Category()
            {
                CategoryID = 2,
                CategoryName = "Mỹ phẩm 1",
                Description = "Sản phẩm dành cho chị em phụ nữ 1"
            });
            data.Add(new Category()
            {
                CategoryID = 3,
                CategoryName = "Mỹ phẩm 2",
                Description = "Sản phẩm dành cho chị em phụ nữ 2"
            });
            return data;
        }

        public IList<Category> List(int page, int pageSize, string searchValue)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category data)
        {
            throw new NotImplementedException();
        }
    }
}
