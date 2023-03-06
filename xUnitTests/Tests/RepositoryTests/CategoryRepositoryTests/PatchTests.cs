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
		public void Patch_UnknownCategory_ByID_ThrowsException() {
			int id_category = 10;
			CategoryPatchDTO category_patch_dto = new CategoryPatchDTO { Name = "Electronics" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Patch(id_category.ToString(), category_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void Patch_UnknownCategory_ByName_ThrowsException() {
			string name_category = "Medical Health";
			CategoryPatchDTO category_patch_dto = new CategoryPatchDTO { Name = "Electronics" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Patch(name_category, category_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void Patch_ConflictedCategory_ByID_ThrowsException() {
			int id_category = 1;
			CategoryPatchDTO category_patch_dto = new CategoryPatchDTO { Name = "Cleaning" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Patch(id_category.ToString(), category_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Category already exists.");
		}
		[Fact]
		public void Patch_ConflictedCategory_ByName_ThrowsException() {
			string name_category = "Book";
			CategoryPatchDTO category_patch_dto = new CategoryPatchDTO { Name = "Cleaning" };
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			FluentActions.Invoking(() => category_repository.Patch(name_category, category_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Category already exists.");
		}
		[Fact]
		public void Patch_Category_ByID_ShouldBeReturn() {
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			int id_category = 1;
			CategoryPatchDTO category_patch_dto = new CategoryPatchDTO { Name = "Electronics" };
			FluentActions.Invoking(() => category_repository.Patch(id_category.ToString(), category_patch_dto)).Invoke();
			Category? category = context.Categories.SingleOrDefault(c => c.Name == category_patch_dto.Name);
			category.Should().NotBeNull();
		}
		[Fact]
		public void Patch_Category_ByName_ShouldBeReturn() {
			CategoryRepository category_repository = new CategoryRepository(cache, context, mapper);
			string name_category = "Book";
			CategoryPatchDTO category_patch_dto = new CategoryPatchDTO { Name = "Electronics" };
			FluentActions.Invoking(() => category_repository.Patch(name_category, category_patch_dto)).Invoke();
			Category? category = context.Categories.SingleOrDefault(c => c.Name == category_patch_dto.Name);
			category.Should().NotBeNull();
		}
	}
}