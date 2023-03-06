using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ProductRepositoryTests {
	public class ListTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public ListTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void List_Product_ByUnknownIDCategory_ThrowsException() {
			int id_category = 10;
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			FluentActions.Invoking(() => product_repository.ListByCategory(id_category.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_Product_ByUnknownNameCategory_ThrowsException() {
			string name_category = "Medical Health";
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			FluentActions.Invoking(() => product_repository.ListByCategory(name_category)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_Product_ByIDCategory_ShouldBeReturn() {
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			int id_category = 1;
			FluentActions.Invoking(() => product_repository.ListByCategory(id_category.ToString())).Invoke();
			List<Product> list_product = context.Products.Where(p => p.IDCategory == id_category).ToList();
			list_product.Should().NotBeNull();
		}
		[Fact]
		public void List_Product_ByNameCategory_ShouldBeReturn() {
			ProductRepository product_repository = new ProductRepository(cache, context, mapper);
			string name_category = "Book";
			FluentActions.Invoking(() => product_repository.ListByCategory(name_category)).Invoke();
			List<Product> list_product = context.Products.Where(p => context.Categories.Any(c => c.ID == p.IDCategory && c.Name == name_category)).ToList();
			list_product.Should().NotBeNull();
		}
	}
}