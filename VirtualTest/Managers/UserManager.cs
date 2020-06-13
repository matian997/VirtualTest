using System.Linq;
using VirtualTest.Configuration;
using VirtualTest.Domain;

namespace VirtualTest.Managers
{
    public class UserManager : BaseManager<User, ApplicationContext>
    {
        public UserManager(ApplicationContext context) : base(context) { }

        public User GetByUserName(string userName)
        {
            
            return context.Users.FirstOrDefault(user => user.UserName == userName);
        }

        public void NewUser(string userName, string password)
        {
            var user = GetByUserName(userName);

            if (user == null)
            {
                user = new User
                {
                    UserName = userName,
                    Password = password
                };

                Add(user);
            }
        }
    }
}
