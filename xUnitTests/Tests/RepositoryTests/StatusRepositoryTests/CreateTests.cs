using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Models.DTOs;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.StatusRepositoryTests {
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
		public void Create_SameStatus_ThrowsException() {
			StatusDTO status_dto = new StatusDTO { Name = "Completed" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Create(status_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Status already exists.");
		}
		[Fact]
		public void Create_Status_ShouldBeReturnSame() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			StatusDTO status_dto = new StatusDTO { Name = "Onshopping" };
			FluentActions.Invoking(() => status_repository.Create(status_dto)).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.Name == status_dto.Name);
			status.Should().NotBeNull();
			status?.Name.Should().Be(status_dto.Name);
		}
	}
}