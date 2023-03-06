using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ProductPatchDTOValidator : AbstractValidator<ProductPatchDTO> {
		public ProductPatchDTOValidator() { // Constructing.
			RuleFor(ppd => ppd.IDBrand).GreaterThanOrEqualTo(0).WithMessage("Brand ID cannot be negative.");
			RuleFor(ppd => ppd.IDCategory).GreaterThanOrEqualTo(0).WithMessage("Category ID cannot be negative.");
			RuleFor(ppd => ppd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(ppd => ppd.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
			RuleFor(ppd => ppd.Price).GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
		}
	}
}