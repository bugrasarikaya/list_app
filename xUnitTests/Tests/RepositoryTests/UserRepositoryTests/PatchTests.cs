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
	public class PatchTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		public readonly IEncryptor encryptor;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public PatchTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			encryptor = test_fixture.Encryptor;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Patch_UnknownUser_ByID_ThrowsException() {
			int id_user = 10;
			UserPatchDTO user_patch_dto = new UserPatchDTO { IDRole = 1, Name = "ninesrodriguez", Password = "ninesrodriguez" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Patch(id_user.ToString(), user_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void Patch_UnknownUser_ByName_ThrowsException() {
			string name_user = "elifparmak";
			UserPatchDTO user_patch_dto = new UserPatchDTO { IDRole = 1, Name = "ninesrodriguez", Password = "ninesrodriguez" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Patch(name_user, user_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void Patch_ConflictedUser_ByID_ThrowsException() {
			int id_user = 1;
			UserPatchDTO user_patch_dto = new UserPatchDTO { IDRole = 2, Name = "heatherpoe", Password = "heatherpoe" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Patch(id_user.ToString(), user_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("User already exists.");
		}
		[Fact]
		public void Patch_ConflictedUser_ByName_ThrowsException() {
			string name_user = "ashrivers";
			UserPatchDTO user_patch_dto = new UserPatchDTO { IDRole = 2, Name = "heatherpoe", Password = "heatherpoe" };
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			FluentActions.Invoking(() => user_repository.Patch(name_user, user_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("User already exists.");
		}
		[Fact]
		public void Patch_User_ByID_ShouldBeReturn() {
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			int id_user = 1;
			UserPatchDTO user_patch_dto = new UserPatchDTO { IDRole = 1, Name = "ninesrodriguez", Password = "ninesrodriguez" };
			FluentActions.Invoking(() => user_repository.Patch(id_user.ToString(), user_patch_dto)).Invoke();
			User? user = context.Users.SingleOrDefault(u => u.Name == user_patch_dto.Name);
			user.Should().NotBeNull();
			user?.IDRole.Should().Be(user_patch_dto.IDRole);
			user?.Name.Should().Be(user_patch_dto.Name);
			user?.Password.Should().Be("6ae937bf2e4e0da8dce4b161931d6e5131fce59a175cb82acc49011a4483444f");
		}
		[Fact]
		public void Patch_User_ByName_ShouldBeReturn() {
			UserRepository user_repository = new UserRepository(cache, encryptor, context, mapper);
			string name_user = "ashrivers";
			UserPatchDTO user_patch_dto = new UserPatchDTO { IDRole = 1, Name = "ninesrodriguez", Password = "ninesrodriguez" };
			FluentActions.Invoking(() => user_repository.Patch(name_user, user_patch_dto)).Invoke();
			User? user = context.Users.SingleOrDefault(u => u.Name == user_patch_dto.Name);
			user.Should().NotBeNull();
			user?.IDRole.Should().Be(user_patch_dto.IDRole);
			user?.Name.Should().Be(user_patch_dto.Name);
			user?.Password.Should().Be("6ae937bf2e4e0da8dce4b161931d6e5131fce59a175cb82acc49011a4483444f");
		}
	}
}