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
	public class PatchTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public PatchTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Patch_UnknownBrand_ByID_ThrowsException() {
			int id_brand = 10;
			BrandPatchDTO brand_patch_dto = new BrandPatchDTO { Name = "Doritos" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Patch(id_brand.ToString(), brand_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Brand could not be found.");
		}
		[Fact]
		public void Patch_UnknownBrand_ByName_ThrowsException() {
			string name_brand = "Dove";
			BrandPatchDTO brand_patch_dto = new BrandPatchDTO { Name = "Doritos" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Patch(name_brand, brand_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Brand could not be found.");
		}
		[Fact]
		public void Patch_ConflictedBrand_ByID_ThrowsException() {
			int id_brand = 1;
			BrandPatchDTO brand_patch_dto = new BrandPatchDTO { Name = "Banat" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Patch(id_brand.ToString(), brand_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Brand already exists.");
		}
		[Fact]
		public void Patch_ConflictedBrand_ByName_ThrowsException() {
			string name_brand = "Alfa";
			BrandPatchDTO brand_patch_dto = new BrandPatchDTO { Name = "Banat" };
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			FluentActions.Invoking(() => brand_repository.Patch(name_brand, brand_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Brand already exists.");
		}
		[Fact]
		public void Patch_Brand_ByID_ShouldBeReturn() {
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			int id_brand = 1;
			BrandPatchDTO brand_patch_dto = new BrandPatchDTO { Name = "Dove" };
			FluentActions.Invoking(() => brand_repository.Patch(id_brand.ToString(), brand_patch_dto)).Invoke();
			Brand? brand = context.Brands.SingleOrDefault(b => b.Name == brand_patch_dto.Name);
			brand.Should().NotBeNull();
		}
		[Fact]
		public void Patch_Brand_ByName_ShouldBeReturn() {
			BrandRepository brand_repository = new BrandRepository(cache, context, mapper);
			string name_brand = "Alfa";
			BrandPatchDTO brand_patch_dto = new BrandPatchDTO { Name = "Dove" };
			FluentActions.Invoking(() => brand_repository.Patch(name_brand, brand_patch_dto)).Invoke();
			Brand? brand = context.Brands.SingleOrDefault(b => b.Name == brand_patch_dto.Name);
			brand.Should().NotBeNull();
		}
	}
}