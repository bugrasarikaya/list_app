using list_api.Data;
namespace xUnitTests.Data {
	public static class Feed {
		public static void Database(ListApiDbContext context) {
			context.AddBrands();
			context.AddCategories();
			context.AddListProducts();
			context.AddLists();
			context.AddProducts();
			context.AddRoles();
			context.AddStatuses();
			context.AddUsers();
			context.SaveChanges();
		}
	}
}