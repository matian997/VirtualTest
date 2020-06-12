using System.Linq;
using VirtualTest.Domain;
using VirtualTest.Mapping;

namespace VirtualTest.Managers
{
    public class UserManager : BaseManager<User, ContextDb>
    {
        public UserManager(ContextDb context) : base(context) { }

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
