using Microsoft.EntityFrameworkCore;
using list_api.Models;
namespace list_api.Data {
	public interface IListApiDbContext {
		DbSet<Category> Categories { get; set; }
		DbSet<List> Lists { get; set; }
		DbSet<ListProduct> ListProducts { get; set; }
		DbSet<Product> Products { get; set; }
		DbSet<Role> Roles { get; set; }
		DbSet<Status> Statuses { get; set; }
		DbSet<User> Users { get; set; }
		int SaveChanges();
	}
}