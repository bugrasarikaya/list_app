using AutoMapper;
using list_api.Data;
using list_api.Models;
using list_api.Models.ViewModels;
using Microsoft.Extensions.Caching.Distributed;
namespace list_api.Repository.Common {
	public static class Fill {
		public static T1 ViewModel<T1, T2>(IDistributedCache cache, IListApiDbContext context, IMapper mapper, T2 record) {
			if (typeof(T1) == typeof(BrandViewModel) && typeof(T2) == typeof(Brand)) {
				Brand brand = (Brand)Convert.ChangeType(record, typeof(Brand))!;
				BrandViewModel brand_view_model = mapper.Map<BrandViewModel>(brand);
				return (T1)Convert.ChangeType(brand_view_model, typeof(T1));
			} else if (typeof(T1) == typeof(CategoryViewModel) && typeof(T2) == typeof(Category)) {
				Category category = (Category)Convert.ChangeType(record, typeof(Category))!;
				CategoryViewModel category_view_model = mapper.Map<CategoryViewModel>(category);
				return (T1)Convert.ChangeType(category_view_model, typeof(T1));
			} else if (typeof(T1) == typeof(ClientUserViewModel) && typeof(T2) == typeof(User)) {
				User user = (User)Convert.ChangeType(record, typeof(User))!;
				ClientUserViewModel client_user_view_model = mapper.Map<ClientUserViewModel>(user);
				return (T1)Convert.ChangeType(client_user_view_model, typeof(T1));
			} else if (typeof(T1) == typeof(ListViewModel) && typeof(T2) == typeof(List)) {
				List list = (List)Convert.ChangeType(record, typeof(List))!;
				ListViewModel list_view_model = mapper.Map<ListViewModel>(list);
				list_view_model.Category = Supply.ByID<Category>(cache, context, list.IDCategory);
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<Product>(cache, context).Where(p => Supply.List<ListProduct>(cache, context).Any(lp => lp.IDProduct == p.ID)).ToList());
				list_view_model.Products = mapper.Map<List<ProductViewModel>>(Supply.List<ListProduct>(cache, context).Where(lp => Supply.List<Product>(cache, context).Any(p => p.ID == lp.IDProduct)).ToList());
				list_view_model.User = mapper.Map<ClientUserViewModel>(Supply.ByID<User>(cache, context, list.IDUser));
				return (T1)Convert.ChangeType(list_view_model, typeof(T1));
			} else if (typeof(T1) == typeof(ProductViewModel) && typeof(T2) == typeof(Product)) {
				Product product = (Product)Convert.ChangeType(record, typeof(Product))!;
				ProductViewModel product_view_model = mapper.Map<ProductViewModel>(product);
				product_view_model.NameBrand = Supply.ByID<Brand>(cache, context, product.IDBrand).Name;
				product_view_model.NameCategory = Supply.ByID<Category>(cache, context, product.IDCategory).Name;
				return (T1)Convert.ChangeType(product_view_model, typeof(T1));
			} else if (typeof(T1) == typeof(RoleViewModel) && typeof(T2) == typeof(Role)) {
				Role role = (Role)Convert.ChangeType(record, typeof(Role))!;
				RoleViewModel role_view_model = mapper.Map<RoleViewModel>(role);
				return (T1)Convert.ChangeType(role_view_model, typeof(T1));
			} else if (typeof(T1) == typeof(StatusViewModel) && typeof(T2) == typeof(Status)) {
				Status status = (Status)Convert.ChangeType(record, typeof(Status))!;
				StatusViewModel status_view_model = mapper.Map<StatusViewModel>(status);
				return (T1)Convert.ChangeType(status_view_model, typeof(T1));
			} else {
				User user = (User)Convert.ChangeType(record, typeof(User))!;
				UserViewModel user_view_model = mapper.Map<UserViewModel>(user);
				user_view_model.NameRole = Supply.ByID<Role>(cache, context, user.IDRole).Name;
				return (T1)Convert.ChangeType(user_view_model, typeof(T1));
			}
		}
	}
}