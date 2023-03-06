using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.StatusRepositoryTests {
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
		public void Delete_UnknownStatus_ByID_ThrowsException() {
			int id_status = 10;
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Delete(id_status.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Status could not be found.");
		}
		[Fact]
		public void Delete_UnknownStatus_ByName_ThrowsException() {
			string name_status = "Cancelled";
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Delete(name_status)).Should().Throw<NotFoundException>().And.Message.Should().Be("Status could not be found.");
		}
		[Fact]
		public void Delete__ConflictedStatus_ByID_ShouldBeReturnNull() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			int id_status = 1;
			FluentActions.Invoking(() => status_repository.Delete(id_status.ToString())).Should().Throw<ConflictException>().And.Message.Should().Be("Status exists in a list.");
		}
		[Fact]
		public void Delete_ConflictedStatus_ByName_ShouldBeReturnNull() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			string name_status = "Completed";
			FluentActions.Invoking(() => status_repository.Delete(name_status)).Should().Throw<ConflictException>().And.Message.Should().Be("Status exists in a list.");
		}
		[Fact]
		public void Delete_Status_ByID_ShouldBeReturnNull() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			int id_status = 3;
			FluentActions.Invoking(() => status_repository.Delete(id_status.ToString())).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.ID == id_status);
			status.Should().BeNull();
		}
		[Fact]
		public void Delete_Status_ByName_ShouldBeReturnNull() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			string name_status = "Updated";
			FluentActions.Invoking(() => status_repository.Delete(name_status)).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.Name == name_status);
			status.Should().BeNull();
		}
	}
}