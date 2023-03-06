using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class ClientUserPatchDTOValidator : AbstractValidator<ClientUserPatchDTO> {
		public ClientUserPatchDTOValidator() { // Constructing.
			RuleFor(cupd => cupd.Name).MinimumLength(8).When(cupd => !string.IsNullOrEmpty(cupd.Name)).WithMessage("Username must have at least 8 characters.");
			RuleFor(cupd => cupd.Name).MaximumLength(100).WithMessage("Username must be at most 100 characters.");
			RuleFor(cupd => cupd.Password).MinimumLength(8).When(cupd => !string.IsNullOrEmpty(cupd.Password)).WithMessage("Password must have at least 8 characters.");
			RuleFor(cupd => cupd.Password).MaximumLength(200).WithMessage("Password must be at most 100 characters.");
		}
	}
}