using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class AboutDao
    {
        OnlineShopDbContext_v2 db = null;
        public AboutDao()
        {
            db = new OnlineShopDbContext_v2();
        }

        public IEnumerable<About> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<About> model = db.Abouts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public long Insert(About entity)
        {
            db.Abouts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public About GetByID(long id)
        {
            return db.Abouts.Find(id);
        }

        public bool Update(About entity)
        {
            try
            {
                var about = db.Abouts.Find(entity.ID);
                about.Name = entity.Name;
                about.Description = entity.Description;
                about.Detail = entity.Detail;
                about.ModifiedDate = DateTime.Now;
                about.ModifiedBy = entity.ModifiedBy;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteByID(long id)
        {
            try
            {
                var about = db.Abouts.Find(id);
                db.Abouts.Remove(about);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeStatus(long id)
        {
            var about = db.Abouts.Find(id);
            bool result;
            if (about.Status != null)
            {
                result = (bool)!about.Status;
                about.Status = !about.Status;
            }
            else
            {
                result = false;
                about.Status = false;
            }
            db.SaveChanges();
            return result;
        }
    }
}
