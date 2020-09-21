using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class CategoryDao
    {
        OnlineShopDbContext_v2 db = null;

        public CategoryDao()
        {
            db = new OnlineShopDbContext_v2();
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public IEnumerable<Category> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public long Insert(Category entity)
        {
            db.Categories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }

        public bool Update(Category entity)
        {
            try
            {
                var model = db.Categories.Find(entity.ID);
                model.Name = entity.Name;
                model.ShowOnHome = entity.ShowOnHome;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = entity.ModifiedBy;
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
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
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
            var category = db.Categories.Find(id);
            bool result;
            if (category.Status != null)
            {
                result = (bool)!category.Status;
                category.Status = !category.Status;
            }
            else
            {
                result = false;
                category.Status = false;
            }
            db.SaveChanges();
            return result;
        }
    }
}
