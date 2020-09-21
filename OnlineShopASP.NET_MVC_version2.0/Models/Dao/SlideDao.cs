using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class SlideDao
    {
        OnlineShopDbContext_v2 db = null;
        public SlideDao()
        {
            db = new OnlineShopDbContext_v2();
        }
        public List<Slider> ListAll()
        {
            return db.Sliders.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }
    }
}
