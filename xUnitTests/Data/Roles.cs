using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class Roles {
		public static void AddRoles(this ListApiDbContext context) {
			context.Roles.AddRange(
				new Role { ID = 1, Name = "Admin" },
				new Role { ID = 2, Name = "User" },
				new Role { ID = 3, Name = "Visitor" }
			);
		}
	}
}