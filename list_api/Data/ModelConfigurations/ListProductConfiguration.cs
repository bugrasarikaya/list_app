using list_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace list_api.Data.ModelConfigurations {
	public class ListProductConfiguration : IEntityTypeConfiguration<ListProduct> {
		public void Configure(EntityTypeBuilder<ListProduct> builder) {
			builder.ToTable("ListProducts");
			builder.HasKey(l => l.ID);
			builder.Property(lp => lp.IDList).IsRequired(true).HasColumnType("int");
			builder.Property(lp => lp.IDProduct).IsRequired(true).HasColumnType("int");
			builder.Property(lp => lp.Quantity).IsRequired(true).HasColumnType("int");
		}
	}
}