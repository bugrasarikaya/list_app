using Microsoft.EntityFrameworkCore;
using list_api.Models;
namespace list_api.Data {
	public interface IListApiDbContext {
		DbSet<User> Users { get; set; }
		int SaveChanges();
	}
}