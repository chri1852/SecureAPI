using SecureAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureAPI.DAL
{
    public static class UserDAL
    {
        public static void SaveNewUser(User user)
        {
            using (SecureAPIDb db = new SecureAPIDb())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public static void UpdateUser(User user)
        {
            using (SecureAPIDb db = new SecureAPIDb())
            {
                if (db.Users.Where(u => u.Id == user.Id).Count() > 0)
                {
                    db.Users.Update(user);
                    db.SaveChanges();
                }
            }
        }

        public static void DeleteUser(User user)
        {
            using (SecureAPIDb db = new SecureAPIDb())
            {
                if (db.Users.Where(u => u.Id == user.Id).Count() > 0)
                {
                    db.Users.Remove(db.Users.First(u => u.Id == user.Id));
                    db.SaveChanges();
                }
            }
        }

        public static int GetEmailAddressCount(string email)
        {
            using (SecureAPIDb db = new SecureAPIDb())
            {
                return db.Users.Count(u => u.Email == email);
            }
        }

        public static int GetUsernameCount(string username)
        {
            using (SecureAPIDb db = new SecureAPIDb())
            {
                return db.Users.Count(u => u.Username == username);
            }
        }

        public static User GetUserByEmailAddress(string email)
        {
            using (SecureAPIDb db = new SecureAPIDb())
            {
                return db.Users.FirstOrDefault(u => u.Email == email);
            }
        }

        public static User GetUserByUsername(string username)
        {
            using (SecureAPIDb db = new SecureAPIDb())
            {
                return db.Users.FirstOrDefault(u => u.Username == username);
            }
        }

        public static User GetUserByUserId(int userId)
        {
            using (SecureAPIDb db = new SecureAPIDb())
            {
                return db.Users.FirstOrDefault(u => u.Id == userId);
            }
        }
    }
}
