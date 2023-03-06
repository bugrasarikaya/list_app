using FluentValidation;
using list_api.Models.DTOs;
namespace list_api.Models.Validators {
	public class UserPatchDTOValidator : AbstractValidator<UserPatchDTO> {
		public UserPatchDTOValidator() { // Constructing.
			RuleFor(upd => upd.IDRole).GreaterThanOrEqualTo(0).WithMessage("Role ID cannot be negative.");
			RuleFor(upd => upd.Name).MinimumLength(8).When(cupd => !string.IsNullOrEmpty(cupd.Name)).WithMessage("Username must have at least 8 characters.");
			RuleFor(upd => upd.Name).MaximumLength(100).WithMessage("Name must be at most 100 characters.");
			RuleFor(upd => upd.Password).MinimumLength(8).When(cupd => !string.IsNullOrEmpty(cupd.Password)).WithMessage("Password must have at least 8 characters.");
			RuleFor(upd => upd.Password).MaximumLength(100).WithMessage("Password must be at most 100 characters.");
		}
	}
}