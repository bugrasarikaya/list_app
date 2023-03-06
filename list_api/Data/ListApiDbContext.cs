using Microsoft.EntityFrameworkCore;
using list_api.Models;
using list_api.Data.ModelConfigurations;
namespace list_api.Data {
	public class ListApiDbContext : DbContext, IListApiDbContext {
		public DbSet<Brand> Brands { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<List> Lists { get; set; }
		public DbSet<ListProduct> ListProducts { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<User> Users { get; set; }
		public ListApiDbContext(DbContextOptions<ListApiDbContext> options) : base(options) { } // Constructing.
		protected override void OnModelCreating(ModelBuilder model_builder) { // Configuring tables.
			model_builder.ApplyConfiguration(new BrandConfiguration());
			model_builder.ApplyConfiguration(new CategoryConfiguration());
			model_builder.ApplyConfiguration(new ListConfiguration());
			model_builder.ApplyConfiguration(new ListProductConfiguration());
			model_builder.ApplyConfiguration(new ProductConfiguration());
			model_builder.ApplyConfiguration(new RoleConfiguration());
			model_builder.ApplyConfiguration(new StatusConfiguration());
			model_builder.ApplyConfiguration(new UserConfiguration());
			model_builder.Entity<List>().HasOne<Category>(lp => lp.Category).WithMany(c => c.Lists).HasForeignKey(lp => lp.IDCategory).OnDelete(DeleteBehavior.ClientSetNull);
			model_builder.Entity<List>().HasOne<Status>(lp => lp.Status).WithMany(s => s.Lists).HasForeignKey(lp => lp.IDStatus).OnDelete(DeleteBehavior.ClientSetNull);
			model_builder.Entity<List>().HasOne<User>(lp => lp.User).WithMany(u => u.Lists).HasForeignKey(lp => lp.IDUser).OnDelete(DeleteBehavior.ClientSetNull);
			model_builder.Entity<ListProduct>().HasOne<List>(lp => lp.List).WithMany(l => l.ListProducts).HasForeignKey(lp => lp.IDList).OnDelete(DeleteBehavior.ClientSetNull);
			model_builder.Entity<ListProduct>().HasOne<Product>(lp => lp.Product).WithMany(l => l.ListProducts).HasForeignKey(lp => lp.IDProduct).OnDelete(DeleteBehavior.ClientSetNull);
			model_builder.Entity<Product>().HasOne<Brand>(p => p.Brand).WithMany(b => b.Products).HasForeignKey(b => b.IDBrand).OnDelete(DeleteBehavior.ClientSetNull);
			model_builder.Entity<Product>().HasOne<Category>(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.IDCategory).OnDelete(DeleteBehavior.ClientSetNull);
			model_builder.Entity<User>().HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.IDRole).OnDelete(DeleteBehavior.ClientSetNull);
		}
		public override int SaveChanges() {
			return base.SaveChanges();
		}
	}
}