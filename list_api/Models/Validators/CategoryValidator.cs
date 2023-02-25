using FluentValidation;
namespace list_api.Models.Validators {
	public class CategoryValidator : AbstractValidator<Category> {
		public CategoryValidator() { // Constructing.
			RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
		}
	}
}