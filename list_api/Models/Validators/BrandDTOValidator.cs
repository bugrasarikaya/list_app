using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class BrandDTOValidator : AbstractValidator<BrandDTO> {
		public BrandDTOValidator() { // Constructing.
			RuleFor(bd => bd.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(bd => bd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
		}
	}
}