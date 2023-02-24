using list_api.Models.ViewModels;
using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
namespace list_api.Repository {
	public class UserRepository : IUserRepository {
		private readonly IListApiDbContext context;
		private User? user { get; set; }
		public UserRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public User Create(UserViewModel user_view_model) { // Creating a user.
			user = new User() { Name = user_view_model.Name, Password = user_view_model.Password };
			if (context.Users.Any(u => u.Name == user.Name)) throw new InvalidOperationException("User already exists.");
			context.Users.Add(user);
			context.SaveChanges();
			return user;
		}
		public User? Delete(int id) { // Deleting a user.
			user = context.Users.FirstOrDefault(u => u.ID == id);
			if (user != null) {
				if (context.Lists.Any(l => l.UserID == id)) throw new InvalidOperationException("User exists in a list.");
				context.Users.Remove(user);
				context.SaveChanges();
			}
			return user;
		}
		public User? Get(int id) { // Getting a user.
			return context.Users.FirstOrDefault(u => u.ID == id);
		}
		public ICollection<User> List() { // Listing all users.
			return context.Users.ToList();
		}
		public User? Update(int id, UserViewModel user_view_model) { // Updating a user.
			user = context.Users.FirstOrDefault(u => u.ID == id);
			if (user != null) {
				user.Name = user_view_model.Name;
				user.Password = user_view_model.Password;
				user.Role = user.Role;
				context.SaveChanges();
			}
			return user;
		}
	}
}