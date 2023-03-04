using list_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace list_api.Data.ModelConfigurations {
	public class RoleConfiguration : IEntityTypeConfiguration<Role> {
		public void Configure(EntityTypeBuilder<Role> builder) {
			builder.ToTable("Roles");
			builder.HasKey(r => r.ID);
			builder.Property(r => r.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
		}
	}
}