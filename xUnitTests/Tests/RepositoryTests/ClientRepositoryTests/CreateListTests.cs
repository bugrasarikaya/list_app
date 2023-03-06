using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository;
using list_api.Services;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ClientRepositoryTests {
	public class CreateListTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public CreateListTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Create_List_UnkownIDUser_ThrowsException() {
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 1, Name = "FavList", Description = "My favourite list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 10;
			FluentActions.Invoking(() => client_repository.CreateList(client_list_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("User not found.");
		}
		[Fact]
		public void Create_SameList_ThrowsException() {
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 4, Name = "List", Description = "This my favourite food list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.CreateList(client_list_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("List already exists.");
		}
		[Fact]
		public void Create_List_ShouldBeReturnSame() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser =  1;
			ClientListDTO client_list_dto = new ClientListDTO { IDCategory = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => client_repository.CreateList(client_list_dto)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDCategory == client_list_dto.IDCategory && l.IDUser == client_repository.IDUser && l.Name == client_list_dto.Name && l.Description == client_list_dto.Description);
			list.Should().NotBeNull();
			list?.Name.Should().Be(client_list_dto.Name);
		}
	}
}