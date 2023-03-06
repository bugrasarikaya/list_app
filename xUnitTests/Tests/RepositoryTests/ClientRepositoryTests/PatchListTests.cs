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
	public class PatchListTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public PatchListTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Patch_UnknownIDUser_ThrowsException() {
			int id_list = 1;
			ClientListPatchDTO client_list_patch_dto = new ClientListPatchDTO { IDCategory = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 10;
			FluentActions.Invoking(() => client_repository.PatchList(id_list.ToString(), client_list_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Patch_ForbiddenStatus_ThrowsException() {
			int id_list = 1;
			ClientListPatchDTO client_list_patch_dto = new ClientListPatchDTO { IDCategory = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.PatchList(id_list.ToString(), client_list_patch_dto)).Should().Throw<ForbiddenException>().And.Message.Should().Be("Completed list cannot be changed.");
		}
		[Fact]
		public void Patch_UnknownIDList_ThrowsException() {
			int id_list = 10;
			ClientListPatchDTO client_list_patch_dto = new ClientListPatchDTO { IDCategory = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			FluentActions.Invoking(() => client_repository.PatchList(id_list.ToString(), client_list_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Patch_UnknownNameList_ThrowsException() {
			string name_list = "Fake List";
			ClientListPatchDTO client_list_patch_dto = new ClientListPatchDTO { IDCategory = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			FluentActions.Invoking(() => client_repository.PatchList(name_list, client_list_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Patch_ConflictedList_ThrowsException() {
			int id_list = 8;
			ClientListPatchDTO client_list_patch_dto = new ClientListPatchDTO { IDCategory = 4, IDStatus = 1, Name = "List", Description = "This my favourite food list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 8;
			FluentActions.Invoking(() => client_repository.PatchList(id_list.ToString(), client_list_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("List already exists.");
		}
		[Fact]
		public void Patch_List_ByID_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			int id_list = 8;
			ClientListPatchDTO client_list_patch_dto = new ClientListPatchDTO { IDCategory = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => client_repository.PatchList(id_list.ToString(), client_list_patch_dto)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDCategory == client_list_patch_dto.IDCategory && l.IDUser == client_repository.IDUser && l.IDStatus == client_list_patch_dto.IDStatus && l.Name == client_list_patch_dto.Name && l.Description == client_list_patch_dto.Description);
			list.Should().NotBeNull();
		}
		[Fact]
		public void Patch_List_ByName_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 6;
			string name_list = "Gift List";
			ClientListPatchDTO client_list_patch_dto = new ClientListPatchDTO { IDCategory = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => client_repository.PatchList(name_list.ToString(), client_list_patch_dto)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDCategory == client_list_patch_dto.IDCategory && l.IDUser == client_repository.IDUser && l.IDStatus == client_list_patch_dto.IDStatus && l.Name == client_list_patch_dto.Name && l.Description == client_list_patch_dto.Description);
			list.Should().NotBeNull();
		}
	}
}