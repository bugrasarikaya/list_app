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
		public void Delete(int id) { // Deleting a status.
			Status status_deleted = Supply.ByID<Status>(cache, context, id);
			Check.ForeignIDForConflict<List, Status>(cache, context, id);
			context.Statuses.Remove(status_deleted);
			context.SaveChanges();
			RedisCache.Recache<Status>(cache, context);
		}
		public StatusViewModel Get(int id) { // Getting a status.
			return mapper.Map<StatusViewModel>(Supply.ByID<Status>(cache, context, id));
		}
		public ICollection<StatusViewModel> List() { // Listing all categories.
			ICollection<StatusViewModel> list_status_view_model = new List<StatusViewModel>();
			foreach (int id in context.Statuses.Select(s => s.ID).ToList()) list_status_view_model.Add(mapper.Map<StatusViewModel>(Supply.ByID<Status>(cache, context, id)));
			return list_status_view_model;
		}
		public StatusViewModel Update(int id, StatusDTO status_dto) { // Updating a status.
			Status status_updated = Supply.ByID<Status>(cache, context, id);
			status_updated.Name = Check.NameForConflict<Status>(cache, context, status_dto.Name);
			context.SaveChanges();
			return mapper.Map<StatusViewModel>(status_updated);
		}
		public StatusViewModel Patch(int id, StatusPatchDTO status_patch_dto) { // Patching a status.
			Status status_patched = Supply.ByID<Status>(cache, context, id);
			if (!string.IsNullOrEmpty(status_patch_dto.Name)) status_patched.Name = Check.NameForConflict<List>(cache, context, status_patch_dto.Name);
			context.SaveChanges();
			return mapper.Map<StatusViewModel>(status_patched);
		}
	}
}