using list_api.Models;
using list_api.Repository.Interface;
using list_api.Data;
namespace list_api.Repository {
	public class ListRepository : IListRepository {
		private readonly IListApiDbContext context;
		private List? list { get; set; }
		public ListRepository(IListApiDbContext context) { // Constructing.
			this.context = context;
		}
		public List Create(List list) { // Creating a list.
			list = new List() { Name = list.Name, Description = list.Description, CategoryID = list.CategoryID, UserID = list.UserID, DateTime = list.DateTime, Cost = list.Cost };
			if (context.Lists.Any(l => l.Name == list.Name)) throw new InvalidOperationException("List already exists.");
			context.Lists.Add(list);
			context.SaveChanges();
			return list;
		}
		public List? Delete(int id) { // Deleting a list.
			list = context.Lists.FirstOrDefault(l => l.ID == id);
			if (list != null) {
				context.Lists.Remove(list);
				context.SaveChanges();
			}
			return list;
		}
		public List? Get(int id) { // Getting a list.
			return context.Lists.FirstOrDefault(l => l.ID == id);
		}
		public ICollection<List> List() { // Listing all lists.
			return context.Lists.ToList();
		}
		public List? Update(int id, List list) { // Updating a list.
			this.list = context.Lists.FirstOrDefault(l => l.ID == id);
			if (this.list != null) {
				this.list.Name = list.Name;
				this.list.Description = list.Description;
				this.list.CategoryID = list.CategoryID;
				this.list.UserID = list.UserID;
				this.list.DateTime = list.DateTime;
				this.list.Cost = list.Cost;
				context.SaveChanges();
			}
			return list;
		}
	}
}