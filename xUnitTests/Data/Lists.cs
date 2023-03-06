using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class Lists {
		public static void AddLists(this ListApiDbContext context) {
			context.Lists.AddRange(
				new List { ID = 1, IDCategory = 4, IDStatus = 1, IDUser = 1, Name = "List", Description = "This my favourite food list.", DateTimeCompleting = new DateTime(2020, 01, 02), DateTimeCreating = new DateTime(2020, 01, 01), DateTimeUpdating = new DateTime(2020, 01, 02), TotalCost = 35.25 },
				new List { ID = 2, IDCategory = 4, IDStatus = 1, IDUser = 2, Name = "My Dinner List", Description = "I hope my darling likes my dinner.", DateTimeCompleting = new DateTime(2020, 01, 06), DateTimeCreating = new DateTime(2020, 01, 01), DateTimeUpdating = new DateTime(2020, 01, 06), TotalCost = 71.60 },
				new List { ID = 3, IDCategory = 5, IDStatus = 1, IDUser = 2, Name = "My Personal Care List", Description = "Products for facial care.", DateTimeCompleting = new DateTime(2020, 03, 02), DateTimeCreating = new DateTime(2020, 01, 01), DateTimeUpdating = new DateTime(2020, 03, 02), TotalCost = 23.95 },
				new List { ID = 4, IDCategory = 1, IDStatus = 1, IDUser = 3, Name = "Books About Space", Description = "Maybe I make a movie about black holes.", DateTimeCompleting = new DateTime(2019, 01, 06), DateTimeCreating = new DateTime(2019, 01, 05), DateTimeUpdating = new DateTime(2019, 01, 06), TotalCost = 33.50 },
				new List { ID = 5, IDCategory = 1, IDStatus = 2, IDUser = 4, Name = "Science Books", Description = "Things to reference before next Camarilla conference.", DateTimeCompleting = null, DateTimeCreating = new DateTime(2019, 04, 05), DateTimeUpdating = new DateTime(2019, 05, 06), TotalCost = 33.50 },
				new List { ID = 6, IDCategory = 3, IDStatus = 2, IDUser = 5, Name = "Clothing", Description = "I need to change my dirty clothes.", DateTimeCompleting = null, DateTimeCreating = new DateTime(1990, 03, 02), DateTimeUpdating = new DateTime(1990, 03, 03), TotalCost = 99.90 },
				new List { ID = 7, IDCategory = 2, IDStatus = 2, IDUser = 5, Name = "Cleaning", Description = "Must clean stains.", DateTimeCompleting = null, DateTimeCreating = new DateTime(1990, 03, 01), DateTimeUpdating = new DateTime(1990, 03, 02), TotalCost = 80.70 },
				new List { ID = 8, IDCategory = 5, IDStatus = 2, IDUser = 6, Name = "Gift List", Description = "Best gift for my sister.", DateTimeCompleting = null, DateTimeCreating = new DateTime(1991, 02, 01), DateTimeUpdating = new DateTime(1991, 03, 01), TotalCost = 9.99 }
			);
		}
	}
}