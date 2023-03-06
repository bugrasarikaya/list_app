using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.BrandRepositoryTests {
	public class GetTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public GetTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Get_UnknownBrand_ByID_ThrowsException() {
			int id_brand = 10;
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Get(id_brand.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Brand could not be found.");
		}
		[Fact]
		public void Get_UnknownBrand_ByName_ThrowsException() {
			string name_brand = "Dove";
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Get(name_brand)).Should().Throw<NotFoundException>().And.Message.Should().Be("Brand could not be found.");
		}
		[Fact]
		public void Get_Brand_ByID_ShouldBeReturn() {
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			int id_brand = 1;
			FluentActions.Invoking(() => brand_repository.Get(id_brand.ToString())).Invoke();
			Brand? brand = context.Brands.SingleOrDefault(b => b.ID == id_brand);
			brand.Should().NotBeNull();
		}
		[Fact]
		public void Get_Brand_ByName_ShouldBeReturn() {
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			string name_brand = "Alfa";
			FluentActions.Invoking(() => brand_repository.Get(name_brand)).Invoke();
			Brand? brand = context.Brands.SingleOrDefault(b => b.Name == name_brand);
			brand.Should().NotBeNull();
		}
	}
}