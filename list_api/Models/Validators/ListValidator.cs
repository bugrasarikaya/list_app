using FluentValidation;
namespace list_api.Models.Validators {
	public class ListValidator : AbstractValidator<List> {
		public ListValidator() { // Constructing.
			RuleFor(l => l.IDCategory).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
			RuleFor(l => l.IDUser).GreaterThan(0).WithMessage("User ID must be greater than 0.");
			RuleFor(l => l.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(l => l.Description).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
		}
	}
}