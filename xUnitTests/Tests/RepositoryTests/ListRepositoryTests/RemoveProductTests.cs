using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Services;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ListRepositoryTests {
	public class RemoveProductTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public RemoveProductTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void RemoveProduct_UnkownIDList_ThrowsException() {
			int id_list = 10;
			int id_product = 2;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.RemoveProduct(id_list, id_product)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void RemoveProduct_UnkownIDProduct_ThrowsException() {
			int id_list = 8;
			int id_product = 10;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.RemoveProduct(id_list, id_product)).Should().Throw<NotFoundException>().And.Message.Should().Be("Product could not be found.");
		}
		[Fact]
		public void RemoveProduct_ForbiddenStatus_ThrowsException() {
			int id_list = 1;
			int id_product = 3;
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.RemoveProduct(id_list, id_product)).Should().Throw<ForbiddenException>().And.Message.Should().Be("Completed list cannot be changed.");
		}
		[Fact]
		public void RemoveProduct_ListProduct_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			int id_list = 8;
			int id_product = 2;
			FluentActions.Invoking(() => list_repository.RemoveProduct(id_list, id_product)).Invoke();
			ListProduct? list_product = context.ListProducts.SingleOrDefault(lp => lp.IDList == id_list && lp.IDProduct == id_product);
			list_product.Should().NotBeNull();
		}
	}
}