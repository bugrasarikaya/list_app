using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class RoleRepository : IRoleRepository {
		private readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public RoleRepository(IDistributedCache cache, IListApiDbContext context, IMapper mapper) { // Constructing.
			this.cache = cache;
			this.context = context;
			this.mapper = mapper;
		}
		public Role Create(RoleDTO role_dto) { // Creating a role.
			Role role_created = new Role() { Name = Check.NameForConflict<Role>(cache, context, role_dto.Name) };
			context.Roles.Add(role_created);
			context.SaveChanges();
			RedisCache.Recache<Role>(cache, context);
			return role_created;
		}
		public void Delete(string param_role) { // Deleting a role.
			Role role_deleted;
			if (int.TryParse(param_role, out int id_role)) role_deleted = Supply.ByID<Role>(cache, context, id_role);
			else role_deleted = Supply.ByName<Role>(cache, context, param_role);
			Check.ForeignIDForConflict<User, Role>(cache, context, role_deleted.ID);
			context.Roles.Remove(role_deleted);
			context.SaveChanges();
			RedisCache.Recache<Role>(cache, context);
		}
		public RoleViewModel Get(string param_role) { // Getting a role.
			if (int.TryParse(param_role, out int id_role)) return mapper.Map<RoleViewModel>(Supply.ByID<Role>(cache, context, id_role));
			else return mapper.Map<RoleViewModel>(Supply.ByName<Role>(cache, context, param_role));
		}
		public ICollection<RoleViewModel> List() { // Listing all roles.
			ICollection<RoleViewModel> list_role_view_model = new List<RoleViewModel>();
			foreach (int id in Supply.List<Role>(cache, context).OrderBy(r => r.Name).Select(r => r.ID).ToList()) list_role_view_model.Add(mapper.Map<RoleViewModel>(Supply.ByID<Role>(cache, context, id)));
			return list_role_view_model;
		}
		public RoleViewModel Update(string param_role, RoleDTO role_dto) { // Updating a role.
			Role role_updated;
			if (int.TryParse(param_role, out int id_role)) role_updated = Supply.ByID<Role>(cache, context, id_role);
			else role_updated = Supply.ByName<Role>(cache, context, param_role);
			role_updated.Name = Check.NameForConflict<Role>(cache, context, role_dto.Name);
			context.SaveChanges();
			return mapper.Map<RoleViewModel>(role_updated);
		}
		public RoleViewModel Patch(string param_role, RolePatchDTO role_patch_dto) { // Patching a role.
			Role role_patched;
			if (int.TryParse(param_role, out int id_role)) role_patched = Supply.ByID<Role>(cache, context, id_role);
			else role_patched = Supply.ByName<Role>(cache, context, param_role);
			if (!string.IsNullOrEmpty(role_patch_dto.Name)) role_patched.Name = Check.NameForConflict<List>(cache, context, role_patch_dto.Name);
			context.SaveChanges();
			return mapper.Map<RoleViewModel>(role_patched);
		}
	}
}