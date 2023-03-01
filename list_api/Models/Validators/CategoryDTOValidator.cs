using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class CategoryDTOValidator : AbstractValidator<CategoryDTO> {
		public CategoryDTOValidator() { // Constructing.
			RuleFor(cd => cd.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(cd => cd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
		}
	}
}