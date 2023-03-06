using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.StatusRepositoryTests {
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
		public void Update_UnknownStatus_ByID_ThrowsException() {
			int id_status = 10;
			StatusDTO status_dto = new StatusDTO { Name = "Onshopping" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Update(id_status.ToString(), status_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Status could not be found.");
		}
		[Fact]
		public void Update_UnknownStatus_ByName_ThrowsException() {
			string name_status = "Cancelled";
			StatusDTO status_dto = new StatusDTO { Name = "Onshopping" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Update(name_status, status_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Status could not be found.");
		}
		[Fact]
		public void Update_ConflictedStatus_ByID_ThrowsException() {
			int id_status = 1;
			StatusDTO status_dto = new StatusDTO { Name = "Uncompleted" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Update(id_status.ToString(), status_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Status already exists.");
		}
		[Fact]
		public void Update_ConflictedStatus_ByName_ThrowsException() {
			string name_status = "Completed";
			StatusDTO status_dto = new StatusDTO { Name = "Uncompleted" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Update(name_status, status_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Status already exists.");
		}
		[Fact]
		public void Update_Status_ByID_ShouldBeReturn() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			int id_status = 1;
			StatusDTO status_dto = new StatusDTO { Name = "Onshopping" };
			FluentActions.Invoking(() => status_repository.Update(id_status.ToString(), status_dto)).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.Name == status_dto.Name);
			status.Should().NotBeNull();
		}
		[Fact]
		public void Update_Status_ByName_ShouldBeReturn() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			string name_status = "Completed";
			StatusDTO status_dto = new StatusDTO { Name = "Onshopping" };
			FluentActions.Invoking(() => status_repository.Update(name_status, status_dto)).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.Name == status_dto.Name);
			status.Should().NotBeNull();
		}
	}
}