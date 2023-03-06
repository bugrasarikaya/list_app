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
	public class DeleteTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public DeleteTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Delete_UnknownList_ThrowsException() {
			int id_list = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.Delete(id_list)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void Delete_List_ShouldBeReturnNull() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_list = 8;
			FluentActions.Invoking(() => list_repository.Delete(id_list)).Invoke();
			List? list = context.Lists.SingleOrDefault(l => l.ID == id_list);
			list.Should().BeNull();
		}
	}
}