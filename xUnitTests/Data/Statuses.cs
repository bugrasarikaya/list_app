using list_api.Data;
using list_api.Models;
namespace xUnitTests.Data {
	public static class Statuses {
		public static void AddStatuses(this ListApiDbContext context) {
			context.Statuses.AddRange(
				new Status { ID = 1, Name = "Completed" },
				new Status { ID = 2, Name = "Uncompleted" },
				new Status { ID = 3, Name = "Updated" }
			);
		}
	}
}