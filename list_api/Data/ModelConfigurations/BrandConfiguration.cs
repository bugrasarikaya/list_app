using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using list_api.Models;
namespace list_api.Data.ModelConfigurations {
	public class BrandConfiguration : IEntityTypeConfiguration<Brand> {
		public void Configure(EntityTypeBuilder<Brand> builder) {
			builder.ToTable("Brands");
			builder.HasKey(b => b.ID);
			builder.Property(b => b.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
		}
	}
}