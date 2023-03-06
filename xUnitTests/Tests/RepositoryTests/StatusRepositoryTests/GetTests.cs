using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.StatusRepositoryTests {
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
		public void Get_UnknownStatus_ByID_ThrowsException() {
			int id_status = 10;
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Get(id_status.ToString())).Should().Throw<NotFoundException>().And.Message.Should().Be("Status could not be found.");
		}
		[Fact]
		public void Get_UnknownStatus_ByName_ThrowsException() {
			string name_status = "Cancelled";
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Get(name_status)).Should().Throw<NotFoundException>().And.Message.Should().Be("Status could not be found.");
		}
		[Fact]
		public void Get_Status_ByID_ShouldBeReturn() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			int id_status = 1;
			FluentActions.Invoking(() => status_repository.Get(id_status.ToString())).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.ID == id_status);
			status.Should().NotBeNull();
		}
		[Fact]
		public void Get_Status_ByName_ShouldBeReturn() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			string name_status = "Completed";
			FluentActions.Invoking(() => status_repository.Get(name_status)).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.Name == name_status);
			status.Should().NotBeNull();
		}
	}
}