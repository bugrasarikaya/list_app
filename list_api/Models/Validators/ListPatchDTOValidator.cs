using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ListPatchDTOValidator : AbstractValidator<ListPatchDTO> {
		public ListPatchDTOValidator() { // Constructing.
			RuleFor(lpd => lpd.IDCategory).GreaterThanOrEqualTo(0).WithMessage("Category ID cannot be negative.");
			RuleFor(lpd => lpd.IDUser).GreaterThanOrEqualTo(0).WithMessage("User ID cannot be negative.");
			RuleFor(lpd => lpd.IDStatus).GreaterThanOrEqualTo(0).WithMessage("Status ID cannot be negative.");
			RuleFor(lpd => lpd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(lpd => lpd.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
		}
	}
}