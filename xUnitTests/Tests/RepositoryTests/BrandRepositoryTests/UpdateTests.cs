using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.BrandRepositoryTests {
	public class UpdateTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public UpdateTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Update_UnknownBrand_ByID_ThrowsException() {
			int id_brand = 10;
			BrandDTO brand_dto = new BrandDTO { Name = "Doritos" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Update(id_brand.ToString(), brand_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Brand could not be found.");
		}
		[Fact]
		public void Update_UnknownBrand_ByName_ThrowsException() {
			string name_brand = "Dove";
			BrandDTO brand_dto = new BrandDTO { Name = "Doritos" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Update(name_brand, brand_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Brand could not be found.");
		}
		[Fact]
		public void Update_ConflictedBrand_ByID_ThrowsException() {
			int id_brand = 1;
			BrandDTO brand_dto = new BrandDTO { Name = "Banat" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Update(id_brand.ToString(), brand_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Brand already exists.");
		}
		[Fact]
		public void Update_ConflictedBrand_ByName_ThrowsException() {
			string name_brand = "Alfa";
			BrandDTO brand_dto = new BrandDTO { Name = "Banat" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Update(name_brand, brand_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Brand already exists.");
		}
		[Fact]
		public void Update_Brand_ByID_ShouldBeReturn() {
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			int id_brand = 1;
			BrandDTO brand_dto = new BrandDTO { Name = "Dove" };
			FluentActions.Invoking(() => brand_repository.Update(id_brand.ToString(), brand_dto)).Invoke();
			Brand? brand = context.Brands.SingleOrDefault(b => b.Name == brand_dto.Name);
			brand.Should().NotBeNull();
		}
		[Fact]
		public void Update_Brand_ByName_ShouldBeReturn() {
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			string name_brand = "Alfa";
			BrandDTO brand_dto = new BrandDTO { Name = "Dove" };
			FluentActions.Invoking(() => brand_repository.Update(name_brand, brand_dto)).Invoke();
			Brand? brand = context.Brands.SingleOrDefault(b => b.Name == brand_dto.Name);
			brand.Should().NotBeNull();
		}
	}
}