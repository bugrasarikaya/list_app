using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.RoleRepositoryTests {
	public class PatchTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public PatchTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Patch_UnknownRole_ByID_ThrowsException() {
			int id_role = 10;
			RolePatchDTO role_patch_dto = new RolePatchDTO { Name = "Manager" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Patch(id_role.ToString(), role_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Role could not be found.");
		}
		[Fact]
		public void Patch_UnknownRole_ByName_ThrowsException() {
			string name_role = "Observer";
			RolePatchDTO role_patch_dto = new RolePatchDTO { Name = "Manager" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Patch(name_role, role_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Role could not be found.");
		}
		[Fact]
		public void Patch_ConflictedRole_ByID_ThrowsException() {
			int id_role = 1;
			RolePatchDTO role_patch_dto = new RolePatchDTO { Name = "User" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Patch(id_role.ToString(), role_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Role already exists.");
		}
		[Fact]
		public void Patch_ConflictedRole_ByName_ThrowsException() {
			string name_role = "Admin";
			RolePatchDTO role_patch_dto = new RolePatchDTO { Name = "User" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Patch(name_role, role_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Role already exists.");
		}
		[Fact]
		public void Patch_Role_ByID_ShouldBeReturn() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			int id_role = 1;
			RolePatchDTO role_patch_dto = new RolePatchDTO { Name = "Manager" };
			FluentActions.Invoking(() => role_repository.Patch(id_role.ToString(), role_patch_dto)).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.Name == role_patch_dto.Name);
			role.Should().NotBeNull();
		}
		[Fact]
		public void Patch_Role_ByName_ShouldBeReturn() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			string name_role = "Admin";
			RolePatchDTO role_patch_dto = new RolePatchDTO { Name = "Manager" };
			FluentActions.Invoking(() => role_repository.Patch(name_role, role_patch_dto)).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.Name == role_patch_dto.Name);
			role.Should().NotBeNull();
		}
	}
}