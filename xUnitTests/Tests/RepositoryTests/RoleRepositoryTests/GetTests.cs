using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.RoleRepositoryTests {
	public class GetTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public GetTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Get_UnknownRole_ByID_ThrowsException() {
			int id_role = 10;
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Get(id_role.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Role could not be found.");
		}
		[Fact]
		public void Get_UnknownRole_ByName_ThrowsException() {
			string name_role = "Observer";
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Get(name_role)).Should().Throw<NotFoundException>().And.Message.Should().Be("Role could not be found.");
		}
		[Fact]
		public void Get_Role_ByID_ShouldBeReturn() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			int id_role = 1;
			FluentActions.Invoking(() => role_repository.Get(id_role.ToString())).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.ID == id_role);
			role.Should().NotBeNull();
		}
		[Fact]
		public void Get_Role_ByName_ShouldBeReturn() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			string name_role = "Admin";
			FluentActions.Invoking(() => role_repository.Get(name_role)).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.Name == name_role);
			role.Should().NotBeNull();
		}
	}
}