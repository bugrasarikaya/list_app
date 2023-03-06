using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class ListProducts {
		public static void AddListProducts(this ListApiDbContext context) {
			context.ListProducts.AddRange(
				new ListProduct { ID = 1, IDList = 1, IDProduct = 3, Quantity = 3, Cost = 35.25 },
				new ListProduct { ID = 2, IDList = 2, IDProduct = 3, Quantity = 1, Cost = 11.75 },
				new ListProduct { ID = 3, IDList = 2, IDProduct = 5, Quantity = 3, Cost = 59.85 },
				new ListProduct { ID = 4, IDList = 3, IDProduct = 6, Quantity = 1, Cost = 23.95 },
				new ListProduct { ID = 5, IDList = 4, IDProduct = 1, Quantity = 1, Cost = 33.50 },
				new ListProduct { ID = 6, IDList = 5, IDProduct = 1, Quantity = 1, Cost = 33.50 },
				new ListProduct { ID = 7, IDList = 6, IDProduct = 4, Quantity = 2, Cost = 99.90 },
				new ListProduct { ID = 8, IDList = 7, IDProduct = 7, Quantity = 3, Cost = 80.70 },
				new ListProduct { ID = 9, IDList = 8, IDProduct = 2, Quantity = 1, Cost = 9.99 }
			);
		}
	}
}