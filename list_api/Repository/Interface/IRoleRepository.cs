using list_api.Models;
using list_api.Models.DTOs;
namespace list_api.Repository.Interface {
	public interface IRoleRepository {
		public Role Create(RoleDTO role_dto);
		public Role? Delete(int id);
		public RoleViewModel Get(int id);
		public ICollection<RoleViewModel> List();
		public Role Update(int id, RoleDTO role_dto);
		public Role Patch(int id, RolePatchDTO role_patch_dto);
	}
}