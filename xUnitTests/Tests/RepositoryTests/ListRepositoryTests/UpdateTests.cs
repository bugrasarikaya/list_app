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
namespace xUnitTests.Tests.RepositoryTests.ListRepositoryTests {
	public class UpdateTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public UpdateTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Update_UnknownList_ThrowsException() {
			int id_list = 10;
			ListDTO list_dto = new ListDTO { IDCategory = 1, IDUser = 1, Name = "FavList", Description = "My favourite list." };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.Update(id_list, list_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Patch_ForbiddenStatus_ThrowsException() {
			int id_list = 1;
			ClientListPatchDTO client_list_patch_dto = new ClientListPatchDTO { IDCategory = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => client_repository.PatchList(id_list.ToString(), client_list_patch_dto)).Should().Throw<ForbiddenException>().And.Message.Should().Be("Completed list cannot be changed.");
		}
		[Fact]
		public void Update_ConflictedList_ThrowsException() {
			int id_list = 1;
			ListDTO list_dto = new ListDTO { IDCategory = 4, IDUser = 1, Name = "List", Description = "This my favourite food list." };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.Update(id_list, list_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("List already exists.");
		}
		[Fact]
		public void Update_List_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_list = 1;
			ListDTO list_dto = new ListDTO { IDCategory = 1, IDUser = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => list_repository.Update(id_list, list_dto)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDCategory == list_dto.IDCategory && l.IDUser == list_dto.IDUser && l.Name == list_dto.Name && l.Description == list_dto.Description);
			list.Should().NotBeNull();
		}
	}
}