using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Models.ViewModels;
using list_api.Repository.Common;
using list_api.Repository.Interface;
namespace list_api.Repository {
	public class ListRepository : IListRepository {
		private readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public ListRepository(IDistributedCache cache, IListApiDbContext context, IMapper mapper) { // Constructing.
			this.cache = cache;
			this.context = context;
			this.mapper = mapper;
		}
		public List Create(ListDTO list_dto) { // Creating a list.
			List list_created = new List() { IDCategory = Check.ID<Category>(cache, context, list_dto.IDCategory), IDStatus = Supply.ByID<Status>(cache, context, (int)Enumerator.Status.Uncompleted).ID, Name = Check.NameForConflict<List>(cache, context, list_dto.Name, list_dto.IDUser), IDUser = Check.ID<User>(cache, context, list_dto.IDUser), Description = list_dto.Description, DateTimeCreating = DateTime.Now };
			list_created.DateTimeUpdating = list_created.DateTimeCreating;
			context.Lists.Add(list_created);
			context.SaveChanges();
			return list_created;
		}
		public void Delete(int id_list) { // Deleting a list.
			List list_deleted = Supply.ByID<List>(cache, context, id_list);
			Check.Status(cache, context, list_deleted);
			context.Lists.Remove(list_deleted);
			context.SaveChanges();
		}
		public ListViewModel Get(int id_list) { // Getting a list.
			List list = Supply.ByID<List>(cache, context, id_list);
			ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
			list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
			list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
			list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
			list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
			return list_view_model;
		}
		public ICollection<ListViewModel> List() { // Listing all lists.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).OrderBy(l => l.ID).OrderByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategory(string param_category) { // Listing all lists which have a specific category.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeCompleting(DateTime date_time_completing) { // Listing all lists which have a specific creating date time.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeCompleting == date_time_completing).OrderByDescending(l => l.DateTimeCompleting).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeCreating(DateTime date_time_creating) { // Listing all lists which have a specific completing date time.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeCreating == date_time_creating).OrderByDescending(l => l.DateTimeCreating).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeUpdating(DateTime date_time_updating) { // Listing all lists which have a specific updating date time.
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeUpdating == date_time_updating).OrderByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByUser(string param_user) { // Listing all lists which have a specific user.
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDUser == user.ID).OrderBy(l => Supply.List<User>(cache, context).Where(u => u.ID == l.ID).Select(u => u.Name)).ThenByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCompleting(string param_category, DateTime date_time_completing) { // Listing all lists which have a specific category and completing date time.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeCompleting == date_time_completing).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeCompleting).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCreating(string param_category, DateTime date_time_creating) { // Listing all lists which have a specific category and creating date time.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeCreating == date_time_creating).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeCreating).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeUpdating(string param_category, DateTime date_time_updating) { // Listing all lists which have a specific category and updating date time.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeUpdating == date_time_updating).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeUpdating).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndUser(string param_category, string param_user) { // Listing all lists which have a specific category and user.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.IDUser == user.ID).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenBy(l => Supply.List<User>(cache, context).Where(u => u.ID == l.ID).Select(u => u.Name)).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeCompletingAndUser(DateTime date_time_completing, string param_user) { // Listing all lists which have a specific completing date time and user.
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeCompleting == date_time_completing && l.IDUser == user.ID).OrderByDescending(l => l.DateTimeUpdating).ThenBy(l => Supply.List<User>(cache, context).Where(u => u.ID == l.ID).Select(u => u.Name)).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeCreatingAndUser(DateTime date_time_creating, string param_user) { // Listing all lists which have a specific creating date time and user.
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeCreating == date_time_creating && l.IDUser == user.ID).OrderByDescending(l => l.DateTimeCreating).ThenBy(l => Supply.List<User>(cache, context).Where(u => u.ID == l.ID).Select(u => u.Name)).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByDateTimeUpdatingAndUser(DateTime date_time_updating, string param_user) { // Listing all lists which have a specific updating date time and user.
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.DateTimeUpdating == date_time_updating && l.IDUser == user.ID).OrderByDescending(l => l.DateTimeUpdating).ThenBy(l => Supply.List<User>(cache, context).Where(u => u.ID == l.ID).Select(u => u.Name)).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCompletingAndUser(string param_category, DateTime date_time_completing, string param_user) { // Listing all lists which have a specific category, completing date time and user.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeCompleting == date_time_completing && l.IDUser == user.ID).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeCompleting).ThenBy(l => Supply.List<User>(cache, context).Where(u => u.ID == l.ID).Select(u => u.Name)).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeCreatingAndUser(string param_category, DateTime date_time_creating, string param_user) { // Listing all lists which have a specific category, creating date time and user.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeCreating == date_time_creating && l.IDUser == user.ID).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeCreating).ThenBy(l => Supply.List<User>(cache, context).Where(u => u.ID == l.ID).Select(u => u.Name)).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ICollection<ListViewModel> ListByCategoryAndDateTimeUpdatingAndUser(string param_category, DateTime date_time_updating, string param_user) { // Listing all lists which have a specific category, updating date time and user.
			Category category;
			if (int.TryParse(param_category, out int id_category)) category = Supply.ByID<Category>(cache, context, id_category);
			else category = Supply.ByName<Category>(cache, context, param_category);
			User user;
			if (int.TryParse(param_user, out int id_user)) user = Supply.ByID<User>(cache, context, id_user);
			else user = Supply.ByName<User>(cache, context, param_user);
			ICollection<ListViewModel> list_list_view_model = new List<ListViewModel>();
			foreach (int id in Supply.List<List>(cache, context).Where(l => l.IDCategory == category.ID && l.DateTimeUpdating == date_time_updating && l.IDUser == user.ID).OrderBy(l => Supply.List<Category>(cache, context).Where(c => c.ID == l.ID).Select(c => c.Name)).ThenByDescending(l => l.DateTimeUpdating).ThenBy(l => Supply.List<User>(cache, context).Where(u => u.ID == l.ID).Select(u => u.Name)).Select(l => l.ID).ToList()) {
				List list = Supply.ByID<List>(cache, context, id);
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				list_list_view_model.Add(list_view_model);
			}
			return list_list_view_model;
		}
		public ListViewModel Update(int id_list, ListDTO list_dto) { // Updating a list.
			List list_updated = Supply.ByID<List>(cache, context, id_list);
			Check.Status(cache, context, list_updated);
			list_updated.IDCategory = Check.ID<Category>(cache, context, list_dto.IDCategory);
			list_updated.IDUser = Check.ID<User>(cache, context, list_dto.IDUser);
			list_updated.Name = Check.NameForConflict<List>(cache, context, list_dto.Name, list_dto.IDUser);
			list_updated.Description = list_dto.Description;
			list_updated.DateTimeUpdating = DateTime.Now;
			context.SaveChanges();
			return mapper.Map<ListViewModel>(list_updated);
		}
		public ListViewModel Patch(int id_list, ListPatchDTO list_patch_dto) { // Patch a list.
			List list_patched = Supply.ByID<List>(cache, context, id_list);
			Check.Status(cache, context, list_patched);
			if (list_patch_dto.IDCategory != default(int)) list_patched.IDCategory = Check.ID<Category>(cache, context, list_patch_dto.IDCategory);
			if (list_patch_dto.IDStatus != default(int)) list_patched.IDStatus = Check.ID<Status>(cache, context, list_patch_dto.IDStatus);
			if (list_patch_dto.IDUser != default(int)) list_patched.IDUser = Check.ID<User>(cache, context, list_patch_dto.IDUser);
			if (!string.IsNullOrEmpty(list_patch_dto.Name)) list_patched.Name = Check.NameForConflict<List>(cache, context, list_patch_dto.Name, list_patch_dto.IDUser);
			if (!string.IsNullOrEmpty(list_patch_dto.Description)) list_patched.Description = list_patch_dto.Description;
			list_patched.DateTimeUpdating = DateTime.Now;
			if (list_patched.IDStatus == Supply.ByID<Status>(cache, context, (int)Enumerator.Status.Completed).ID) list_patched.DateTimeCompleting = list_patched.DateTimeUpdating;
			context.SaveChanges();
			return mapper.Map<ListViewModel>(list_patched);
		}
		public ListViewModel AddProduct(ListProductDTO list_product_dto) { // Adding a product to a list.
			List list = Supply.ByID<List>(cache, context, list_product_dto.IDList);
			Check.Status(cache, context, list);
			Product product = Supply.ByID<Product>(cache, context, list_product_dto.IDProduct);
			ListProduct list_product_created = new ListProduct() { IDList = list_product_dto.IDList, IDProduct = list_product_dto.IDProduct, Quantity = list_product_dto.Quantity };
			Check.ForeignIDForConflict<ListProduct, ListProduct>(cache, context, list_product_dto.IDList, list_product_dto.IDProduct);
			context.ListProducts.Add(list_product_created);
			list.TotalCost += product.Price * list_product_dto.Quantity;
			list.DateTimeUpdating = DateTime.Now;
			context.SaveChanges();
			return Get(list_product_dto.IDList)!;
		}
		public ListViewModel RemoveProduct(int id_list, int id_product) { // Removing a product from a list.
			List list = Supply.ByID<List>(cache, context, id_list);
			Check.Status(cache, context, list);
			Product product = Supply.ByID<Product>(cache, context, id_product);
			ListProduct list_product_deleted = Supply.ByID<ListProduct>(cache, context, id_list, id_product);
			list.TotalCost -= product.Price * list_product_deleted.Quantity;
			list.DateTimeUpdating = DateTime.Now;
			context.ListProducts.Remove(list_product_deleted);
			context.SaveChanges();
			return Get(id_list);
		}
	}
}