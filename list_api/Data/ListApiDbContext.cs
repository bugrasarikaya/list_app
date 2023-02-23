using Microsoft.EntityFrameworkCore;
using list_api.Models;
namespace list_api.Data {
	public class ListApiDbContext : DbContext, IListApiDbContext {
		public DbSet<User> Users { get; set; }
		public override int SaveChanges() {
			return base.SaveChanges();
		}
	}
}