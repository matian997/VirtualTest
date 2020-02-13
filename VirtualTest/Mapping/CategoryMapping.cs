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

            this.Property(category => category.Id)
                .HasColumnName("CATEGORY_ID");

            this.Property(category => category.Name)
                .HasMaxLength(50)
                .HasColumnName("CATEGORY_NAME")
                .IsRequired();
        }
    }
}
