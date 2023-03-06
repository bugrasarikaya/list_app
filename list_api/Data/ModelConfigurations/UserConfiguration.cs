using list_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace list_api.Data.ModelConfigurations {
	public class UserConfiguration : IEntityTypeConfiguration<User> {
		public void Configure(EntityTypeBuilder<User> builder) {
			builder.ToTable("Users");
			builder.HasKey(u => u.ID);
			builder.Property(u => u.IDRole).IsRequired(true).HasColumnType("int");
			builder.Property(u => u.Name).IsRequired(true).HasMaxLength(100).HasColumnType("varchar");
			builder.Property(u => u.Password).IsRequired(true).HasMaxLength(64).HasColumnType("varchar");
			builder.Property(u => u.RefreshToken).HasMaxLength(36).HasColumnType("varchar");
			builder.Property(u => u.RefreshTokenExpireDate).IsRequired(true).HasColumnType("datetime");
		}
	}
}