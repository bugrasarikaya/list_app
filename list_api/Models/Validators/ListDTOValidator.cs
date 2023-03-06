using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ListDTOValidator : AbstractValidator<ListDTO> {
		public ListDTOValidator() { // Constructing.
			RuleFor(ld => ld.IDCategory).GreaterThan(0).WithMessage("Category ID must be greater than 0.");
			RuleFor(ld => ld.IDUser).GreaterThan(0).WithMessage("User ID must be greater than 0.");
			RuleFor(ld => ld.Name).NotNull().NotEmpty().WithMessage("Name cannot be empty.");
			RuleFor(ld => ld.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(ld => ld.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
		}
	}
}