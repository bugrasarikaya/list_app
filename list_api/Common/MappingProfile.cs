using AutoMapper;
using list_api.Models;
namespace list_api.Common {
	public class MappingProfile : Profile {
		public MappingProfile() {
			CreateMap<List, ListViewModel>();
			CreateMap<Product, ProductViewModel>();
			CreateMap<ListProduct, ProductViewModel>().ForSourceMember(lp => lp.ID, opt => opt.DoNotValidate());
			CreateMap<User, UserViewModel>();
		}
	}
}