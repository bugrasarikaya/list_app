using FluentValidation;
namespace list_api.Models.Validators {
	public class ListValidator : AbstractValidator<List> {
		public ListValidator() { // Constructing.
			RuleFor(l => l.IDCategory).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
			RuleFor(l => l.IDUser).GreaterThan(0).WithMessage("User ID must be greater than 0.");
			RuleFor(l => l.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(l => l.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(l => l.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
		}
	}
}