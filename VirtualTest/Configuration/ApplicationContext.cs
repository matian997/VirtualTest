using System;
using System.Data.Entity;
using VirtualTest.Domain;

namespace VirtualTest.Configuration
{
    public class ApplicationContext : DbContext
    {
        private static readonly Lazy<ApplicationContext> instance = new Lazy<ApplicationContext>(() => new ApplicationContext());

        public IDbSet<User> Users { get; set; }
        public IDbSet<Test> Tests { get; set; }
        public IDbSet<Category> Categories { get; set; }

        private ApplicationContext() : base("AplicationContextTest") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

           base.OnModelCreating(modelBuilder);
        }

        public static ApplicationContext Instance
        {
            get
            {
                return instance.Value;
            }
        }
    }
}
