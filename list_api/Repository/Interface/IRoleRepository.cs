using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IRoleRepository {
		public RoleViewModel Create(RoleDTO role_dto);
		public Role? Delete(string param_role);
		public RoleViewModel Get(string param_role);
		public ICollection<RoleViewModel> List();
		public RoleViewModel Update(string param_role, RoleDTO role_dto);
		public RoleViewModel Patch(string param_role, RolePatchDTO role_patch_dto);
	}
}