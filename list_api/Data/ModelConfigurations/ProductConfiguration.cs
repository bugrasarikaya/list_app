using list_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace list_api.Data.ModelConfigurations {
	public class ProductConfiguration : IEntityTypeConfiguration<Product> {
		public void Configure(EntityTypeBuilder<Product> builder) {
			builder.ToTable("Products");
			builder.HasKey(p => p.ID);
			builder.Property(p => p.IDBrand).IsRequired(true).HasColumnType("int");
			builder.Property(p => p.IDCategory).IsRequired(true).HasColumnType("int");
			builder.Property(p => p.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
			builder.Property(p => p.Description).HasMaxLength(200).HasColumnType("varchar");
			builder.Property(p => p.Price).IsRequired(true).HasColumnType("float");
		}
	}
}