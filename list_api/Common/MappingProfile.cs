using AutoMapper;
using list_api.Models;
namespace list_api.Common {
	public class MappingProfile : Profile {
		public MappingProfile() {
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