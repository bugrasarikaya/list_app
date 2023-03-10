using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using list_api.Security;
using list_api.Services;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ClientRepositoryTests {
	public class PatchUserTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		public readonly IEncryptor encryptor;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public PatchUserTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			encryptor = test_fixture.Encryptor;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void Patch_UnknownUser_ByID_ThrowsException() {
			ClientUserPatchDTO client_user_patch_dto = new ClientUserPatchDTO { Name = "ninesrodriguez", Password = "ninesrodriguez" };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 10;
			FluentActions.Invoking(() => client_repository.PatchUser(client_user_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("User could not be found.");
		}
		[Fact]
		public void Patch_ConflictedUser_ByID_ThrowsException() {
			ClientUserPatchDTO client_user_patch_dto = new ClientUserPatchDTO {  Name = "heatherpoe", Password = "heatherpoe" };
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			FluentActions.Invoking(() => client_repository.PatchUser(client_user_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("User already exists.");
		}
		[Fact]
		public void Patch_User_ByID_ShouldBeReturn() {
			ClientRepository client_repository = new ClientRepository(cache, context, mapper, messager);
			client_repository.IDUser = 1;
			ClientUserPatchDTO client_user_patch_dto = new ClientUserPatchDTO { Name = "ninesrodriguez", Password = "ninesrodriguez" };
			FluentActions.Invoking(() => client_repository.PatchUser(client_user_patch_dto)).Invoke();
			User? user = context.Users.SingleOrDefault(u => u.Name == client_user_patch_dto.Name);
			user.Should().NotBeNull();
			user?.Name.Should().Be(client_user_patch_dto.Name);
			user?.Password.Should().Be("6ae937bf2e4e0da8dce4b161931d6e5131fce59a175cb82acc49011a4483444f");
		}
	}
}