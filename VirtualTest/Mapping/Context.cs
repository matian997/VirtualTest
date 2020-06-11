using System;
using System.Data.Entity;
using VirtualTest.Domain;

namespace VirtualTest
{
    public class Context : DbContext
    {
        private static readonly Lazy<Context> instance = new Lazy<Context>(() => new Context());

        public IDbSet<User> Users { get; set; }
        public IDbSet<Test> Tests { get; set; }
        public IDbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuielder)
        {
            modelBuielder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuielder);
        }

        public static Context Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
