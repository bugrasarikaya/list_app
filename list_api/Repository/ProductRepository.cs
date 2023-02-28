using AutoMapper;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class ProductRepository : IProductRepository {
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public ProductRepository(IListApiDbContext context, IMapper mapper) { // Constructing.
			this.context = context;
			this.mapper = mapper;
		}
		public Product Create(ProductDTO product_dto) { // Creating a product.
			Product product_created = new Product() { IDCategory = Check.ID<Category>(context, product_dto.IDCategory), Name = Check.NameForConflict<Product>(context, product_dto.Name), Description = product_dto.Description, Price = product_dto.Price };
			context.Products.Add(product_created);
			context.SaveChanges();
			return product_created;
		}
		public Product? Delete(int id) { // Deleting a product.
			Product product_deleted = Supply.ByID<Product>(context, id);
			Check.ForeignIDForConflict<ListProduct, Product>(context, id);
			context.Products.Remove(product_deleted);
			context.SaveChanges();
			return product_deleted;
		}
		public Product Get(int id) { // Getting a product.
			return Supply.ByID<Product>(context, id);
		}
		public ICollection<ProductViewModel> List() { // Listing all products.
			ICollection<ProductViewModel> list_product_view_model = new List<ProductViewModel>();
			foreach (int id in context.Products.Select(p => p.ID).ToList()) list_product_view_model.Add(mapper.Map<ProductViewModel>(Supply.ByID<Product>(context, id)));
			return list_product_view_model;
		}
		public ICollection<ProductViewModel> List(int id_category) { // Listing all products which have a specific category.
			ICollection<ProductViewModel> list_product_view_model = new List<ProductViewModel>();
			foreach (int id in context.Products.Where(p => p.IDCategory == id_category).Select(p => p.ID).ToList()) list_product_view_model.Add(mapper.Map<ProductViewModel>(Supply.ByID<Product>(context, id)));
			return list_product_view_model;
		}
		public Product Update(int id, ProductDTO product_dto) { // Updating a product.
			Product product_updated = Supply.ByID<Product>(context, id);
			product_updated.IDCategory = Check.ID<Category>(context, product_dto.IDCategory);
			product_updated.Name = Check.NameForConflict<Product>(context, product_dto.Name);
			product_updated.Description = product_dto.Description;
			product_updated.Price = product_dto.Price;
			context.SaveChanges();
			return product_updated;
		}
		public Product Patch(int id, ProductPatchDTO product_patch_dto) { // Patching a product.
			Product product_patched = Supply.ByID<Product>(context, id);
			if (product_patch_dto.IDCategory != default(int)) product_patched.IDCategory = Check.ID<Category>(context, product_patch_dto.IDCategory);
			if (!string.IsNullOrEmpty(product_patch_dto.Name)) product_patched.Name = Check.NameForConflict<List>(context, product_patch_dto.Name);
			if (!string.IsNullOrEmpty(product_patch_dto.Description)) product_patched.Description = product_patch_dto.Description;
			context.SaveChanges();
			return product_patched;
		}
	}
}