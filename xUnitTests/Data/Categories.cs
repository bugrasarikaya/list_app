using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class Categories {
		public static void AddCategories(this ListApiDbContext context) {
			context.Categories.AddRange(
				new Category { ID = 1, Name = "Book" },
				new Category { ID = 2, Name = "Cleaning" },
				new Category { ID = 3, Name = "Clothes" },
				new Category { ID = 4, Name = "Food" },
				new Category { ID = 5, Name = "Personal Care" },
				new Category { ID = 6, Name = "Toys" }
			);
		}
	}
}