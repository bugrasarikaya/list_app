using list_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace list_api.Data.ModelConfigurations {
	public class ListConfiguration : IEntityTypeConfiguration<List> {
		public void Configure(EntityTypeBuilder<List> builder) {
			builder.ToTable("Lists");
			builder.HasKey(l => l.ID);
			builder.Property(l => l.IDCategory).IsRequired(true).HasColumnType("int");
			builder.Property(l => l.IDStatus).IsRequired(true).HasColumnType("int");
			builder.Property(l => l.IDUser).IsRequired(true).HasColumnType("int");
			builder.Property(l => l.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
			builder.Property(l => l.Description).HasMaxLength(200).HasColumnType("varchar");
			builder.Property(l => l.DateTimeCompleting).HasColumnType("datetime");
			builder.Property(l => l.DateTimeCreating).IsRequired(true).HasColumnType("datetime");
			builder.Property(l => l.DateTimeUpdating).IsRequired(true).HasColumnType("datetime");
			builder.Property(l => l.TotalCost).IsRequired(true).HasColumnType("float");
		}
	}
}