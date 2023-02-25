using FluentValidation;
namespace list_api.Models.Validators {
	public class ProductValidator : AbstractValidator<Product> {
		public ProductValidator() { // Constructing.
			RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(p => p.Description).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(p => p.Price).GreaterThan(0.0).WithMessage("Price must be greater than 0.0.");
		}
	}
}