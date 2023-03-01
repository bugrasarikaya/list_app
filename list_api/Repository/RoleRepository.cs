using AutoMapper;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class RoleRepository : IRoleRepository {
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public RoleRepository(IListApiDbContext context, IMapper mapper) { // Constructing.
			this.context = context;
			this.mapper = mapper;
		}
		public Role Create(RoleDTO role_dto) { // Creating a role.
			Role role_created = new Role() { Name = Check.NameForConflict<Role>(context, role_dto.Name) };
			context.Roles.Add(role_created);
			context.SaveChanges();
			return role_created;
		}
		public void Delete(int id) { // Deleting a role.
			Role role_deleted = Supply.ByID<Role>(context, id);
			Check.ForeignIDForConflict<User, Role>(context, id);
			context.Roles.Remove(role_deleted);
			context.SaveChanges();
		}
		public RoleViewModel Get(int id) { // Getting a role.
			return mapper.Map<RoleViewModel>(Supply.ByID<Role>(context, id));
		}
		public ICollection<RoleViewModel> List() { // Listing all categories.
			ICollection<RoleViewModel> list_role_view_model = new List<RoleViewModel>();
			foreach (int id in context.Roles.Select(r => r.ID).ToList()) list_role_view_model.Add(mapper.Map<RoleViewModel>(Supply.ByID<Role>(context, id)));
			return list_role_view_model;
		}
		public RoleViewModel Update(int id, RoleDTO role_dto) { // Updating a role.
			Role role_updated = Supply.ByID<Role>(context, id);
			role_updated.Name = Check.NameForConflict<Role>(context, role_dto.Name);
			context.SaveChanges();
			return mapper.Map<RoleViewModel>(role_updated);
		}
		public RoleViewModel Patch(int id, RolePatchDTO role_patch_dto) { // Patching a role.
			Role role_patched = Supply.ByID<Role>(context, id);
			if (!string.IsNullOrEmpty(role_patch_dto.Name)) role_patched.Name = Check.NameForConflict<List>(context, role_patch_dto.Name);
			context.SaveChanges();
			return mapper.Map<RoleViewModel>(role_patched);
		}
	}
}