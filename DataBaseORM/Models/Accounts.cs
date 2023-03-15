

namespace ServerSide.DataBaseORM
{
    public class Accounts
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ulong SocialClubId { get; set; }

        public Accounts()
        {
            
        }

        public Accounts(string login, string email, string password, ulong socialClubId)
        {
            this.Login = login;
            this.Email = email;
            this.Password = password;
            this.SocialClubId = socialClubId;
        }
    }
}