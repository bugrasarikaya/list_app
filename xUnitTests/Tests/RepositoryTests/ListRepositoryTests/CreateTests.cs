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
namespace xUnitTests.Tests.RepositoryTests.ListRepositoryTests {
	public class CreateTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public CreateTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Create_SameList_ThrowsException() {
			ListDTO list_dto = new ListDTO { IDCategory = 4, IDUser = 1, Name = "List", Description = "This my favourite food list." };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.Create(list_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("List already exists.");
		}
		[Fact]
		public void Create_List_ShouldBeReturnSame() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			ListDTO list_dto = new ListDTO { IDCategory = 1, IDUser = 1, Name = "FavList", Description = "My favourite list." };
			FluentActions.Invoking(() => list_repository.Create(list_dto)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.IDCategory == list_dto.IDCategory && l.IDUser == list_dto.IDUser && l.Name == list_dto.Name && l.Description == list_dto.Description);
			list.Should().NotBeNull();
			list?.Name.Should().Be(list_dto.Name);
		}
	}
}