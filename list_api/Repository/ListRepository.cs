using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
namespace list_api.Repository {
	public class ListRepository : IListRepository {
		private readonly IListApiDbContext context;
		public ListRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public List Create(List list) { // Creating a list.
			List? list_created = new List() { Name = list.Name, Description = list.Description, IDCategory = list.IDCategory, IDUser = list.IDUser, DateTime = list.DateTime, Cost = list.Cost };
			if (context.Lists.Any(l => l.Name == list.Name)) throw new InvalidOperationException("List already exists.");
			context.Lists.Add(list_created);
			context.SaveChanges();
			return list_created;
		}
		public List? Delete(int id) { // Deleting a list.
			List? list_deleted = context.Lists.FirstOrDefault(l => l.ID == id);
			if (list_deleted != null) {
				context.Lists.Remove(list_deleted);
				context.SaveChanges();
			}
			return list_deleted;
		}
		public List? Get(int id) { // Getting a list.
			return context.Lists.FirstOrDefault(l => l.ID == id);
		}
		public ICollection<List> List() { // Listing all lists.
			return context.Lists.ToList();
		}
		public List? Update(int id, List list) { // Updating a list.
			List? list_updated = context.Lists.FirstOrDefault(l => l.ID == id);
			if (list_updated != null) {
				list_updated.IDCategory = list.IDCategory;
				list_updated.IDUser = list.IDUser;
				if (context.Lists.Any(l => l.Name == list.Name)) throw new InvalidOperationException("List already exists.");
				list_updated.Name = list.Name;
				list_updated.Description = list.Description;
				context.SaveChanges();
			}
			return list_updated;
		}
	}
}