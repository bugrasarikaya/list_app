using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class BrandPatchDTOValidator : AbstractValidator<BrandPatchDTO> {
		public BrandPatchDTOValidator() { // Constructing.
			RuleFor(bpd => bpd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
		}
	}
}