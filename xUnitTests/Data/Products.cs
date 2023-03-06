using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class Products {
		public static void AddProducts(this ListApiDbContext context) {
			context.Products.AddRange(
				new Product { ID = 1, IDBrand = 1, IDCategory = 1, Name = "Ceviz Kabuğundaki Evren", Description = "Science book.", Price = 33.50 },
				new Product { ID = 2, IDBrand = 2, IDCategory = 5, Name = "Hair Brush", Description = "Natural brush for hair.", Price = 9.99 },
				new Product { ID = 3, IDBrand = 3, IDCategory = 4, Name = "Spagetti (Çubuk) 500g", Description = "Special blend sprout spaghetti.", Price = 11.75 },
				new Product { ID = 4, IDBrand = 4, IDCategory = 3, Name = "Regular Fit Male T-Shirt", Description = "%100 cotton.", Price = 49.95 },
				new Product { ID = 5, IDBrand = 5, IDCategory = 4, Name = "Nescafe Gold Filter Coffee 250 G", Description = "%200 organic.", Price = 19.95 },
				new Product { ID = 6, IDBrand = 5, IDCategory = 5, Name = "Face Creme", Description = "Face moisturizer.", Price = 23.95 },
				new Product { ID = 7, IDBrand = 7, IDCategory = 2, Name = "Bleach", Description = "Effective formula.", Price = 26.90 },
				new Product { ID = 8, IDBrand = 8, IDCategory = 4, Name = "Cherry Juice", Description = "Healthy.", Price = 13.00 }
			);
		}
	}
}