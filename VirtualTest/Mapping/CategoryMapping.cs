using System.Data.Entity.ModelConfiguration;
using VirtualTest.Domain;

namespace VirtualTest.Mapping
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            this.ToTable("Categories");

            this.HasKey(category => category.Id);

            this.Property(category => category.No)
               .HasColumnName("CATEGORY_NO")
               .IsRequired();

            this.Property(category => category.Id)
                .HasColumnName("CATEGORY_ID");

            this.Property(category => category.Name)
                .HasMaxLength(70)
                .HasColumnName("CATEGORY_NAME")
                .IsRequired();
        }
    }
}
