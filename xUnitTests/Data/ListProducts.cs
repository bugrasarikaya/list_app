using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class ListProducts {
		public static void AddListProducts(this ListApiDbContext context) {
			context.ListProducts.AddRange(
				new ListProduct { ID = 1, IDList = 1, IDProduct = 3, Quantity = 3 },
				new ListProduct { ID = 3, IDList = 2, IDProduct = 3, Quantity = 1 },
				new ListProduct { ID = 4, IDList = 2, IDProduct = 5, Quantity = 3 },
				new ListProduct { ID = 5, IDList = 3, IDProduct = 6, Quantity = 1 },
				new ListProduct { ID = 6, IDList = 4, IDProduct = 1, Quantity = 1 },
				new ListProduct { ID = 8, IDList = 5, IDProduct = 1, Quantity = 1 },
				new ListProduct { ID = 9, IDList = 6, IDProduct = 4, Quantity = 2 },
				new ListProduct { ID = 10, IDList = 7, IDProduct = 7, Quantity = 3 },
				new ListProduct { ID = 11, IDList = 8, IDProduct = 2, Quantity = 1 }
			);
		}
	}
}