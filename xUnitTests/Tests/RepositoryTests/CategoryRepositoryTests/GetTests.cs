using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.CategoryRepositoryTests {
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
		public void Get_UnknownCategory_ByID_ThrowsException() {
			int id_category = 10;
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Get(id_category.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void Get_UnknownCategory_ByName_ThrowsException() {
			string name_category = "Medical Health";
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Get(name_category)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void Get_Category_ByID_ShouldBeReturn() {
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			int id_category = 1;
			FluentActions.Invoking(() => category_repository.Get(id_category.ToString())).Invoke();
			Category? category = context.Categories.SingleOrDefault(c => c.ID == id_category);
			category.Should().NotBeNull();
		}
		[Fact]
		public void Get_Category_ByName_ShouldBeReturn() {
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			string name_category = "Book";
			FluentActions.Invoking(() => category_repository.Get(name_category)).Invoke();
			Category? category = context.Categories.SingleOrDefault(c => c.Name == name_category);
			category.Should().NotBeNull();
		}
	}
}