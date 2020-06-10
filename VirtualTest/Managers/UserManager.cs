using System.Linq;
using VirtualTest.Domain;

namespace VirtualTest
{
    public class UserManager : BaseManager<User, Context>
    {
        public UserManager(Context dbContext) : base(dbContext) { }
        
        public override void Add(User user)
        {
            this.context.Set<User>().Add(user);
            this.context.SaveChanges();
        }

        public bool FindByUserName(string userName)
        {
            return this.context.Set<User>().Any(user => user.UserName == userName);
        }
    }
}
