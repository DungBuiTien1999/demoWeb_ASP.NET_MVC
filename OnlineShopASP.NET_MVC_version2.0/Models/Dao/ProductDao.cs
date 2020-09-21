using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ProductDao
    {
        OnlineShopDbContext_v2 db = null;
        public ProductDao()
        {
            db = new OnlineShopDbContext_v2();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }
        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null ).OrderByDescending(y => y.CreateDate).Take(top).ToList();//&& x.TopHot > DateTime.Now co the them vao
        }
    }
}
