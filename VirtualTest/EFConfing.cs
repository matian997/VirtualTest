using System.Data.Entity;
using VirtualTest.Domain;

namespace VirtualTest
{
    public class EFConfing : DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Test> Tests { get; set; }
        public IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuielder)
        {
            modelBuielder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuielder);
        }
    }
}
