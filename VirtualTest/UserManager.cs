using System.Linq;
using VirtualTest.Domain;

namespace VirtualTest
{
    public class UserManager : Manager<User, EFConfing>
    {
        public UserManager(EFConfing dbContext) : base(dbContext) { }
        
        public override bool Add(User user)
        {
            var result = this.FindByUserName(user.UserName);
            if (!result)
            {
                this.dbContext.Set<User>().Add(user);
                this.dbContext.SaveChanges();
                
                return true;
            }

            return false;
        }

        public bool FindByUserName(string userName)
        {
            return this.dbContext.Set<User>().Any(user => user.UserName == userName);
        }
    }
}
