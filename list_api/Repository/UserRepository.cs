using AutoMapper;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class UserRepository : IUserRepository {
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public UserRepository(IListApiDbContext context, IMapper mapper) { // Constructing.
			this.context = context;
			this.mapper = mapper;
		}
		public User Create(UserDTO user_dto) { // Creating a user.
			User user_created = new User() { IDRole = Check.ID<Role>(context, user_dto.IDRole), Name = Check.NameForConflict<User>(context, user_dto.Name), Password = user_dto.Password };
			context.Users.Add(user_created);
			context.SaveChanges();
			return user_created;
		}
		public User? Delete(int id) { // Deleting a user.
			User user_deleted = Supply.ByID<User>(context, id);
			Check.ForeignIDForConflict<List, User>(context, id);
			context.Users.Remove(user_deleted);
			context.SaveChanges();
			return user_deleted;
		}
		public UserViewModel Get(int id) { // Getting a user.
			return mapper.Map<UserViewModel>(Supply.ByID<User>(context, id));
		}
		public ICollection<UserViewModel> List() { // Listing all categories.
			ICollection<UserViewModel> list_user_view_model = new List<UserViewModel>();
			foreach (int id in context.Users.Select(u => u.ID).ToList()) list_user_view_model.Add(mapper.Map<UserViewModel>(Supply.ByID<User>(context, id)));
			return list_user_view_model;
		}
		public User Update(int id, UserDTO user_dto) { // Updating a user.
			User user_updated = Supply.ByID<User>(context, id);
			user_updated.Name = Check.NameForConflict<User>(context, user_dto.Name);
			context.SaveChanges();
			return user_updated;
		}
		public User Patch(int id, UserPatchDTO user_patch_dto) { // Patching a user.
			User user_patched = Supply.ByID<User>(context, id);
			if (user_patch_dto.IDRole != default(int)) user_patch_dto.IDRole = Check.ID<Role>(context, user_patch_dto.IDRole);
			if (!string.IsNullOrEmpty(user_patch_dto.Name)) user_patched.Name = Check.NameForConflict<List>(context, user_patch_dto.Name);
			if (!string.IsNullOrEmpty(user_patch_dto.Password)) user_patched.Password = user_patch_dto.Password;
			context.SaveChanges();
			return user_patched;
		}
	}
}