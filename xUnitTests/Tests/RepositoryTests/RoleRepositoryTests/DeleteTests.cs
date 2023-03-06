using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.RoleRepositoryTests {
	public class DeleteTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public DeleteTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Delete_UnknownRole_ByID_ThrowsException() {
			int id_role = 10;
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Delete(id_role.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Role could not be found.");
		}
		[Fact]
		public void Delete_UnknownRole_ByName_ThrowsException() {
			string name_role = "Observer";
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Delete(name_role)).Should().Throw<NotFoundException>().And.Message.Should().Be("Role could not be found.");
		}
		[Fact]
		public void Delete_ConflictedRole_ByID_ShouldBeReturnNull() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			int id_role = 1;
			FluentActions.Invoking(() => role_repository.Delete(id_role.ToString())).Should().Throw<ConflictException>().And.Message.Should().Be("Role exists in a user.");
		}
		[Fact]
		public void Delete_ConflictedRole_ByName_ShouldBeReturnNull() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			string name_role = "Admin";
			FluentActions.Invoking(() => role_repository.Delete(name_role)).Should().Throw<ConflictException>().And.Message.Should().Be("Role exists in a user.");
		}
		[Fact]
		public void Delete_Role_ByID_ShouldBeReturnNull() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			int id_role = 3;
			FluentActions.Invoking(() => role_repository.Delete(id_role.ToString())).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.ID == id_role);
			role.Should().BeNull();
		}
		[Fact]
		public void Delete_Role_ByName_ShouldBeReturnNull() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			string name_role = "Visitor";
			FluentActions.Invoking(() => role_repository.Delete(name_role)).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.Name == name_role);
			role.Should().BeNull();
		}
	}
}