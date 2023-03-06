using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Security;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.UserRepositoryTests {
	public class GetTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		public readonly IEncryptor encryptor;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public GetTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			encryptor = test_fixture.Encryptor;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Get_UnknownUser_ByID_ThrowsException() {
			int id_user = 10;
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Get(id_user.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void Get_UnknownUser_ByName_ThrowsException() {
			string name_user = "elifparmak";
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Get(name_user)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void Get_User_ByID_ShouldBeReturn() {
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			int id_user = 1;
			FluentActions.Invoking(() => user_repository.Get(id_user.ToString())).Invoke();
			User? user = context.Users.SingleOrDefault(u => u.ID == id_user);
			user.Should().NotBeNull();
		}
		[Fact]
		public void Get_User_ByName_ShouldBeReturn() {
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			string name_user = "ashrivers";
			FluentActions.Invoking(() => user_repository.Get(name_user)).Invoke();
			User? user = context.Users.SingleOrDefault(u => u.Name == name_user);
			user.Should().NotBeNull();
		}
	}
}