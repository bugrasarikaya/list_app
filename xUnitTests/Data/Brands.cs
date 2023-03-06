using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class Brands {
		public static void AddBrands(this ListApiDbContext context) {
			context.Brands.AddRange(
				new Brand { ID = 1, Name = "Alfa" },
				new Brand { ID = 2, Name = "Banat" },
				new Brand { ID = 3, Name = "Filiz" },
				new Brand { ID = 4, Name = "Loft" },
				new Brand { ID = 5, Name = "Nescafe" },
				new Brand { ID = 7, Name = "Domestos" },
				new Brand { ID = 8, Name = "Tchibo" },
				new Brand { ID = 9, Name = "Cappy" }
			);
		}
	}
}