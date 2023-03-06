using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.RoleRepositoryTests {
	public class CreateTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		public CreateTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
		}
		[Fact]
		public void Create_SameRole_ThrowsException() {
			RoleDTO role_dto = new RoleDTO { Name = "Admin" };
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			FluentActions.Invoking(() => role_repository.Create(role_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Role already exists.");
		}
		[Fact]
		public void Create_Role_ShouldBeReturnSame() {
			RoleRepository role_repository = new RoleRepository(cache, context, mapper);
			RoleDTO role_dto = new RoleDTO { Name = "Manager" };
			FluentActions.Invoking(() => role_repository.Create(role_dto)).Invoke();
			Role? role = context.Roles.SingleOrDefault(r => r.Name == role_dto.Name);
			role.Should().NotBeNull();
			role?.Name.Should().Be(role_dto.Name);
		}
	}
}