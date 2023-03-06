using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
using list_api.Security;
namespace list_api.Repository {
	public class UserRepository : IUserRepository {
		private readonly IDistributedCache cache;
		private readonly IEncryptor encryptor;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public UserRepository(IDistributedCache cache, IEncryptor encryptor, IListApiDbContext context, IMapper mapper) { // Constructing.
			this.cache = cache;
			this.encryptor = encryptor;
			this.context = context;
			this.mapper = mapper;
		}
		public UserViewModel Create(UserDTO user_dto) { // Creating a user.
			User user_created = new User() { IDRole = Check.ID<Role>(cache, context, user_dto.IDRole), Name = Check.NameForConflict<User>(cache, context, user_dto.Name), Password = encryptor.Encrpyt(user_dto.Password) };
			context.Users.Add(user_created);
			context.SaveChanges();
			return Fill.ViewModel<UserViewModel, User>(cache, context, mapper, user_created);
		}
		public User? Delete(string param_user) { // Deleting a user.
			User user_deleted;
			if (int.TryParse(param_user, out int id_user)) user_deleted = Supply.ByID<User>(cache, context, id_user);
			else user_deleted = Supply.ByName<User>(cache, context, param_user);
			Check.ForeignIDForConflict<List, User>(cache, context, user_deleted.ID);
			context.Users.Remove(user_deleted);
			context.SaveChanges();
			return user_deleted;
		}
		public UserViewModel Get(string param_user) { // Getting a user.
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			return Fill.ViewModel<UserViewModel, User>(cache, context, mapper, user);
		}
		public ICollection<UserViewModel> List() { // Listing all users.
			ICollection<UserViewModel> list_user_view_model = new List<UserViewModel>();
			foreach (int id in Supply.List<User>(cache, context).OrderBy(b => b.Name).Select(c => c.ID).ToList()) list_user_view_model.Add(Fill.ViewModel<UserViewModel, User>(cache, context, mapper, Supply.ByID<User>(cache, context, id)));
			return list_user_view_model;
		}
		public UserViewModel Update(string param_user, UserDTO user_dto) { // Updating a user.
			User user_updated;
			if (int.TryParse(param_user, out int id_user)) user_updated = Supply.ByID<User>(cache, context, id_user);
			else user_updated = Supply.ByName<User>(cache, context, param_user);
			user_updated.IDRole = Check.ID<Role>(cache, context, user_dto.IDRole);
			user_updated.Name = Check.NameForConflict<User>(cache, context, user_dto.Name);
			user_updated.Password = user_dto.Password;
			context.SaveChanges();
			return Fill.ViewModel<UserViewModel, User>(cache, context, mapper, user_updated);
		}
		public UserViewModel Patch(string param_user, UserPatchDTO user_patch_dto) { // Patching a user.
			User user_patched;
			if (int.TryParse(param_user, out int id_user)) user_patched = Supply.ByID<User>(cache, context, id_user);
			else user_patched = Supply.ByName<User>(cache, context, param_user);
			if (user_patch_dto.IDRole != default(int)) user_patch_dto.IDRole = Check.ID<Role>(cache, context, user_patch_dto.IDRole);
			if (!string.IsNullOrEmpty(user_patch_dto.Name)) user_patched.Name = Check.NameForConflict<List>(cache, context, user_patch_dto.Name);
			if (!string.IsNullOrEmpty(user_patch_dto.Password)) user_patched.Password = user_patch_dto.Password;
			context.SaveChanges();
			return Fill.ViewModel<UserViewModel, User>(cache, context, mapper, user_patched);
		}
	}
}