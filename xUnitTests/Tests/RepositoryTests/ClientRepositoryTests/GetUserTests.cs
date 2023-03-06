using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Security;
using list_api.Services;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ClientRepositoryTests {
	public class GetTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		public readonly IEncryptor encryptor;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public GetTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			encryptor = test_fixture.Encryptor;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Get_UnknownUser_ThrowsException() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 10;
			FluentActions.Invoking(() => client_repository.GetUser()).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void Get_User_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.GetUser()).Invoke();
			User? user = context.Users.SingleOrDefault(u => u.ID == client_repository.IDUser);
			user.Should().NotBeNull();
		}
	}
}