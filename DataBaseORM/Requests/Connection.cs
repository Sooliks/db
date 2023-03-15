using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServerSide.DataBaseORM
{
    public class Connection
    {
        public static bool IsLoginExist(string login)
        {
            bool result;
            using (Context db = new Context())
            {
                result = db.Accounts.Any(x => x.Login == login);
                db.SaveChanges();
            }
            return result;
        }
        public static bool IsEmailExist(string email)
        {
            bool result;
            using (Context db = new Context())
            {
                result = db.Accounts.Any(x => x.Email == email);
                db.SaveChanges();
            }
            return result;
        }
        public static bool IsSocialClubIdExist(ulong socialClubId)
        {
            bool result;
            using (Context db = new Context())
            {
                result = db.Accounts.Any(x => x.SocialClubId == socialClubId);
                db.SaveChanges();
            }
            return result;
        }
        public static bool IsPasswordValid(string login, string password)
        {
            Accounts activeAccount = new Accounts();
            using (Context db = new Context())
            {
                activeAccount = db.Accounts.FirstOrDefault(p=> p.Login==login);
            }
            if (Bcrypt.BCrypt.CheckPassword(password, activeAccount.Password)) return true;
            return false;
        }

        public static void LoadAccount(Account account)
        {
            Accounts activeAccount = new Accounts();
            using (Context db = new Context())
            {
                activeAccount = db.Accounts.FirstOrDefault(p=> p.Login==account.LoginData);
                account.Id = activeAccount.Id;
            }
        }
        public static void Register(string login, string email, string password, ulong socialClubId)
        {
            using (Context db = new Context())
            {
                string saltePassword = Bcrypt.BCrypt.HashPassword(password, Bcrypt.BCrypt.GenerateSalt());
                var user = new Accounts(login,email,saltePassword,socialClubId);
                db.Accounts.Add(user);
                db.SaveChangesAsync();
            }
        }
        
    }
}