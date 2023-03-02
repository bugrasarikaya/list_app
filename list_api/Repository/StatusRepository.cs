using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class StatusRepository : IStatusRepository {
		private readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public StatusRepository(IDistributedCache cache, IListApiDbContext context, IMapper mapper) { // Constructing.
			this.cache = cache;
			this.context = context;
			this.mapper = mapper;
		}
		public Status Create(StatusDTO status_dto) { // Creating a status.
			Status status_created = new Status() { Name = Check.NameForConflict<Status>(cache, context, status_dto.Name) };
			context.Statuses.Add(status_created);
			context.SaveChanges();
			RedisCache.Recache<Status>(cache, context);
			return status_created;
		}
		public void Delete(string param_status) { // Deleting a status.
			Status status_deleted;
			if (int.TryParse(param_status, out int id_status)) status_deleted = Supply.ByID<Status>(cache, context, id_status);
			else status_deleted = Supply.ByName<Status>(cache, context, param_status);
			Check.ForeignIDForConflict<List, Status>(cache, context, status_deleted.ID);
			context.Statuses.Remove(status_deleted);
			context.SaveChanges();
			RedisCache.Recache<Status>(cache, context);
		}
		public StatusViewModel Get(string param_status) { // Getting a status.
			if (int.TryParse(param_status, out int id_status)) return mapper.Map<StatusViewModel>(Supply.ByID<Status>(cache, context, id_status));
			else return mapper.Map<StatusViewModel>(Supply.ByName<Status>(cache, context, param_status));
		}
		public ICollection<StatusViewModel> List() { // Listing all statuses.
			ICollection<StatusViewModel> list_status_view_model = new List<StatusViewModel>();
			foreach (int id in Supply.List<Status>(cache, context).OrderBy(s => s.Name).Select(s => s.ID).ToList()) list_status_view_model.Add(mapper.Map<StatusViewModel>(Supply.ByID<Status>(cache, context, id)));
			return list_status_view_model;
		}
		public StatusViewModel Update(string param_status, StatusDTO status_dto) { // Updating a status.
			Status status_updated;
			if (int.TryParse(param_status, out int id_status)) status_updated = Supply.ByID<Status>(cache, context, id_status);
			else status_updated = Supply.ByName<Status>(cache, context, param_status);
			status_updated.Name = Check.NameForConflict<Status>(cache, context, status_dto.Name);
			context.SaveChanges();
			return mapper.Map<StatusViewModel>(status_updated);
		}
		public StatusViewModel Patch(string param_status, StatusPatchDTO status_patch_dto) { // Patching a status.
			Status status_patched;
			if (int.TryParse(param_status, out int id_status)) status_patched = Supply.ByID<Status>(cache, context, id_status);
			else status_patched = Supply.ByName<Status>(cache, context, param_status);
			if (!string.IsNullOrEmpty(status_patch_dto.Name)) status_patched.Name = Check.NameForConflict<List>(cache, context, status_patch_dto.Name);
			context.SaveChanges();
			return mapper.Map<StatusViewModel>(status_patched);
		}
	}
}