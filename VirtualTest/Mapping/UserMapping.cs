using System.Data.Entity.ModelConfiguration;
using System.Security.Cryptography.X509Certificates;
using VirtualTest.Domain;

namespace VirtualTest.Mapping 
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            this.ToTable("Users");

            this.HasKey(user => user.Id);

            this.Property(user => user.Id)
                .HasColumnName("User_Id")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.Property(user => user.UserName)
                .HasColumnName("User_UserName")
                .HasMaxLength(10)
                .IsRequired();

            this.Property(user => user.Password)
                .HasColumnName("User_Password")
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
