using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using list_api.Data;
using list_api.Exceptions;
using list_api.Models;
using list_api.Repository;
using list_api.Models.DTOs;
using list_api.Services;
using xUnitTests.TestSetups;
namespace xUnitTests.Tests.RepositoryTests.ListRepositoryTests {
	public class AddProductTests : IClassFixture<RepositoryTestFixture> {
		public readonly IDistributedCache cache;
		private readonly IListApiDbContext context;
		private readonly IMapper mapper;
		private readonly IMessageService messager;
		public AddProductTests(RepositoryTestFixture test_fixture) {
			cache = test_fixture.Cache;
			context = test_fixture.Context;
			mapper = test_fixture.Mapper;
			messager = test_fixture.Messager;
		}
		[Fact]
		public void AddProduct_UnkownIDList_ThrowsException() {
			ListProductDTO list_product_dto = new ListProductDTO { IDList = 10, IDProduct = 1, Quantity = 1 };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.AddProduct(list_product_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void AddProduct_UnkownIDProduct_ThrowsException() {
			ListProductDTO list_product_dto = new ListProductDTO { IDList = 5, IDProduct = 10, Quantity = 1 };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.AddProduct(list_product_dto)).Should().Throw<NotFoundException>().And.Message.Should().Be("List could not be found.");
		}
		[Fact]
		public void AddProduct_ForbiddenStatus_ThrowsException() {
			ListProductDTO list_product_dto = new ListProductDTO { IDList = 1, IDProduct = 1, Quantity = 1 };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.AddProduct(list_product_dto)).Should().Throw<ForbiddenException>().And.Message.Should().Be("Completed list cannot be changed.");
		}
		[Fact]
		public void AddProduct_ConflictedListProduct_ThrowsException() {
			ListProductDTO list_product_dto = new ListProductDTO { IDList = 5, IDProduct = 4, Quantity = 1 };
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			FluentActions.Invoking(() => list_repository.AddProduct(list_product_dto)).Should().Throw<ConflictException>().And.Message.Should().Be("Product already exists in list.");
		}
		[Fact]
		public void AddProduct_ListProduct_ShouldBeReturn() {
			ListRepository list_repository = new ListRepository(cache, context, mapper, messager);
			ListProductDTO list_product_dto = new ListProductDTO { IDList = 5, IDProduct = 5, Quantity = 1 };
			FluentActions.Invoking(() => list_repository.AddProduct(list_product_dto)).Invoke();
			ListProduct? list_product = context.ListProducts.SingleOrDefault(lp => lp.IDList == list_product_dto.IDList && lp.IDProduct == list_product_dto.IDProduct && lp.Quantity == list_product_dto.Quantity);
			list_product.Should().NotBeNull();
		}
	}
}