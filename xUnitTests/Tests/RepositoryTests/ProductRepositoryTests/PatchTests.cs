using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ProductRepositoryTests {
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
		public void Patch_UnknownProduct_ThrowsException() {
			int id_product = 10;
			ProductPatchDTO product_patch_dto = new ProductPatchDTO { IDBrand = 4, IDCategory = 3, Name = "Jacket", Description = "Leather.", Price = 234.90 };
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			FluentActions.Invoking(() => product_repository.Patch(id_product, product_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Product could not be found.");
		}
		[Fact]
		public void Patch_ConflictedProduct_ThrowsException() {
			int id_product = 1;
			ProductPatchDTO product_patch_dto = new ProductPatchDTO { IDBrand = 1, IDCategory = 1, Name = "Ceviz Kabuğundaki Evren", Description = "Science book", Price = 33.50 };
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			FluentActions.Invoking(() => product_repository.Patch(id_product, product_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Product already exists.");
		}
		[Fact]
		public void Patch_Product_ShouldBeReturn() {
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			int id_product = 1;
			ProductPatchDTO product_patch_dto = new ProductPatchDTO { IDBrand = 4, IDCategory = 3, Name = "Jacket", Description = "Leather.", Price = 234.90 };
			FluentActions.Invoking(() => product_repository.Patch(id_product, product_patch_dto)).Invoke();
			Product? product = context.Products.SingleOrDefault(p => p.IDBrand == product_patch_dto.IDBrand && p.IDCategory == product_patch_dto.IDCategory && p.Name == product_patch_dto.Name && p.Description == product_patch_dto.Description && p.Price == product_patch_dto.Price);
			product.Should().NotBeNull();
		}
	}
}