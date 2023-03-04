using list_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace list_api.Data.ModelConfigurations {
	public class StatusConfiguration : IEntityTypeConfiguration<Status> {
		public void Configure(EntityTypeBuilder<Status> builder) {
			builder.ToTable("Statuses");
			builder.HasKey(s => s.ID);
			builder.Property(s => s.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
		}
	}
}