using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.BrandRepositoryTests {
	public class CreateTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public CreateTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Create_SameBrand_ThrowsException() {
			BrandDTO brand_dto = new BrandDTO { Name = "Alfa" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Create(brand_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Brand already exists.");
		}
		[Fact]
		public void Create_Brand_ShouldBeReturnSame() {
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			BrandDTO brand_dto = new BrandDTO { Name = "Dove" };
			FluentActions.Invoking(() => brand_repository.Create(brand_dto)).Invoke();
			Brand? brand = context.Brands.SingleOrDefault(b => b.Name == brand_dto.Name);
			brand.Should().NotBeNull();
			brand?.Name.Should().Be(brand_dto.Name);
		}
	}
}