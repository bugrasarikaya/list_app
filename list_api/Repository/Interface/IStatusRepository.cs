using list_api.Models;
using list_api.Models.DTOs;
namespace list_api.Repository.Interface {
	public interface IStatusRepository {
		public Status Create(StatusDTO status_dto);
		public Status? Delete(int id);
		public StatusViewModel Get(int id);
		public ICollection<StatusViewModel> List();
		public Status Update(int id, StatusDTO status_dto);
		public Status Patch(int id, StatusPatchDTO status_patch_dto);
	}
}