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
	public class GetListTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public GetListTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Get_UnknownIDUser_ThrowsException() {
			int id_list = 10;
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 10;
			FluentActions.Invoking(() => client_repository.GetList(id_list.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Get_UnknownIDList_ThrowsException() {
			int id_list = 10;
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.GetList(id_list.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Get_UnknownNameList_ThrowsException() {
			string name_list = "Fake List";
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.GetList(name_list)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Get_List_ByID_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			int id_list = 1;
			FluentActions.Invoking(() => client_repository.GetList(id_list.ToString())).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.ID == id_list);
			list.Should().NotBeNull();
		}
		[Fact]
		public void Get_List_ByName_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			string name_list = "List";
			FluentActions.Invoking(() => client_repository.GetList(name_list)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDUser == client_repository.IDUser && l.Name == name_list);
			list.Should().NotBeNull();
		}
	}
}