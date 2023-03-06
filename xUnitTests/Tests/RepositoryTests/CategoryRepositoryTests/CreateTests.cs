using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.CategoryRepositoryTests {
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
		public void Create_SameCategory_ThrowsException() {
			CategoryDTO category_dto = new CategoryDTO { Name = "Book" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Create(category_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Category already exists.");
		}
		[Fact]
		public void Create_Category_ShouldBeReturnSame() {
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			CategoryDTO category_dto = new CategoryDTO { Name = "Electronics" };
			FluentActions.Invoking(() => category_repository.Create(category_dto)).Invoke();
			Category? category = context.Categories.SingleOrDefault(c => c.Name == category_dto.Name);
			category.Should().NotBeNull();
			category?.Name.Should().Be(category_dto.Name);
		}
	}
}