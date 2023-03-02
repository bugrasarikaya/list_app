using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
namespace list_api.Repository.Interface {
	public interface IStatusRepository {
		public Status Create(StatusDTO status_dto);
		public void Delete(string param_status);
		public StatusViewModel Get(string param_status);
		public ICollection<StatusViewModel> List();
		public StatusViewModel Update(string param_status, StatusDTO status_dto);
		public StatusViewModel Patch(string param_status, StatusPatchDTO status_patch_dto);
	}
}