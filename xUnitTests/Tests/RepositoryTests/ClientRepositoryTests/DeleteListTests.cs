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
	public class DeleteListTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public DeleteListTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Delete_UnknownIDUser_ThrowsException() {
			int id_list = 1;
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 10;
			FluentActions.Invoking(() => client_repository.DeleteList(id_list.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Delete_UnknownIDList_ThrowsException() {
			int id_list = 10;
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.DeleteList(id_list.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Delete_UnknownNameList_ThrowsException() {
			string name_list = "Fake List";
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.DeleteList(name_list)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Patch_ForbiddenStatus_ThrowsException() {
			int id_list = 1;
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.DeleteList(id_list.ToString())).Should().Throw<ForbiddenException>().And.Message.Should().Be("Completed list cannot be changed.");
		}
		[Fact]
		public void Delete_List_ByID_ShouldBeReturnNull() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			int id_list = 8;
			FluentActions.Invoking(() => client_repository.DeleteList(id_list.ToString())).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.ID == id_list && l.IDUser == client_repository.IDUser);
			list.Should().BeNull();
		}
		[Fact]
		public void Delete_List_ByName_ShouldBeReturnNull() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			string name_list = "Gift List";
			FluentActions.Invoking(() => client_repository.DeleteList(name_list)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.Name == name_list && l.IDUser == client_repository.IDUser);
			list.Should().BeNull();
		}
	}
}