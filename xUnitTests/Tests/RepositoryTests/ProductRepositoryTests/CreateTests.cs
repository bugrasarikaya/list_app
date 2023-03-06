using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ProductRepositoryTests {
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
		public void Create_SameProduct_ThrowsException() {
			ProductDTO product_dto = new ProductDTO { IDBrand = 1, IDCategory = 1, Name = "Ceviz Kabuğundaki Evren", Description = "Science book", Price = 33.50 };
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			FluentActions.Invoking(() => product_repository.Create(product_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Product already exists.");
		}
		[Fact]
		public void Create_Product_ShouldBeReturnSame() {
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			ProductDTO product_dto = new ProductDTO { IDBrand = 4, IDCategory = 3, Name = "Jacket", Description = "Leather.", Price = 234.90 };
			FluentActions.Invoking(() => product_repository.Create(product_dto)).Invoke();
			Product? product = context.Products.SingleOrDefault(p => p.IDBrand == product_dto.IDBrand && p.IDCategory == product_dto.IDCategory && p.Name == product_dto.Name && p.Description == product_dto.Description && p.Price == product_dto.Price);
			product.Should().NotBeNull();
			product?.Name.Should().Be(product_dto.Name);
		}
	}
}