using System.Data.Entity.ModelConfiguration;
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

            this.Property(user => user.Name)
                .HasColumnName("User_Name")
                .HasMaxLength(20)
                .IsRequired();

            this.Property(user => user.LastName)
                .HasColumnName("User_LastName")
                .HasMaxLength(20)
                .IsRequired();

            this.HasMany(user => user.Tests);
        }
    }
}
