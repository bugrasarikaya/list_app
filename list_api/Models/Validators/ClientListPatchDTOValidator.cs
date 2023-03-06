using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ClientListPatchDTOValidator : AbstractValidator<ClientListPatchDTO> {
		public ClientListPatchDTOValidator() { // Constructing.
			RuleFor(clpd => clpd.IDCategory).GreaterThanOrEqualTo(0).WithMessage("Category ID cannot be negative.");
			RuleFor(clpd => clpd.IDStatus).GreaterThanOrEqualTo(0).WithMessage("Status ID cannot be negative.");
			RuleFor(clpd => clpd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(clpd => clpd.Description).MaximumLength(200).WithMessage("Description must be at most 200 characters.");
		}
	}
}