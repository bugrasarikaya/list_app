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
	public class UpdateTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public UpdateTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Update_UnknownRole_ByID_ThrowsException() {
			int id_role = 10;
			RoleDTO role_dto = new RoleDTO { Name = "Manager" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Update(id_role.ToString(), role_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Role could not be found.");
		}
		[Fact]
		public void Update_UnknownRole_ByName_ThrowsException() {
			string name_role = "Observer";
			RoleDTO role_dto = new RoleDTO { Name = "Manager" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Update(name_role, role_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Role could not be found.");
		}
		[Fact]
		public void Update_ConflictedRole_ByID_ThrowsException() {
			int id_role = 1;
			RoleDTO role_dto = new RoleDTO { Name = "User" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Update(id_role.ToString(), role_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Role already exists.");
		}
		[Fact]
		public void Update_ConflictedRole_ByName_ThrowsException() {
			string name_role = "Admin";
			RoleDTO role_dto = new RoleDTO { Name = "User" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Update(name_role, role_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Role already exists.");
		}
		[Fact]
		public void Update_Role_ByID_ShouldBeReturn() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			int id_role = 1;
			RoleDTO role_dto = new RoleDTO { Name = "Manager" };
			FluentActions.Invoking(() => role_repository.Update(id_role.ToString(), role_dto)).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.Name == role_dto.Name);
			role.Should().NotBeNull();
		}
		[Fact]
		public void Update_Role_ByName_ShouldBeReturn() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			string name_role = "Admin";
			RoleDTO role_dto = new RoleDTO { Name = "Manager" };
			FluentActions.Invoking(() => role_repository.Update(name_role, role_dto)).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.Name == role_dto.Name);
			role.Should().NotBeNull();
		}
	}
}