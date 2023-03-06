using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ProductDTOValidator : AbstractValidator<ProductDTO> {
		public ProductDTOValidator() { // Constructing.
			RuleFor(pd => pd.IDBrand).GreaterThan(0).WithMessage("Brand ID must be greater than 0.");
			RuleFor(pd => pd.IDCategory).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
			RuleFor(pd => pd.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(pd => pd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(pd => pd.Description).MaximumLength(100).WithMessage("Maximum character count is 100.");
			RuleFor(pd => pd.Price).GreaterThan(0.0).WithMessage("Price must be greater than 0.0.");
		}
	}
}