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

        public bool NewUser(string userName, string password)
        {
            var user = GetByUserName(userName);
            bool result;
            if (user == null)
            {
                user = new User
                {
                    UserName = userName,
                    Password = password
                };

                Add(user);
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}
