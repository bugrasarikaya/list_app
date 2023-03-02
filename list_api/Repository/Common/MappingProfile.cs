using AutoMapper;
using list_api.Models;
using list_api.Models.ViewModels;
namespace list_api.Repository.Common {
	public class MappingProfile : Profile {
		public MappingProfile() {
			CreateMap<Brand, BrandViewModel>();
			CreateMap<Category, CategoryViewModel>();
			CreateMap<List, ListViewModel>();
			CreateMap<ListProduct, ProductViewModel>().ForSourceMember(lp => lp.ID, opt => opt.DoNotValidate());
			CreateMap<Product, ProductViewModel>();
			CreateMap<Role, RoleViewModel>();
			CreateMap<Status, StatusViewModel>();
			CreateMap<User, ClientUserViewModel>();
		}
	}
}