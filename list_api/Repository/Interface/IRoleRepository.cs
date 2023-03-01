using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IRoleRepository {
		public Role Create(RoleDTO role_dto);
		public void Delete(int id);
		public RoleViewModel Get(int id);
		public ICollection<RoleViewModel> List();
		public RoleViewModel Update(int id, RoleDTO role_dto);
		public RoleViewModel Patch(int id, RolePatchDTO role_patch_dto);
	}
}