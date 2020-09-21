using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dao
{
    public class UserDao
    {
        OnlineShopDbContext_v2 db = null;

        public UserDao()
        {
            db = new OnlineShopDbContext_v2();
        }

        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool DeleteByID(long id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<User> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pageSize);
        }

        public User GetByUsername(string userName)
        {
            return db.Users.SingleOrDefault(x=> x.Username == userName);
        }

        public User GetByID(long id)
        {
            return db.Users.Find(id);
        }

        public int Login(string userName, string passWord)
        {
            var result = db.Users.SingleOrDefault(x => x.Username == userName);
            if (result == null)
                return 0;//not found username (Account do not existed)
            else
            {
                if (result.Status == false)
                    return -1;//Account locked
                else
                {
                    if (result.Password == passWord)
                        return 1;//Login success
                    else
                        return -2;// Password wrong
                }
            }
        }

        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id);
            bool result;
            if(user.Status != null)
            {
                result = (bool)!user.Status;
                user.Status = !user.Status;
            }
            else
            {
                result = false;
                user.Status = false;
            }
            db.SaveChanges();
            return result;
        }
    }
}
