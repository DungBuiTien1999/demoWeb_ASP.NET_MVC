using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class ContentDao
    {
        OnlineShopDbContext_v2 db = null;

        public ContentDao()
        {
            db = new OnlineShopDbContext_v2();
        }

        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public bool Update(Content entity)
        {
            try
            {
                var content = GetByID(entity.ID);
                content = entity;
                content.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public long Insert(Content entity)
        {
            entity.CreateDate = DateTime.Now;
            db.Contents.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool DeleteByID(long id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<Content> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }
    }
}
