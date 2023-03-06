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
	public class PatchTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public PatchTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Patch_UnknownList_ThrowsException() {
			int id_list = 10;
			ListPatchDTO list_patch_dto = new ListPatchDTO { IDCategory = 1, IDUser = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.Patch(id_list, list_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Patch_ConflictedList_ThrowsException() {
			int id_list = 1;
			ListPatchDTO list_patch_dto = new ListPatchDTO { IDCategory = 4, IDStatus = 1, IDUser = 1, Name = "List", Description = "This my favourite food list." };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.Patch(id_list, list_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("List already exists.");
		}
		[Fact]
		public void Patch_List_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_list = 1;
			ListPatchDTO list_patch_dto = new ListPatchDTO { IDCategory = 1, IDUser = 1, IDStatus = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => list_repository.Patch(id_list, list_patch_dto)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDCategory == list_patch_dto.IDCategory && l.IDUser == list_patch_dto.IDUser && l.IDStatus == list_patch_dto.IDStatus && l.Name == list_patch_dto.Name && l.Description == list_patch_dto.Description);
			list.Should().NotBeNull();
		}
	}
}