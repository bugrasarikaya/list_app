using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using list_api.Services;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ClientRepositoryTests {
	public class UpdateListTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public UpdateListTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Update_UnknownIDList_ThrowsException() {
			int id_list = 10;
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 1, Name = "FavList", Description = "My favourite list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			FluentActions.Invoking(() => client_repository.UpdateList(id_list.ToString(), client_list_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Update_UnknownNameList_ThrowsException() {
			string name_list = "Fake List";
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 1, Name = "FavList", Description = "My favourite list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			FluentActions.Invoking(() => client_repository.UpdateList(name_list, client_list_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Update_ConflictedList_ThrowsException() {
			int id_list = 8;
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 4, Name = "List", Description = "This my favourite food list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			FluentActions.Invoking(() => client_repository.UpdateList(id_list.ToString(), client_list_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("List already exists.");
		}
		[Fact]
		public void Patch_ForbiddenStatus_ThrowsException() {
			int id_list = 1;
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => client_repository.UpdateList(id_list.ToString(), client_list_dto)).Should().Throw<ForbiddenException>().And.Message.Should().Be("Completed list cannot be changed.");
		}
		[Fact]
		public void Update_List_ByID_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			int id_list = 8;
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => client_repository.UpdateList(id_list.ToString(), client_list_dto)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDCategory == client_list_dto.IDCategory && l.IDUser == client_repository.IDUser && l.Name == client_list_dto.Name && l.Description == client_list_dto.Description);
			list.Should().NotBeNull();
		}
		[Fact]
		public void Update_List_ByName_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			string name_list = "Gift List";
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => client_repository.UpdateList(name_list.ToString(), client_list_dto)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDCategory == client_list_dto.IDCategory && l.IDUser == client_repository.IDUser && l.Name == client_list_dto.Name && l.Description == client_list_dto.Description);
			list.Should().NotBeNull();
		}
	}
}