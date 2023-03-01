using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class ClientRepository : IClientRepository {
		private readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public int IDUser { get; set; }
		public ClientRepository(IDistributedCache cache, IListApiDbContext context, IMapper mapper) { // Constructing.
			this.cache = cache;
			this.context = context;
			this.mapper = mapper;
		}
		public List CreateList(ClientListDTO list_client_dto) { // Creating a list of client.
			List list_created = new List() { IDCategory = Check.ID<Category>(cache, context, list_client_dto.IDCategory), IDStatus = Supply.ByID<Status>(cache, context, 2).ID, IDUser = Check.ID<User>(cache, context, IDUser), Name = Check.NameForConflict<List>(cache, context, list_client_dto.Name), Description = list_client_dto.Description, DateTime = DateTime.Now };
			context.Lists.Add(list_created);
			context.SaveChanges();
			return list_created;
		}
		public void DeleteList(int id_list) { // Deleting a list of client.
			List list_deleted = Supply.ByID<List>(cache, context, id_list);
			context.Lists.Remove(list_deleted);
			context.SaveChanges();
		}
		public ListViewModel GetList(int id_list) { // Getting a list of client.
			List list = Supply.ByID<List>(cache, context, id_list);
			ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
			list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
			list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
			list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
			list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
			list_view_model.Status = Supply.ByID<Status>(cache, context, list.IDStatus).Name;
			return list_view_model;
		}
		public ICollection<ListViewModel> ListLists() { // Listing all lists of client.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in context.Lists.Where(l => l.IDUser == IDUser).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_view_model.Status = Supply.ByID<Status>(cache, context, list.IDStatus).Name;
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListListsByCategory(int id_category) { // Listing all lists of client by category.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == IDUser).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_view_model.Status = Supply.ByID<Status>(cache, context, list.IDStatus).Name;
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ListViewModel UpdateList(int id_list, ClientListDTO list_client_dto) { // Updating a list of client.
			List list_updated = Supply.ByID<List>(cache, context, id_list);
			list_updated.IDCategory = Check.ID<Category>(cache, context, list_client_dto.IDCategory);
			list_updated.IDStatus = Check.ID<Status>(cache, context, list_client_dto.IDStatus);
			list_updated.Name = Check.NameForConflict<List>(cache, context, list_client_dto.Name, IDUser);
			list_updated.Description = list_client_dto.Description;
			context.SaveChanges();
			return mapper.Map<ListViewModel>(list_updated);
		}
		public ListViewModel PatchList(int id_list, ClientListPatchDTO list_client_patch_dto) { // Patching a list of client.
			List list_patched = Supply.ByID<List>(cache, context, id_list);
			if (list_client_patch_dto.IDCategory != default(int)) list_patched.IDCategory = Check.ID<Category>(cache, context, list_client_patch_dto.IDCategory);
			if (list_client_patch_dto.IDStatus != default(int)) list_patched.IDStatus = Check.ID<Status>(cache, context, list_client_patch_dto.IDStatus);
			if (!string.IsNullOrEmpty(list_client_patch_dto.Name)) list_patched.Name = Check.NameForConflict<List>(cache, context, list_client_patch_dto.Name, IDUser);
			if (!string.IsNullOrEmpty(list_client_patch_dto.Description)) list_patched.Name = list_client_patch_dto.Description;
			context.SaveChanges();
			return mapper.Map<ListViewModel>(list_patched);
		}
		public ListViewModel AddProduct(ListProductDTO list_product_dto) { // Adding a product to a list of client.
			List list = Supply.ByID<List>(cache, context, list_product_dto.IDList);
			Product product = Supply.ByID<Product>(cache, context, list_product_dto.IDProduct);
			ListProduct list_product_created = new ListProduct() { IDList = list_product_dto.IDList, IDProduct = list_product_dto.IDProduct, Quantity = list_product_dto.Quantity };
			Check.ForeignIDForConflict<ListProduct, ListProduct>(cache, context, list_product_dto.IDList, list_product_dto.IDProduct);
			context.ListProducts.Add(list_product_created);
			list.TotalCost += product.Price * list_product_dto.Quantity;
			list.DateTime = DateTime.Now;
			list.IDStatus = Supply.ByID<Status>(cache, context, 2).ID;
			context.SaveChanges();
			return GetList(list_product_dto.IDList);
		}
		public ListViewModel RemoveProduct(int id_list, int id_product) { // Removing a product from a list of client.
			List list = Supply.ByID<List>(cache, context, id_list);
			Product? product = Supply.ByID<Product>(cache, context, id_product);
			ListProduct list_product_deleted = Supply.ByID<ListProduct>(cache, context, id_list, id_product);
			list.TotalCost -= product.Price * list_product_deleted.Quantity;
			list.DateTime = DateTime.Now;
			list.IDStatus = Supply.ByID<Status>(cache, context, 2).ID;
			context.ListProducts.Remove(list_product_deleted);
			context.SaveChanges();
			return GetList(id_list);
		}
		public ListViewModel ClearProducts(int id_list) { // Removing all products from a list of client.
			List list = Supply.ByID<List>(cache, context, id_list);
			list.TotalCost = 0.0;
			list.DateTime = DateTime.Now;
			list.IDStatus = Supply.ByID<Status>(cache, context, 2).ID;
			context.ListProducts.RemoveRange(context.ListProducts.Where(lp => lp.IDList == id_list));
			context.SaveChanges();
			return GetList(id_list);
		}
		public ClientUserViewModel GetUser() { // Getting a user info of client.
			User user = Supply.ByID<User>(cache, context, IDUser);
			return mapper.Map<ClientUserViewModel>(user);
		}
		public ClientUserViewModel UpdateUser(ClientUserDTO user_client_dto) { // Updating user info of client.
			User user_updated = Supply.ByID<User>(cache, context, IDUser);
			user_updated.Name = Check.NameForConflict<User>(cache, context, user_client_dto.Name);
			user_updated.Password = user_client_dto.Password;
			context.SaveChanges();
			return mapper.Map<ClientUserViewModel>(user_updated);
		}
		public ClientUserViewModel PatchUser(ClientUserPatchDTO client_user_patch_dto) { // Patching a user.
			User user_patched = Supply.ByID<User>(cache, context, IDUser);
			if (!string.IsNullOrEmpty(client_user_patch_dto.Name)) user_patched.Name = Check.NameForConflict<List>(cache, context, client_user_patch_dto.Name);
			if (!string.IsNullOrEmpty(client_user_patch_dto.Password)) user_patched.Password = client_user_patch_dto.Password;
			context.SaveChanges();
			return mapper.Map<ClientUserViewModel>(user_patched);
		}
	}
}