using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.CategoryRepositoryTests {
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
		public void Update_UnknownCategory_ByID_ThrowsException() {
			int id_category = 10;
			CategoryDTO category_dto = new CategoryDTO { Name = "Electronics" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Update(id_category.ToString(), category_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void Update_UnknownCategory_ByName_ThrowsException() {
			string name_category = "Medical Health";
			CategoryDTO category_dto = new CategoryDTO { Name = "Electronics" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Update(name_category, category_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void Update_ConflictedCategory_ByID_ThrowsException() {
			int id_category = 1;
			CategoryDTO category_dto = new CategoryDTO { Name = "Cleaning" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Update(id_category.ToString(), category_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Category already exists.");
		}
		[Fact]
		public void Update_ConflictedCategory_ByName_ThrowsException() {
			string name_category = "Book";
			CategoryDTO category_dto = new CategoryDTO { Name = "Cleaning" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Update(name_category, category_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Category already exists.");
		}
		[Fact]
		public void Update_Category_ByID_ShouldBeReturn() {
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			int id_category = 1;
			CategoryDTO category_dto = new CategoryDTO { Name = "Electronics" };
			FluentActions.Invoking(() => category_repository.Update(id_category.ToString(), category_dto)).Invoke();
			Category? category = context.Categories.SingleOrDefault(c => c.Name == category_dto.Name);
			category.Should().NotBeNull();
		}
		[Fact]
		public void Update_Category_ByName_ShouldBeReturn() {
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			string name_category = "Book";
			CategoryDTO category_dto = new CategoryDTO { Name = "Electronics" };
			FluentActions.Invoking(() => category_repository.Update(name_category, category_dto)).Invoke();
			Category? category = context.Categories.SingleOrDefault(c => c.Name == category_dto.Name);
			category.Should().NotBeNull();
		}
	}
}