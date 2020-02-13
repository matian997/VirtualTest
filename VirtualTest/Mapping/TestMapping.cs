using System.Data.Entity.ModelConfiguration;
using VirtualTest.Domain;

namespace VirtualTest.Mapping
{
    public class TestMapping : EntityTypeConfiguration<Test>
    {
        public TestMapping()
        {
            this.ToTable("Tests");

            this.HasKey(test => test.Id);

            this.Property(test => test.Id)
                .HasColumnName("Test_ID")
                .IsRequired()
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            this.Property(test => test.Score)
                .HasColumnName("Test_Score")
                .IsRequired();

            this.Property(test => test.Duration)
                .HasColumnName("Test_Duration")
                .IsRequired();

            this.Property(test => test.StartDate)
                .HasColumnName("Test_StartDate")
                .IsRequired();

            this.Property(test => test.EndDate)
                .HasColumnName("Tests_EndDate")
                .IsRequired();
        }
    }
}
