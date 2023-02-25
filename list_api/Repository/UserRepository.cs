using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
namespace list_api.Repository {
	public class UserRepository : IUserRepository {
		private readonly IListApiDbContext context;
		public UserRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public User Create(User user) { // Creating a user.
			User user_created = new User() { Name = user.Name, Password = user.Password, Role = user.Role };
			if (context.Users.Any(u => u.Name == user_created.Name)) throw new InvalidOperationException("User already exists.");
			context.Users.Add(user_created);
			context.SaveChanges();
			return user_created;
		}
		public User? Delete(int id) { // Deleting a user.
			User? user_deleted = context.Users.FirstOrDefault(u => u.ID == id);
			if (user_deleted != null) {
				if (context.Lists.Any(l => l.IDUser == id)) throw new InvalidOperationException("User exists in a list.");
				context.Users.Remove(user_deleted);
				context.SaveChanges();
			}
			return user_deleted;
		}
		public User? Get(int id) { // Getting a user.
			return context.Users.FirstOrDefault(u => u.ID == id);
		}
		public ICollection<User> List() { // Listing all users.
			return context.Users.ToList();
		}
		public User? Update(int id, User user) { // Updating a user.
			User? user_updated = context.Users.FirstOrDefault(u => u.ID == id);
			if (user_updated != null) {
				if (context.Users.Any(u => u.Name == user.Name)) throw new InvalidOperationException("User already exists.");
				user_updated.Name = user.Name;
				user_updated.Password = user.Password;
				user_updated.Role = user.Role;
				context.SaveChanges();
			}
			return user_updated;
		}
	}
}