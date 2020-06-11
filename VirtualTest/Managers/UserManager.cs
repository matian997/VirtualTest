using System.Linq;
using VirtualTest.Domain;

namespace VirtualTest
{
    public class UserManager : BaseManager<User, Context>
    {
        public UserManager(Context context) : base(context) { }

        public User GetByUserName(string userName)
        {
            return context.Users.FirstOrDefault(user => user.UserName == userName);
        }

        public void NewUser(string userName, string password)
        {
            var user = GetByUserName(userName);

            if (user != null)
            {
                user = new User
                {
                    UserName = userName,
                    Password = password
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
