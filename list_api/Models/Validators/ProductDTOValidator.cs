using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ProductDTOValidator : AbstractValidator<ProductDTO> {
		public ProductDTOValidator() { // Constructing.
			RuleFor(p => p.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(cd => cd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(p => p.Description).MaximumLength(100).WithMessage("Maximum character count is 100.");
			RuleFor(p => p.Price).GreaterThan(0.0).WithMessage("Price must be greater than 0.0.");
		}
	}
}