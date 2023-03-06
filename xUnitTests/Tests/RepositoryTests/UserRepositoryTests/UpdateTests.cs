using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using list_api.Security;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.UserRepositoryTests {
	public class UpdateTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		public readonly IEncryptor encryptor;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public UpdateTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			encryptor = test_fixture.Encryptor;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Update_UnknownUser_ByID_ThrowsException() {
			int id_user = 10;
			UserDTO user_dto = new UserDTO { IDRole = 1, Name = "ninesrodriguez", Password = "ninesrodriguez" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Update(id_user.ToString(), user_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void Update_UnknownUser_ByName_ThrowsException() {
			string name_user = "elifparmak";
			UserDTO user_dto = new UserDTO { IDRole = 1, Name = "ninesrodriguez", Password = "ninesrodriguez" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Update(name_user, user_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void Update_ConflictedUser_ByID_ThrowsException() {
			int id_user = 1;
			UserDTO user_dto = new UserDTO { IDRole = 2, Name = "heatherpoe", Password = "heatherpoe" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Update(id_user.ToString(), user_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("User already exists.");
		}
		[Fact]
		public void Update_ConflictedUser_ByName_ThrowsException() {
			string name_user = "ashrivers";
			UserDTO user_dto = new UserDTO { IDRole = 2, Name = "heatherpoe", Password = "heatherpoe" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Update(name_user, user_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("User already exists.");
		}
		[Fact]
		public void Update_User_ByID_ShouldBeReturn() {
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			int id_user = 1;
			UserDTO user_dto = new UserDTO { IDRole = 1, Name = "ninesrodriguez", Password = "ninesrodriguez" };
			FluentActions.Invoking(() => user_repository.Update(id_user.ToString(), user_dto)).Invoke();
			User? user = context.Users.SingleOrDefault(u => u.Name == user_dto.Name);
			user.Should().NotBeNull();
			user?.IDRole.Should().Be(user_dto.IDRole);
			user?.Name.Should().Be(user_dto.Name);
			user?.Password.Should().Be("6ae937bf2e4e0da8dce4b161931d6e5131fce59a175cb82acc49011a4483444f");
		}
		[Fact]
		public void Update_User_ByName_ShouldBeReturn() {
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			string name_user = "ashrivers";
			UserDTO user_dto = new UserDTO { IDRole = 1, Name = "ninesrodriguez", Password = "ninesrodriguez" };
			FluentActions.Invoking(() => user_repository.Update(name_user, user_dto)).Invoke();
			User? user = context.Users.SingleOrDefault(u => u.Name == user_dto.Name);
			user.Should().NotBeNull();
			user?.IDRole.Should().Be(user_dto.IDRole);
			user?.Name.Should().Be(user_dto.Name);
			user?.Password.Should().Be("6ae937bf2e4e0da8dce4b161931d6e5131fce59a175cb82acc49011a4483444f");
		}
	}
}