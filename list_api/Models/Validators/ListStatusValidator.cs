using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using System.Reflection;
namespace list_api.Models.Validators {
	public class ListStatusValidator : AbstractValidator<JsonPatchDocument<List>> {
		public ListStatusValidator() { // Constructing.
			List<string> list_path = new List<string>();
			PropertyInfo[] list_property = typeof(List).GetProperties();
			for (int i = 1; i < list_property.Count(); i++) list_path.Add("/" + list_property[i].ToString());
			list_path.Remove("/DateTime");
			list_path.Remove("/TotalCost");
			RuleForEach(jpd => jpd.Operations.Select(o => o.op)).NotNull().NotEmpty().WithMessage("Operation connot be empty.");
			RuleForEach(jpd => jpd.Operations.Select(o => o.op)).Equal("replace", StringComparer.OrdinalIgnoreCase).WithMessage("Operation must be \"replace\".");
			RuleForEach(jpd => jpd.Operations.Select(o => o.path)).Must(x => list_path.Contains(x, StringComparer.OrdinalIgnoreCase)).WithMessage("Path must be \"/{property}\".");
			RuleForEach(jpd => jpd.Operations.Where(o => string.Equals(o.path, "/IDCategory", StringComparison.OrdinalIgnoreCase)).Select(o => (int)o.value)).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
			RuleForEach(jpd => jpd.Operations.Where(o => string.Equals(o.path, "/IDUser", StringComparison.OrdinalIgnoreCase)).Select(o => (int)o.value)).GreaterThan(0).WithMessage("User ID must be greater than 0.");
			RuleForEach(jpd => jpd.Operations.Where(o => string.Equals(o.path, "/Name", StringComparison.OrdinalIgnoreCase)).Select(o => (string)o.value)).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleForEach(jpd => jpd.Operations.Where(o => string.Equals(o.path, "/Description", StringComparison.OrdinalIgnoreCase)).Select(o => (string)o.value)).MaximumLength(200).WithMessage("Maximum character count is 200.");
			RuleForEach(jpd => jpd.Operations.Where(o => string.Equals(o.path, "/Status", StringComparison.OrdinalIgnoreCase)).Select(o => (string)o.value)).Equal("Completed").WithMessage("Value must be \"Completed\".");
		}
	}
}