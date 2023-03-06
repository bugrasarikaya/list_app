using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ProductRepositoryTests {
	public class DeleteTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public DeleteTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Delete_UnknownProduct_ByID_ThrowsException() {
			int id_product = 10;
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			FluentActions.Invoking(() => product_repository.Delete(id_product)).Should().Throw<NotFoundException>().And.Message.Should().Be("Product could not be found.");
		}
		[Fact]
		public void Delete__ConflictedProduct_ShouldBeReturnNull() {
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			int id_product = 1;
			FluentActions.Invoking(() => product_repository.Delete(id_product)).Should().Throw<ConflictException>().And.Message.Should().Be("Product exists in a list.");
		}
		[Fact]
		public void Delete_Product_ShouldBeReturnNull() {
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			int id_product = 8;
			FluentActions.Invoking(() => product_repository.Delete(id_product)).Invoke();
			Product? product = context.Products.SingleOrDefault(p => p.ID == id_product);
			product.Should().BeNull();
		}
	}
}