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
		public void Patch_UnknownStatus_ByID_ThrowsException() {
			int id_status = 10;
			StatusPatchDTO status_patch_dto = new StatusPatchDTO { Name = "Onshopping" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Patch(id_status.ToString(), status_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Status could not be found.");
		}
		[Fact]
		public void Patch_UnknownStatus_ByName_ThrowsException() {
			string name_status = "Cancelled";
			StatusPatchDTO status_patch_dto = new StatusPatchDTO { Name = "Onshopping" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Patch(name_status, status_patch_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("Status could not be found.");
		}
		[Fact]
		public void Patch_ConflictedStatus_ByID_ThrowsException() {
			int id_status = 1;
			StatusPatchDTO status_patch_dto = new StatusPatchDTO { Name = "Uncompleted" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Patch(id_status.ToString(), status_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Status already exists.");
		}
		[Fact]
		public void Patch_ConflictedStatus_ByName_ThrowsException() {
			string name_status = "Completed";
			StatusPatchDTO status_patch_dto = new StatusPatchDTO { Name = "Uncompleted" };
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			FluentActions.Invoking(() => status_repository.Patch(name_status, status_patch_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Status already exists.");
		}
		[Fact]
		public void Patch_Status_ByID_ShouldBeReturn() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			int id_status = 1;
			StatusPatchDTO status_patch_dto = new StatusPatchDTO { Name = "Onshopping" };
			FluentActions.Invoking(() => status_repository.Patch(id_status.ToString(), status_patch_dto)).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.Name == status_patch_dto.Name);
			status.Should().NotBeNull();
		}
		[Fact]
		public void Patch_Status_ByName_ShouldBeReturn() {
			StatusRepository status_repository = new StatusRepository(cache, context, mapper);
			string name_status = "Completed";
			StatusPatchDTO status_patch_dto = new StatusPatchDTO { Name = "Onshopping" };
			FluentActions.Invoking(() => status_repository.Patch(name_status, status_patch_dto)).Invoke();
			Status? status = context.Statuses.SingleOrDefault(s => s.Name == status_patch_dto.Name);
			status.Should().NotBeNull();
		}
	}
}