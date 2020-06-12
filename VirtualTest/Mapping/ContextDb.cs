using System;
using System.Data.Entity;
using VirtualTest.Domain;

namespace VirtualTest.Mapping
{
    public class ContextDb : DbContext
    {
        private static readonly Lazy<ContextDb> instance = new Lazy<ContextDb>(() => new ContextDb());

        public IDbSet<User> Users { get; set; }
        public IDbSet<Test> Tests { get; set; }
        public IDbSet<Category> Categories { get; set; }

        private ContextDb() : base("Context") { }

        protected override void OnModelCreating(DbModelBuilder modelBuielder)
        {
           modelBuielder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuielder);
        }

        public static ContextDb Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
