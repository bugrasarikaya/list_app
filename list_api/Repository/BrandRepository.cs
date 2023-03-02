using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class BrandRepository : IBrandRepository {
		private readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public BrandRepository(IDistributedCache cache, IListApiDbContext context, IMapper mapper) { // Constructing.
			this.cache = cache;
			this.context = context;
			this.mapper = mapper;
		}
		public Brand Create(BrandDTO brand_dto) { // Creating a brand.
			Brand brand_created = new Brand() { Name = Check.NameForConflict<Brand>(cache, context, brand_dto.Name) };
			context.Brands.Add(brand_created);
			context.SaveChanges();
			RedisCache.Recache<Brand>(cache, context);
			return brand_created;
		}
		public void Delete(string param_brand) { // Deleting a brand.
			Brand brand_deleted;
			if (int.TryParse(param_brand, out int id_brand)) brand_deleted = Supply.ByID<Brand>(cache, context, id_brand);
			else brand_deleted = Supply.ByName<Brand>(cache, context, param_brand);
			Check.ForeignIDForConflict<Product, Brand>(cache, context, brand_deleted.ID);
			context.Brands.Remove(brand_deleted);
			context.SaveChanges();
			RedisCache.Recache<Brand>(cache, context);
		}
		public BrandViewModel Get(string param_brand) { // Getting a brand.
			if (int.TryParse(param_brand, out int id_brand)) return mapper.Map<BrandViewModel>(Supply.ByID<Brand>(cache, context, id_brand));
			else return mapper.Map<BrandViewModel>(Supply.ByName<Brand>(cache, context, param_brand));
		}
		public ICollection<BrandViewModel> List() { // Listing all brands.
			ICollection<BrandViewModel> list_brand_view_model = new List<BrandViewModel>();
			foreach (int id in Supply.List<Brand>(cache, context).OrderBy(b => b.Name).Select(c => c.ID).ToList()) list_brand_view_model.Add(mapper.Map<BrandViewModel>(Supply.ByID<Brand>(cache, context, id)));
			return list_brand_view_model;
		}
		public BrandViewModel Update(string param_brand, BrandDTO brand_dto) { // Updating a brand.
			Brand brand_updated;
			if (int.TryParse(param_brand, out int id_brand)) brand_updated = Supply.ByID<Brand>(cache, context, id_brand);
			else brand_updated = Supply.ByName<Brand>(cache, context, param_brand);
			brand_updated.Name = Check.NameForConflict<Brand>(cache, context, brand_dto.Name);
			context.SaveChanges();
			return mapper.Map<BrandViewModel>(brand_updated);
		}
		public BrandViewModel Patch(string param_brand, BrandPatchDTO brand_patch_dto) { // Patching a brand.
			Brand brand_patched;
			if (int.TryParse(param_brand, out int id_brand)) brand_patched = Supply.ByID<Brand>(cache, context, id_brand);
			else brand_patched = Supply.ByName<Brand>(cache, context, param_brand);
			if (!string.IsNullOrEmpty(brand_patch_dto.Name)) brand_patched.Name = Check.NameForConflict<List>(cache, context, brand_patch_dto.Name);
			context.SaveChanges();
			return mapper.Map<BrandViewModel>(brand_patched);
		}
	}
}