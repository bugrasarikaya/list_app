using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
using System.Net;
using System.Web.Http;
using AutoMapper;
using System.Collections.Generic;

namespace list_api.Repository {
	public class ListRepository : IListRepository {
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public ListRepository(IListApiDbContext context, IMapper mapper) { // Constructing.
			this.context = context;
			this.mapper = mapper;
		}
		public List Create(List list) { // Creating a list.
			List? list_created = new List() { Name = list.Name, Description = list.Description, IDCategory = list.IDCategory, IDUser = list.IDUser };
			if (context.Lists.Any(l => l.Name == list.Name)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "List already exists." });
			context.Lists.Add(list_created);
			context.SaveChanges();
			return list_created;
		}
		public List? Delete(int id_list) { // Deleting a list.
			List? list_deleted = Supply<List>(id_list);
			if (list_deleted != null) {
				context.Lists.Remove(list_deleted);
				context.SaveChanges();
			}
			return list_deleted;
		}
		public ListViewModel Get(int id_list) { // Getting a list.
			List list = Supply<List>(id_list);
			ListViewModel list_view_model = mapper.Map<ListViewModel>(Supply<List>(id_list));
			list_view_model.Category = Supply<Category>(list.IDCategory);
			list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
			list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
			list_view_model.User = mapper.Map<UserViewModel>(Supply<User>(list.IDUser));
			return list_view_model;
		}
		public ICollection<ListViewModel> List() { // Listing all lists.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in context.Lists.Select(l => l.ID).ToList()) {
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
		public ICollection<ListViewModel>? ListByCategory(int id_category) { // Listing all lists which have a specific category.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in context.Lists.Where(l => l.IDCategory == id_category).Select(l => l.ID).ToList()) {
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
		//public ICollection<ListViewModel>? ListByNameCategory(string name_category) { // Listing all lists which have a specific category name.
		//	ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
		//	foreach (int id in context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category)).Select(l => l.ID).ToList()) {
		//		List list = Supply<List>(id);
		//		ListViewModel list_view_model = mapper.Map<ListViewModel>(Supply<List>(id));
		//		list_view_model.Category = Supply<Category>(list.IDCategory);
		//		list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
		//		list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
		//		list_view_model.User = mapper.Map<UserViewModel>(Supply<User>(list.IDUser));
		//		list_list_view_model.Add(list_view_model);
		//	}
		//	return list_list_view_model;
		//}
		public ICollection<ListViewModel>? ListByUser(int id_user) { // Listing all lists which have a specific user.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in context.Lists.Where(l => l.IDUser == id_user).Select(l => l.ID).ToList()) {
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
		//public ICollection<ListViewModel>? ListByNameUser(string name_user) { // Listing all lists which have a specific user name.
		//	ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
		//	foreach (int id in context.Lists.Where(l => context.Users.Any(u => u.ID == l.IDUser && u.Name == name_user)).Select(l => l.ID).ToList()) {
		//		List list = Supply<List>(id);
		//		ListViewModel list_view_model = mapper.Map<ListViewModel>(Supply<List>(id));
		//		list_view_model.Category = Supply<Category>(list.IDCategory);
		//		list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
		//		list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
		//		list_view_model.User = mapper.Map<UserViewModel>(Supply<User>(list.IDUser));
		//		list_list_view_model.Add(list_view_model);
		//	}
		//	return list_list_view_model;
		//}
		public ICollection<ListViewModel>? ListByCategoryAndUser(int id_category, int id_user) { // Listing all lists which have a specific category and user.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == id_user).Select(l => l.ID).ToList()) {
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
		//public ICollection<ListViewModel>? ListByNameCategoryAndNameUser(string name_category, string name_user) { // Listing all lists which have a specific category name and user name.
		//	ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
		//	foreach (int id in context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && context.Users.Any(u => u.ID == l.IDUser && u.Name == name_user)).Select(l => l.ID).ToList()) {
		//		List list = Supply<List>(id);
		//		ListViewModel list_view_model = mapper.Map<ListViewModel>(Supply<List>(id));
		//		list_view_model.Category = Supply<Category>(list.IDCategory);
		//		list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.Products.Where(p => context.ListProducts.Any(lp => lp.IDProduct == p.ID)).ToList());
		//		list_view_model.Products = mapper.Map<List<ProductViewModel>>(context.ListProducts.Where(lp => context.Products.Any(p => p.ID == lp.IDProduct)).ToList());
		//		list_view_model.User = mapper.Map<UserViewModel>(Supply<User>(list.IDUser));
		//		list_list_view_model.Add(list_view_model);
		//	}
		//	return list_list_view_model;
		//}
		public List Update(int id_list, List list) { // Updating a list.
			List list_updated = Supply<List>(id_list);
			list_updated.IDCategory = list.IDCategory;
			list_updated.IDUser = list.IDUser;
			if (context.Lists.Any(l => l.Name == list.Name)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "List already exists." });
			list_updated.Name = list.Name;
			list_updated.Description = list.Description;
			list_updated.Status = list.Status;
			context.SaveChanges();
			return list_updated;
		}
		public List SetCompleted(int id_list) { // Setting status of list to "Completed".
			List list = Supply<List>(id_list);
			list.Status = "Completed";
			context.SaveChanges();
			return list;
		}
		public ListViewModel Add(ListProduct list_product) { // Adding a product to a list.
			List list = Supply<List>(list_product.IDList);
			Product? product = Supply<Product>(list_product.IDProduct);
			ListProduct list_product_added = new ListProduct() { IDList = list_product.IDList, IDProduct = list_product.IDProduct, Quantity = list_product.Quantity };
			if (context.ListProducts.Any(lp => lp.IDList == list_product_added.IDList && lp.IDProduct == list_product_added.IDProduct)) throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Conflict) { ReasonPhrase = "Product already exists in list." });
			context.ListProducts.Add(list_product_added);
			list.TotalCost += product.Price * list_product.Quantity;
			list.DateTime = DateTime.Now;
			list.Status = "Uncompleted";
			context.SaveChanges();
			return Get(list.ID);
		}
		public ListViewModel? Remove(int id_list, int id_product) { // Removing a product from a list.
			List list = Supply<List>(id_list);
			Product? product = Supply<Product>(id_product);
			ListProduct? list_product_deleted = context.ListProducts.FirstOrDefault(lp => lp.IDList == id_list && lp.IDProduct == id_product);
			if (list_product_deleted != null) {
				list.TotalCost -= product.Price * list_product_deleted.Quantity;
				list.DateTime = DateTime.Now;
				list.Status = "Uncompleted";
				context.ListProducts.Remove(list_product_deleted);
				context.SaveChanges();
				return Get(list.ID);
			}
			return null;
		}
		public List Clear(int id_list) { // Removing all products from a list.
			List list = Supply<List>(id_list);
			list.TotalCost = 0.0;
			list.DateTime = DateTime.Now;
			list.Status = "Uncompleted";
			context.ListProducts.RemoveRange(context.ListProducts.Where(lp => lp.IDList == id_list));
			context.SaveChanges();
			return list;
		}
		public T Supply<T>(int id) { // Checking existence of a record and supplying.
			if (typeof(T) == typeof(List)) {
				List? list = context.Lists.FirstOrDefault(l => l.ID == id);
				if (list != null) return (T)Convert.ChangeType(list, typeof(T));
				else throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "List could not be found" });
			} else if (typeof(T) == typeof(Product)) {
				Product? product = context.Products.FirstOrDefault(p => p.ID == id);
				if (product != null) return (T)Convert.ChangeType(product, typeof(T));
				else throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "Product could not be found." });
			} else if (typeof(T) == typeof(Category)) {
				Category? category = context.Categories.FirstOrDefault(p => p.ID == id);
				if (category != null) return (T)Convert.ChangeType(category, typeof(T));
				else throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "Category could not be found." });
			} else {
				User? user = context.Users.FirstOrDefault(u => u.ID == id);
				if (user != null) return (T)Convert.ChangeType(user, typeof(T));
				else throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound) { ReasonPhrase = "User could not be found." });
			}
		}
	}
}