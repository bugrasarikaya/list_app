using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ClientUserPatchDTOValidator : AbstractValidator<ClientUserPatchDTO> {
		public ClientUserPatchDTOValidator() { // Constructing.
			RuleFor(u => u.Name).MinimumLength(8).WithMessage("Username must have at least 8 characters.");
			RuleFor(u => u.Name).MaximumLength(100).WithMessage("Username must be at most 100 characters.");
			RuleFor(u => u.Password).MinimumLength(8).WithMessage("Password must have at least 8 characters.");
			RuleFor(u => u.Password).MaximumLength(200).WithMessage("Password must be at most 100 characters.");
		}
	}
}