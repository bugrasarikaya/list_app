using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
using list_api.Services;
namespace list_api.Repository {
	public class ClientRepository : IClientRepository {
		public int IDUser { get; set; }
		private readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public ClientRepository(IDistributedCache cache, IListApiDbContext context, IMapper mapper, IMessageService messager) { // Constructing.
			this.cache = cache;
			this.context = context;
			this.mapper = mapper;
			this.messager = messager;
		}
		public List CreateList(ClientListDTO list_client_dto) { // Creating a list of client.
			List list_created = new List() { IDCategory = Check.ID<Category>(cache, context, list_client_dto.IDCategory), IDStatus = Supply.ByID<Status>(cache, context, (int)Enumerator.Status.Uncompleted).ID, IDUser = Check.ID<User>(cache, context, IDUser), Name = Check.NameForConflict<List>(cache, context, list_client_dto.Name, IDUser), Description = list_client_dto.Description, DateTimeCreating = DateTime.Now };
			list_created.DateTimeUpdating = list_created.DateTimeCreating;
			context.Lists.Add(list_created);
			context.SaveChanges();
			return list_created;
		}
		public void DeleteList(string param_list) { // Deleting a list of client.
			List list_deleted;
			if (int.TryParse(param_list, out int id_list)) list_deleted = Supply.ByID<List>(cache, context, id_list);
			else list_deleted = Supply.ByName<List>(cache, context, param_list, IDUser);
			Check.Status(cache, context, list_deleted);
			context.Lists.Remove(list_deleted);
			context.SaveChanges();
		}
		public ListViewModel GetList(string param_list) { // Getting a list of client.
			List list;
			if (int.TryParse(param_list, out int id_list)) list = Supply.ByID<List>(cache, context, id_list);
			else list = Supply.ByName<List>(cache, context, param_list, IDUser);
			return Fill.ViewModel<ListViewModel, List>(cache, context, mapper, list);
		}
		public ICollection<ListViewModel> ListLists() { // Listing all lists of client.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDUser == IDUser).OrderByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) list_list_view_model.Add(Fill.ViewModel<ListViewModel, List>(cache, context, mapper, Supply.ByID<List>(cache, context, id)));
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListListsByCategory(string param_category) { // Listing all lists of client by category.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.IDUser == IDUser).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) list_list_view_model.Add(Fill.ViewModel<ListViewModel, List>(cache, context, mapper, Supply.ByID<List>(cache, context, id)));
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeCompleting(DateTime date_time_completing) { // // Listing all lists of client, which have a specific creating date time.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeCompleting == date_time_completing && l.IDUser == IDUser).OrderByDescending(l => l.DateTimeCompleting).Select(l => l.ID).ToList()) list_list_view_model.Add(Fill.ViewModel<ListViewModel, List>(cache, context, mapper, Supply.ByID<List>(cache, context, id)));
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeCreating(DateTime date_time_creating) { // // Listing all lists of client, which have a specific creating date time.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeCreating == date_time_creating && l.IDUser == IDUser).OrderByDescending(l => l.DateTimeCreating).Select(l => l.ID).ToList()) list_list_view_model.Add(Fill.ViewModel<ListViewModel, List>(cache, context, mapper, Supply.ByID<List>(cache, context, id)));
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeUpdating(DateTime date_time_updating) { // // Listing all lists of client, which have a specific updating date time.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeUpdating == date_time_updating && l.IDUser == IDUser).OrderByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) list_list_view_model.Add(Fill.ViewModel<ListViewModel, List>(cache, context, mapper, Supply.ByID<List>(cache, context, id)));
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCompleting(string param_category, DateTime date_time_completing) { // Listing all lists of client which have a specific category and completing date time.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeCompleting == date_time_completing && l.IDUser == IDUser).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeCompleting).Select(l => l.ID).ToList()) list_list_view_model.Add(Fill.ViewModel<ListViewModel, List>(cache, context, mapper, Supply.ByID<List>(cache, context, id)));
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCreating(string param_category, DateTime date_time_creating) { // Listing all lists of client which have a specific category and creating date time.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeCreating == date_time_creating && l.IDUser == IDUser).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeCreating).Select(l => l.ID).ToList()) list_list_view_model.Add(Fill.ViewModel<ListViewModel, List>(cache, context, mapper, Supply.ByID<List>(cache, context, id)));
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeUpdating(string param_category, DateTime date_time_updating) { // Listing all lists of client which have a specific category and updating date time.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeUpdating == date_time_updating && l.IDUser == IDUser).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) list_list_view_model.Add(Fill.ViewModel<ListViewModel, List>(cache, context, mapper, Supply.ByID<List>(cache, context, id)));
			return list_list_view_model;
		}
		public ListViewModel UpdateList(string param_list, ClientListDTO list_client_dto) { // Updating a list of client.
			List list_updated;
			if (int.TryParse(param_list, out int id_list)) list_updated = Supply.ByID<List>(cache, context, id_list);
			else list_updated = Supply.ByName<List>(cache, context, param_list, IDUser);
			Check.Status(cache, context, list_updated);
			list_updated.IDCategory = Check.ID<Category>(cache, context, list_client_dto.IDCategory);
			list_updated.Name = Check.NameForConflict<List>(cache, context, list_client_dto.Name, IDUser);
			list_updated.Description = list_client_dto.Description;
			context.SaveChanges();
			return Fill.ViewModel<ListViewModel, List>(cache, context, mapper, list_updated);
		}
		public ListViewModel PatchList(string param_list, ClientListPatchDTO list_client_patch_dto) { // Patching a list of client.
			List list_patched;
			if (int.TryParse(param_list, out int id_list)) list_patched = Supply.ByID<List>(cache, context, id_list);
			else list_patched = Supply.ByName<List>(cache, context, param_list, IDUser);
			Check.Status(cache, context, list_patched);
			if (list_client_patch_dto.IDCategory != default(int)) list_patched.IDCategory = Check.ID<Category>(cache, context, list_client_patch_dto.IDCategory);
			if (list_client_patch_dto.IDStatus != default(int)) list_patched.IDStatus = Check.ID<Status>(cache, context, list_client_patch_dto.IDStatus);
			if (!string.IsNullOrEmpty(list_client_patch_dto.Name)) list_patched.Name = Check.NameForConflict<List>(cache, context, list_client_patch_dto.Name, IDUser);
			if (!string.IsNullOrEmpty(list_client_patch_dto.Description)) list_patched.Name = list_client_patch_dto.Description;
			list_patched.DateTimeUpdating = DateTime.Now;
			if (list_patched.IDStatus == Supply.ByID<Status>(cache, context, (int)Enumerator.Status.Completed).ID) list_patched.DateTimeCompleting = list_patched.DateTimeUpdating;
			context.SaveChanges();
			ListViewModel list_view_model = Fill.ViewModel<ListViewModel, List>(cache, context, mapper, list_patched);
			if (list_patched.IDStatus == Supply.ByID<Status>(cache, context, (int)Enumerator.Status.Completed).ID) messager.Publish(list_view_model);
			return list_view_model;
		}
		public ListViewModel AddProduct(ListProductDTO list_product_dto) { // Adding a product to a list of client.
			List list = Supply.ByID<List>(cache, context, list_product_dto.IDList);
			Check.Status(cache, context, list);
			Product product = Supply.ByID<Product>(cache, context, list_product_dto.IDProduct);
			ListProduct list_product_created = new ListProduct() { IDList = list_product_dto.IDList, IDProduct = list_product_dto.IDProduct, Quantity = list_product_dto.Quantity };
			Check.ForeignIDForConflict<ListProduct, ListProduct>(cache, context, list_product_dto.IDList, list_product_dto.IDProduct);
			context.ListProducts.Add(list_product_created);
			list.TotalCost += product.Price * list_product_dto.Quantity;
			list.DateTimeUpdating = DateTime.Now;
			context.SaveChanges();
			return Fill.ViewModel<ListViewModel, List>(cache, context, mapper, list);
		}
		public ListViewModel RemoveProduct(string param_list, int id_product) { // Removing a product from a list of client.
			List list;
			if (int.TryParse(param_list, out int id_list)) list = Supply.ByID<List>(cache, context, id_list);
			else list = Supply.ByName<List>(cache, context, param_list, IDUser);
			Check.Status(cache, context, list);
			Product product = Supply.ByID<Product>(cache, context, id_product);
			ListProduct list_product_deleted = Supply.ByID<ListProduct>(cache, context, id_list, id_product);
			list.TotalCost -= product.Price * list_product_deleted.Quantity;
			list.DateTimeUpdating = DateTime.Now;
			context.ListProducts.Remove(list_product_deleted);
			context.SaveChanges();
			return Fill.ViewModel<ListViewModel, List>(cache, context, mapper, list);
		}
		public ClientUserViewModel GetUser() { // Getting a user info of client.
			return Fill.ViewModel<ClientUserViewModel, User>(cache, context, mapper, Supply.ByID<User>(cache, context, IDUser));
		}
		public ClientUserViewModel UpdateUser(ClientUserDTO user_client_dto) { // Updating user info of client.
			User user_updated = Supply.ByID<User>(cache, context, IDUser);
			user_updated.Name = Check.NameForConflict<User>(cache, context, user_client_dto.Name);
			user_updated.Password = user_client_dto.Password;
			context.SaveChanges();
			return Fill.ViewModel<ClientUserViewModel, User>(cache, context, mapper, user_updated);
		}
		public ClientUserViewModel PatchUser(ClientUserPatchDTO client_user_patch_dto) { // Patching a user.
			User user_patched = Supply.ByID<User>(cache, context, IDUser);
			if (!string.IsNullOrEmpty(client_user_patch_dto.Name)) user_patched.Name = Check.NameForConflict<List>(cache, context, client_user_patch_dto.Name);
			if (!string.IsNullOrEmpty(client_user_patch_dto.Password)) user_patched.Password = client_user_patch_dto.Password;
			context.SaveChanges();
			return Fill.ViewModel<ClientUserViewModel, User>(cache, context, mapper, user_patched);
		}
	}
}