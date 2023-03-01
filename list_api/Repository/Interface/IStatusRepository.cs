using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IStatusRepository {
		public Status Create(StatusDTO status_dto);
		public void Delete(int id);
		public StatusViewModel Get(int id);
		public ICollection<StatusViewModel> List();
		public StatusViewModel Update(int id, StatusDTO status_dto);
		public StatusViewModel Patch(int id, StatusPatchDTO status_patch_dto);
	}
}