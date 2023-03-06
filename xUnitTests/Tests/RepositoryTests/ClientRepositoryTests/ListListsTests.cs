using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Services;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ClientRepositoryTests {
	public class ListListsTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public ListListsTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void List_List_ByUnknownIDUser_ThrowsException() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 10;
			FluentActions.Invoking(() => client_repository.ListLists()).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownIDCategory_ThrowsException() {
			int id_category = 10;
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.ListListsByCategory(id_category.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategory_ThrowsException() {
			string name_category = "Medical Health";
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.ListListsByCategory(name_category)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategory_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			int id_category = 1;
			FluentActions.Invoking(() => client_repository.ListListsByCategory(id_category.ToString())).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == client_repository.IDUser).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategory_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			string name_category = "Book";
			FluentActions.Invoking(() => client_repository.ListListsByCategory(name_category)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && l.IDUser == client_repository.IDUser).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryAndValidDateTimeCompleting_ThrowsException() {
			int id_category = 10;
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeCompleting(id_category.ToString(), date_time_completing)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryAndValidDateTimeCompleting_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeCompleting(name_category, date_time_completing)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryAndValidDateTimeCompleting_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			int id_category = 1;
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeCompleting(id_category.ToString(), date_time_completing)).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == client_repository.IDUser).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryAndValidDateTimeCompleting_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			string name_category = "Book";
			DateTime date_time_completing = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeCompleting(name_category, date_time_completing)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && l.IDUser == client_repository.IDUser).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryAndValidDateTimeCreating_ThrowsException() {
			int id_category = 10;
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeCreating(id_category.ToString(), date_time_creating)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryAndValidDateTimeCreating_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeCreating(name_category, date_time_creating)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryAndValidDateTimeCreating_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			int id_category = 1;
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeCreating(id_category.ToString(), date_time_creating)).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == client_repository.IDUser).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryAndValidDateTimeCreating_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			string name_category = "Book";
			DateTime date_time_creating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeCreating(name_category, date_time_creating)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && l.IDUser == client_repository.IDUser).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByUnknownIDCategoryAndValidDateTimeUpdating_ThrowsException() {
			int id_category = 10;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeUpdating(id_category.ToString(), date_time_updating)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByUnknownNameCategoryAndValidDateTimeUpdating_ThrowsException() {
			string name_category = "Medical Health";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeUpdating(name_category, date_time_updating)).Should().Throw<NotFoundException>().And.Message.Should().Be("Category could not be found.");
		}
		[Fact]
		public void List_List_ByIDCategoryAndValidDateTimeUpdating_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			int id_category = 1;
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeUpdating(id_category.ToString(), date_time_updating)).Invoke();
			List<List> list_list = context.Lists.Where(l => l.IDCategory == id_category && l.IDUser == client_repository.IDUser).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
		[Fact]
		public void List_List_ByNameCategoryAndValidDateTimeUpdating_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			string name_category = "Book";
			DateTime date_time_updating = DateTime.Now.AddYears(-1);
			FluentActions.Invoking(() => client_repository.ListByCategoryAndDateTimeUpdating(name_category, date_time_updating)).Invoke();
			List<List> list_list = context.Lists.Where(l => context.Categories.Any(c => c.ID == l.IDCategory && c.Name == name_category) && l.IDUser == client_repository.IDUser).ToList();
			list_list.Should().HaveCountGreaterThan(0);
		}
	}
}