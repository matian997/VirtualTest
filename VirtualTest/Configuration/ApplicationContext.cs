using System;
using System.Data.Entity;
using VirtualTest.Domain;

namespace VirtualTest.Configuration
{
    public class ApplicationContext : DbContext
    {
        public IDbSet<User> Users { get; set; }
        public IDbSet<Test> Tests { get; set; }
        public IDbSet<Category> Categories { get; set; }

        public ApplicationContext() : base("ApplicationContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationContext, VirtualTest.Migrations.Configuration>());
        }
        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

           base.OnModelCreating(modelBuilder);
        }
    }
}
