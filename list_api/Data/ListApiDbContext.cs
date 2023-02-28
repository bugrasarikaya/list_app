using Microsoft.EntityFrameworkCore;
using list_api.Models;
namespace list_api.Data {
	public class ListApiDbContext : DbContext, IListApiDbContext {
		public DbSet<Category> Categories { get; set; }
		public DbSet<List> Lists { get; set; }
		public DbSet<ListProduct> ListProducts { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Status> Statuses { get; set; }
		public DbSet<User> Users { get; set; }
		public ListApiDbContext(DbContextOptions<ListApiDbContext> options) : base(options) { } // Constructing.
		public override int SaveChanges() {
			return base.SaveChanges();
		}
	}
}