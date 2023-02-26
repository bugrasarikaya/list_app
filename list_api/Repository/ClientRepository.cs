using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
using System.Net;
using System.Web.Http;
using AutoMapper;
namespace list_api.Repository {
	public class ClientRepository : IClientRepository {
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public int IDUser { get; set; }
		public ClientRepository(IListApiDbContext context, IMapper mapper) { // Constructing.
			this.context = context;
			this.mapper = mapper;
		}
		public List CreateList(List list) { // Creating a list of client.
			List? list_created = new List() { Name = list.Name, Description = list.Description, IDCategory = list.IDCategory, IDUser = IDUser };
			if (context.Lists.Any(l => l.Name == list.Name && l.IDUser == IDUser)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "List already exists." });
			context.Lists.Add(list_created);
			context.SaveChanges();
			return list_created;
		}
		public List? Deletelist(int id_list) { // Deleting a list of client.
			List? list_deleted = Supply<List>(id_list);
			if (list_deleted != null) {
				context.Lists.Remove(list_deleted);
				context.SaveChanges();
			}
			return list_deleted;
		}
		public ListViewModel? GetList(int id_list) { // Getting a list of client.
			List list = Supply<List>(id_list);
			ListViewModel list_view_model = mapper.Map<ListViewModel>(Supply<List>(id_list));
			list_view_model.Category = Supply<Category>(list.IDCategory);
			list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
			list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
			list_view_model.User = mapper.Map<UserViewModel>(Supply<User>(list.IDUser));
			return list_view_model;
		}
		public ICollection<ListViewModel> ListLists() { // Listing all lists of client.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in context.Lists.Where(l => l.IDUser == IDUser).Select(l => l.ID).ToList()) {
				List list = Supply<List>(id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(Supply<List>(id));
				list_view_model.Category = Supply<Category>(list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<UserViewModel>(Supply<User>(list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListListsByCategory(int id_category) { // Listing all lists of client by category.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == IDUser).Select(l => l.ID).ToList()) {
				List list = Supply<List>(id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(Supply<List>(id));
				list_view_model.Category = Supply<Category>(list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<UserViewModel>(Supply<User>(list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public List UpdateList(int id_list, List list) { // Updating a list of client.
			List? list_updated = Supply<List>(id_list);
			list_updated.IDCategory = list.IDCategory;
			list_updated.IDUser = IDUser;
			if (context.Lists.Any(l => l.Name == list.Name && l.IDUser == IDUser)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "List already exists." });
			list_updated.Name = list.Name;
			list_updated.Description = list.Description;
			context.SaveChanges();
			return list_updated;
		}
		public List SetCompleted(int id_list) { // Setting status of list of client, to "Completed".
			List list = Supply<List>(id_list);
			list.Status = "Completed";
			context.SaveChanges();
			return list;
		}
		//public ICollection<Product>? ListListProducts(int id_list) { // Listing all products in a list of client.
		//	List list = Supply<List>(id_list);
		//	List<int>? list_product_ids = context.ListProducts.Where(lp => lp.ID == list.ID).Select(lp => lp.IDProduct).ToList();
		//	if (list_product_ids != null) return context.Products.Where(p => list_product_ids.Contains(p.ID)).ToList();
		//	else return null;
		//}
		public ListViewModel AddProduct(ListProduct list_product) { // Adding a product to a list of client.
			List list = Supply<List>(list_product.IDList);
			Product? product = Supply<Product>(list_product.IDProduct);
			ListProduct list_product_created = new ListProduct() { IDList = list_product.IDList, IDProduct = list_product.IDProduct, Quantity = list_product.Quantity };
			if (context.ListProducts.Any(lp => lp.IDList == list_product_created.IDList && lp.IDProduct == list_product_created.IDProduct)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "Product already exists." });
			context.ListProducts.Add(list_product_created);
			list.TotalCost += product.Price * list_product.Quantity;
			list.DateTime = DateTime.Now;
			list.Status = "Uncompleted";
			context.SaveChanges();
			return GetList(list_product.IDList)!;
		}
		public ListViewModel? RemoveProduct(int id_list, int id_product) { // Removing a product from a list of client.
			List list = Supply<List>(id_list);
			Product? product = Supply<Product>(id_product);
			ListProduct? list_product_deleted = context.ListProducts.FirstOrDefault(lp => lp.IDList == id_list && lp.IDProduct == id_product);
			if (list_product_deleted != null) {
				list.TotalCost -= product.Price * list_product_deleted.Quantity;
				list.DateTime = DateTime.Now;
				list.Status = "Uncompleted";
				context.ListProducts.Remove(list_product_deleted);
				context.SaveChanges();
				return GetList(id_list);
			}
			return null;
		}
		public List Clear(int id_list) { // Removing all products from a list of client.
			List list = Supply<List>(id_list);
			list.TotalCost = 0.0;
			list.DateTime = DateTime.Now;
			list.Status = "Uncompleted";
			context.ListProducts.RemoveRange(context.ListProducts.Where(lp => lp.IDList == id_list));
			context.SaveChanges();
			return list;
		}
		public UserViewModel GetUser() { // Getting a user info of client.
			User user = Supply<User>(IDUser);
			UserViewModel user_view_model = mapper.Map<UserViewModel>(user);
			return user_view_model;
		}
		public UserViewModel UpdateUser(User user) { // Updating user info of client.
			User user_updated = Supply<User>(IDUser);
			if (context.Users.Any(u => u.Name == user.Name)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "User already exists." });
			user_updated.Name = user.Name;
			user_updated.Password = user.Password;
			context.SaveChanges();
			UserViewModel user_view_model = mapper.Map<UserViewModel>(user_updated);
			return user_view_model;
		}
		public T Supply<T>(int id) { // Checking existence of a record and supplying.
			if (typeof(T) == typeof(List)) {
				List? list = context.Lists.FirstOrDefault(l => l.ID == id && l.IDUser == IDUser);
				if (list != null) return (T)Convert.ChangeType(list, typeof(T));
				else throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "List could not be found" });
			} else if (typeof(T) == typeof(Product)) {
				Product? product = context.Products.FirstOrDefault(p => p.ID == id);
				if (product != null) return (T)Convert.ChangeType(product, typeof(T));
				else throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "Product could not be found." });
			} else {
				User? user = context.Users.FirstOrDefault(u => u.ID == id);
				if (user != null) return (T)Convert.ChangeType(user, typeof(T));
				else throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "User could not be found." });
			}
		}
	}
}