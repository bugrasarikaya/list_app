using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository;
using list_api.Security;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.UserRepositoryTests {
	public class CreateTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		public readonly IEncryptor encryptor;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public CreateTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			encryptor = test_fixture.Encryptor;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Create_SameUser_ThrowsException() {
			UserDTO user_dto = new UserDTO { IDRole = 2, Name = "ashrivers", Password = "ashrivers" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Create(user_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("User already exists.");
		}
		[Fact]
		public void Create_User_ShouldBeReturnSame() {
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			UserDTO user_dto = new UserDTO { IDRole = 1, Name = "ninesrodriguez", Password = "password" };
			FluentActions.Invoking(() => user_repository.Create(user_dto)).Invoke();
			User? user = context.Users.SingleOrDefault(c => c.Name == user_dto.Name);
			user.Should().NotBeNull();
			user?.IDRole.Should().Be(user_dto.IDRole);
			user?.Name.Should().Be(user_dto.Name);
			user?.Password.Should().Be("6ae937bf2e4e0da8dce4b161931d6e5131fce59a175cb82acc49011a4483444f");
		}
	}
}