using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Services;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ListRepositoryTests {
	public class ListTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public ListTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void List_List_ByUnknownIDCategory_ThrowsException() {
			int id_category = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategory(id_category.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategory_ThrowsException() {
			string name_category = "Medical Health";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategory(name_category)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategory_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_category = 1;
			FluentActions.Invoking(() => list_repository.ListByCategory(id_category.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategory_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_category = "Book";
			FluentActions.Invoking(() => list_repository.ListByCategory(name_category)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDUser_ThrowsException() {
			int id_user = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByUser(id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameUser_ThrowsException() {
			string name_user = "elifparmak";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByUser(name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByIDUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_user = 1;
			FluentActions.Invoking(() => list_repository.ListByUser(id_user.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDUser == id_user).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_user = "ashrivers";
			FluentActions.Invoking(() => list_repository.ListByUser(name_user)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Users.Any(u => u.ID == l.IDUser && u.Name == name_user)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryAndValidDateTimeCompleting_ThrowsException() {
			int id_category = 10;
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompleting(id_category.ToString(), date_time_completing)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryAndValidDateTimeCompleting_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompleting(name_category, date_time_completing)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryAndValidDateTimeCompleting_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_category = 1;
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompleting(id_category.ToString(), date_time_completing)).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryAndValidDateTimeCompleting_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_category = "Book";
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompleting(name_category, date_time_completing)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryAndValidDateTimeCreating_ThrowsException() {
			int id_category = 10;
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreating(id_category.ToString(), date_time_creating)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryAndValidDateTimeCreating_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreating(name_category, date_time_creating)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryAndValidDateTimeCreating_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_category = 1;
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreating(id_category.ToString(), date_time_creating)).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryAndValidDateTimeCreating_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_category = "Book";
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreating(name_category, date_time_creating)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryAndValidDateTimeUpdating_ThrowsException() {
			int id_category = 10;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdating(id_category.ToString(), date_time_updating)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryAndValidDateTimeUpdating_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdating(name_category, date_time_updating)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryAndValidDateTimeUpdating_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_category = 1;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdating(id_category.ToString(), date_time_updating)).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryAndValidDateTimeUpdating_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_category = "Book";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdating(name_category, date_time_updating)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryAndKnownIDUser_ThrowsException() {
			int id_category = 10;
			int id_user = 1;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndUser(id_category.ToString(), id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryAndKnownNameUser_ThrowsException() {
			string name_category = "Medical Health";
			string name_user = "ashrivers";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndUser(name_category, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByKnownIDCategoryAndUnknownIDUser_ThrowsException() {
			int id_category = 1;
			int id_user = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndUser(id_category.ToString(), id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByKnownNameCategoryAndUnknownNameUser_ThrowsException() {
			string name_category = "Medical Health";
			string name_user = "elifparmak";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndUser(name_category, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryAndIDCategory_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_category = 1;
			int id_user = 1;
			FluentActions.Invoking(() => list_repository.ListByCategoryAndUser(id_category.ToString(), id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == id_user).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryAndNameCategory_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_category = "Book";
			string name_user = "ashrivers";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndUser(name_category, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && context.Users.Any(u => u.ID == l.IDUser && u.Name == name_user)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByValidDateTimeCompletingAndUnknownIDUser_ThrowsException() {
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			int id_user = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByDateTimeCompletingAndUser(date_time_completing, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByValidDateTimeCompletingAndUnknownNameUser_ThrowsException() {
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			string name_user = "elifparmak";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByDateTimeCompletingAndUser(date_time_completing, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByValidDateTimeCompletingAndIDUser_ShouldBeReturn() {
			int id_user = 1;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => list_repository.ListByDateTimeCompletingAndUser(date_time_completing, id_user.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDUser == id_user).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByValidDateTimeCompletingAndyNameUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			FluentActions.Invoking(() => list_repository.ListByDateTimeCompletingAndUser(date_time_completing, name_user)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Users.Any(c => c.ID == l.IDUser && c.Name == name_user)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByValidDateTimeCreatingAndUnknownIDUser_ThrowsException() {
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			int id_user = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByDateTimeCreatingAndUser(date_time_creating, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByValidDateTimeCreatingAndUnknownNameUser_ThrowsException() {
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			string name_user = "elifparmak";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByDateTimeCreatingAndUser(date_time_creating, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByValidDateTimeCreatingAndIDUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			int id_user = 1;
			FluentActions.Invoking(() => list_repository.ListByDateTimeCreatingAndUser(date_time_creating, id_user.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDUser == id_user).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByValidDateTimeCreatingAndNameUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			FluentActions.Invoking(() => list_repository.ListByDateTimeCreatingAndUser(date_time_creating, name_user)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Users.Any(c => c.ID == l.IDUser && c.Name == name_user)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByValidDateTimeUpdatingAndUnknownIDUser_ThrowsException() {
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByDateTimeUpdatingAndUser(date_time_updating, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ValidDateTimeUpdatingAndUnknownNameUser_ThrowsException() {
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "elifparmak";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByDateTimeUpdatingAndUser(date_time_updating, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByValidDateTimeUpdatingAndIDUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 1;
			FluentActions.Invoking(() => list_repository.ListByDateTimeUpdatingAndUser(date_time_updating, id_user.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDUser == id_user).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByValidDateTimeUpdatingAndNameUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			FluentActions.Invoking(() => list_repository.ListByDateTimeUpdatingAndUser(date_time_updating, name_user)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Users.Any(u => u.ID == l.IDUser && u.Name == name_user)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryValidDateTimeCompletingAndKnownIDUser_ThrowsException() {
			int id_category = 10;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 1;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompletingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByKnownIDCategoryValidDateTimeCompletingAndUnknownIDUser_ThrowsException() {
			int id_category = 1;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompletingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryValidDateTimeCompletingAndKnownNameUser_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompletingAndUser(name_category, date_time_updating, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByKnowDValidDateTimeCompletingAndUnknownNameUser_ThrowsException() {
			string name_category = "Book";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "elifparmak";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompletingAndUser(name_category, date_time_updating, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryValidDateTimeCompletingAndIDUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_category = 1;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 1;
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompletingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == id_user).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryValidDateTimeCompletingAndNameUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_category = "Book";
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCompletingAndUser(name_category, date_time_completing, name_user)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && context.Users.Any(u => u.ID == l.IDUser && u.Name == name_user)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryValidDateTimeCreatingAndKnownIDUser_ThrowsException() {
			int id_category = 10;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 1;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreatingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByKnownIDCategoryValidDateTimeCreatingAndUnknownIDUser_ThrowsException() {
			int id_category = 1;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreatingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryValidDateTimeCreatingAndKnownNameUser_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreatingAndUser(name_category, date_time_updating, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByKnowDValidDateTimeCreatingAndUnknownNameUser_ThrowsException() {
			string name_category = "Book";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "elifparmak";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreatingAndUser(name_category, date_time_updating, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryValidDateTimeCreatingAndIDUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_category = 1;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 1;
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreatingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == id_user).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryValidDateTimeCreatingAndNameUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_category = "Book";
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeCreatingAndUser(name_category, date_time_creating, name_user)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && context.Users.Any(u => u.ID == l.IDUser && u.Name == name_user)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryValidDateTimeUpdatingAndKnownIDUser_ThrowsException() {
			int id_category = 10;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 1;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdatingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByKnownIDCategoryValidDateTimeUpdatingAndUnknownIDUser_ThrowsException() {
			int id_category = 1;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdatingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryValidDateTimeUpdatingAndKnownNameUser_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdatingAndUser(name_category, date_time_updating, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByKnowDValidDateTimeUpdatingAndUnknownNameUser_ThrowsException() {
			string name_category = "Book";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "elifparmak";
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdatingAndUser(name_category, date_time_updating, name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryValidDateTimeUpdatingAndIDUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_category = 1;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			int id_user = 1;
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdatingAndUser(id_category.ToString(), date_time_updating, id_user.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == id_user).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryValidDateTimeUpdatingAndNameUser_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			string name_category = "Book";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			string name_user = "ashrivers";
			FluentActions.Invoking(() => list_repository.ListByCategoryAndDateTimeUpdatingAndUser(name_category, date_time_updating, name_user)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && context.Users.Any(u => u.ID == l.IDUser && u.Name == name_user)).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
	}
}