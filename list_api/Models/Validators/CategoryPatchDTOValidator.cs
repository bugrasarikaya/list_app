using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class CategoryPatchDTOValidator : AbstractValidator<CategoryPatchDTO> {
		public CategoryPatchDTOValidator() { // Constructing.
			RuleFor(cpd => cpd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
		}
	}
}