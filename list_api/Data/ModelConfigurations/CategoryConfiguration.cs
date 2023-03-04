using list_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace list_api.Data.ModelConfigurations {
	public class CategoryConfiguration : IEntityTypeConfiguration<Category> {
		public void Configure(EntityTypeBuilder<Category> builder) {
			builder.ToTable("Categories");
			builder.HasKey(c => c.ID);
			builder.Property(c => c.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
		}
	}
}