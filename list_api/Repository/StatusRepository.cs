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
		public StatusViewModel Create(StatusDTO status_dto) { // Creating a status.
			Status status_created = new Status() { Name = Check.NameForConflict<Status>(cache, context, status_dto.Name) };
			context.Statuses.Add(status_created);
			context.SaveChanges();
			RedisCache.Recache<Status>(cache, context);
			return Fill.ViewModel<StatusViewModel, Status>(cache, context, mapper, status_created);
		}
		public Status? Delete(string param_status) { // Deleting a status.
			Status status_deleted;
			if (int.TryParse(param_status, out int id_status)) status_deleted = Supply.ByID<Status>(cache, context, id_status);
			else status_deleted = Supply.ByName<Status>(cache, context, param_status);
			Check.ForeignIDForConflict<List, Status>(cache, context, status_deleted.ID);
			context.Statuses.Remove(status_deleted);
			context.SaveChanges();
			RedisCache.Recache<Status>(cache, context);
			return status_deleted;
		}
		public StatusViewModel Get(string param_status) { // Getting a status.
			Status status;
			if (int.TryParse(param_status, out int id_status)) status = Supply.ByID<Status>(cache, context, id_status);
			else status = Supply.ByName<Status>(cache, context, param_status);
			return Fill.ViewModel<StatusViewModel, Status>(cache, context, mapper, status);
		}
		public ICollection<StatusViewModel> List() { // Listing all statuses.
			ICollection<StatusViewModel> list_status_view_model = new List<StatusViewModel>();
			foreach (int id in Supply.List<Status>(cache, context).OrderBy(b => b.Name).Select(c => c.ID).ToList()) list_status_view_model.Add(Fill.ViewModel<StatusViewModel, Status>(cache, context, mapper, Supply.ByID<Status>(cache, context, id)));
			return list_status_view_model;
		}
		public StatusViewModel Update(string param_status, StatusDTO status_dto) { // Updating a status.
			Status status_updated;
			if (int.TryParse(param_status, out int id_status)) status_updated = Supply.ByID<Status>(cache, context, id_status);
			else status_updated = Supply.ByName<Status>(cache, context, param_status);
			status_updated.Name = Check.NameForConflict<Status>(cache, context, status_dto.Name);
			context.SaveChanges();
			return Fill.ViewModel<StatusViewModel, Status>(cache, context, mapper, status_updated);
		}
		public StatusViewModel Patch(string param_status, StatusPatchDTO status_patch_dto) { // Patching a status.
			Status status_patched;
			if (int.TryParse(param_status, out int id_status)) status_patched = Supply.ByID<Status>(cache, context, id_status);
			else status_patched = Supply.ByName<Status>(cache, context, param_status);
			if (!string.IsNullOrEmpty(status_patch_dto.Name)) status_patched.Name = Check.NameForConflict<List>(cache, context, status_patch_dto.Name);
			context.SaveChanges();
			return Fill.ViewModel<StatusViewModel, Status>(cache, context, mapper, status_patched);
		}
	}
}