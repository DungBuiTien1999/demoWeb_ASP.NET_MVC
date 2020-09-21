using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.EF;

namespace Models.Dao
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext_v2 db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext_v2();
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
